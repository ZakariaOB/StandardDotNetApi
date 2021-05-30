using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StandardApi.Contracts;
using StandardApi.Contracts.V1.Requests;
using StandardApi.Contracts.V1.Responses;
using StandardApi.Domain;
using StandardApi.Extensions;
using StandardApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StandardApi.Controllers.V1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MessagesApiController : Controller
    {
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;

        public MessagesApiController(IMessageService messageService, IMapper mapper)
        {
            _messageService = messageService;
            _mapper = mapper;
        }

        [HttpGet(ApiRoutes.Messages.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var messages = await _messageService.GetMessagesAsync();

            var messageResponses = _mapper.Map<IEnumerable<MessageResponse>>(messages);

            return Ok(messageResponses);
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
            return Ok(message);
        }

        [HttpPost(ApiRoutes.Messages.Create)]
        public async Task<IActionResult> Create([FromBody]CreateMessageRequest request)
        {
            var message = new Message
            { 
                Text = request.Text,
                UserId = HttpContext.GetUserId()
            };

            await _messageService.CreateMessageAsync(message);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.Messages.Get.Replace("{messageId}", message.Id.ToString());

            var response = new MessageResponse { Id = message.Id };
            return Created(locationUri, response);
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
                return Ok(message);

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
