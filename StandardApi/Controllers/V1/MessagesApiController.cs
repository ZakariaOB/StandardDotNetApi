using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StandardApi.Cache;
using StandardApi.Contracts;
using StandardApi.Contracts.Contracts.V1.Requests.Queries;
using StandardApi.Contracts.Contracts.V1.Responses;
using StandardApi.Contracts.V1.Requests;
using StandardApi.Contracts.V1.Responses;
using StandardApi.Domain;
using StandardApi.Extensions;
using StandardApi.Helpers;
using StandardApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StandardApi.Controllers.V1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    public class MessagesApiController : Controller
    {
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public MessagesApiController(IMessageService messageService, IMapper mapper, IUriService uriService)
        {
            _messageService = messageService;
            _mapper = mapper;
            _uriService = uriService;
        }

        /// <summary>
        /// Returns all the messages in the System
        /// </summary>
        /// <response code="200">Returns all the messages in the System</response>
        /// <response code="400">Unable to create the message due to validation error</response>
        [HttpGet(ApiRoutes.Messages.GetAll)]
        [Cached(600)]
        public async Task<IActionResult> GetAll([FromQuery]PaginationQuery paginationQuery)
        {
            var pagination = _mapper.Map<PaginationFilter>(paginationQuery);
            
            var messages = await _messageService.GetMessagesAsync(pagination);
            
            var messageResponses = _mapper.Map<IEnumerable<MessageResponse>>(messages);
            
            if (pagination == null || pagination.PageNumber < 1 || pagination.PageSize < 1)
            {
                return Ok(new PagedResponse<MessageResponse>(messageResponses));
            }

            _uriService.Init(ApiRoutes.Messages.GetAll);
            PagedResponse<MessageResponse> paginatedResponse = PaginationHelper.CreatePaginatedResponse(
                _uriService, 
                pagination, 
                messageResponses);
            
            return Ok(paginatedResponse);
        }

        [HttpGet(ApiRoutes.Messages.GetAllWithPrevilige)]
        [Authorize(Policy = "MessagePrevilege")]
        public async Task<IActionResult> GetAllWithPrevilige()
        {
            var messages = await _messageService.GetMessagesAsync();
            // Random test just to check authorization with handlers
            return Ok(messages.Where(m => !string.IsNullOrEmpty(m.Text)));
        }

        [HttpGet(ApiRoutes.Messages.Get)]
        public async Task<IActionResult> Get([FromRoute]Guid messageId)
        {
            var message = await _messageService.GetMessageByIdAsync(messageId);
            if (message == null)
                return NotFound();
            return Ok(_mapper.Map<MessageResponse>(message));
        }

        [HttpPost(ApiRoutes.Messages.Create)]
        [ProducesResponseType(typeof(MessageResponse), 201)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> Create([FromBody]CreateMessageRequest request)
        {
            var message = new Message
            { 
                Text = request.Text,
                UserId = HttpContext.GetUserId()
            };

            var created = await _messageService.CreateMessageAsync(message);
            if (!created)
            {
                return BadRequest(new ErrorResponse
                {
                    Errors = new List<ErrorModel>
                    {
                        new ErrorModel
                        {
                            Message = "Not able to create the message"
                        }
                    }
                });
            }

            var locationUri = _uriService.GetMessageUri(message.Id.ToString());
            var response = new MessageResponse { Id = message.Id };

            return Created(locationUri, new Response<MessageResponse>(response));
        }

        [HttpPut(ApiRoutes.Messages.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid messageId, [FromBody]UpdateMessageRequest request)
        {
            var userOwnsPost = await _messageService.UserOwnMessage(messageId, HttpContext.GetUserId());
            if (!userOwnsPost)
            {
                return BadRequest(new { error = "You don't own this message" });
            }
            var message = new Message 
            { 
                Id = messageId,
                Text = request.Text
            };
            message.Text = request.Text;

            var isUpdated = await _messageService.UpdateMessageAsync(message);
            if (isUpdated)
                return Ok(new Response<MessageResponse>(_mapper.Map<MessageResponse>(message)));

            return NotFound();
        }

        [HttpDelete(ApiRoutes.Messages.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid messageId)
        {
            var userOwnsPost = await _messageService.UserOwnMessage(messageId, HttpContext.GetUserId());
            if (!userOwnsPost)
            {
                return BadRequest(new { error = "You don't own this message" });
            }
            var deleted = await _messageService.DeleteMessageAsync(messageId);
            if (deleted)
                return NoContent();

            return NotFound();
        }
    }

}
