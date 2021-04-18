using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StandardApi.Contracts;
using StandardApi.Contracts.V1.Requests;
using StandardApi.Contracts.V1.Responses;
using StandardApi.Domain;
using StandardApi.Services;
using System;
using System.Threading.Tasks;

namespace StandardApi.Controllers.V1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MessagesApiController : Controller
    {
        private readonly IMessageService _messageService;

        public MessagesApiController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet(ApiRoutes.Messages.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var messages = await _messageService.GetMessagesAsync();
            return Ok(messages);
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
            var message = new Message { Text = request.Text};

            await _messageService.CreateMessageAsync(message);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.Messages.Get.Replace("{messageId}", message.Id.ToString());

            var response = new MessageResponse { Id = message.Id };
            return Created(locationUri, response);
        }

        [HttpPut(ApiRoutes.Messages.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid messageId, [FromBody]UpdateMessageRequest request)
        {
            var message = new Message 
            { 
                Id = messageId,
                Text = request.Text
            };

            var isUpdated = await _messageService.UpdateMessageAsync(message);
            if (isUpdated)
                return Ok(message);

            return NotFound();
        }

        [HttpDelete(ApiRoutes.Messages.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid messageId)
        {
            var deleted = await _messageService.DeleteMessageAsync(messageId);
            if (deleted)
                return NoContent();

            return NotFound();
        }
    }

}
