using Microsoft.AspNetCore.Mvc;
using StandardApi.Contracts;
using StandardApi.Controllers.V1.Requests;
using StandardApi.Controllers.V1.Responses;
using StandardApi.Domain;
using StandardApi.Services;
using System;

namespace StandardApi.Controllers.V1
{
    public class MessagesController : Controller
    {
        private readonly IMessageService _messageService;

        public MessagesController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet(ApiRoutes.Messages.GetAll)]
        public IActionResult GetAll()
        {
            return Ok(_messageService.GetMessages());
        }

        [HttpGet(ApiRoutes.Messages.Get)]
        public IActionResult Get([FromRoute]Guid messageId)
        {
            var message = _messageService.GetMessageById(messageId);
            if (message == null)
            {
                return NotFound();
            }
            return Ok(message);
        }

        [HttpPost(ApiRoutes.Messages.Create)]
        public IActionResult Create([FromBody]CreateMessageRequest request)
        {
            var message = new Message { Id = request.Id };
            if (request.Id == Guid.Empty)
            {
                message.Id = Guid.NewGuid();
            }

            message.Text = request.Text;
            _messageService.GetMessages().Add(message);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.Messages.Get.Replace("{messageId}", message.Id.ToString());

            var response = new MessageResponse { Id = message.Id };
            return Created(locationUri, response);
        }
    }

}
