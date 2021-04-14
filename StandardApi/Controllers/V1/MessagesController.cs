using Microsoft.AspNetCore.Mvc;
using StandardApi.Contracts;
using StandardApi.Controllers.V1.Requests;
using StandardApi.Controllers.V1.Responses;
using StandardApi.Domain;
using System;
using System.Collections.Generic;

namespace StandardApi.Controllers.V1
{
    public class MessagesController : Controller
    {
        private List<Message> _messages;

        public MessagesController()
        {
            _messages = new List<Message>();
            for (int i = 0; i < 10; i++)
            {
                _messages.Add(new Message 
                { 
                    Id = Guid.NewGuid()
                });
            }
        }

        [HttpGet(ApiRoutes.Messages.GetAll)]
        public IActionResult GetAll()
        {
            return Ok(this._messages);
        }

        [HttpPost(ApiRoutes.Messages.Create)]
        public IActionResult Create([FromBody]CreateMessageRequest request)
        {
            var message = new Message { Id = request.Id };
            if (request.Id == Guid.Empty)
                message.Id = Guid.NewGuid();

            _messages.Add(message);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.Messages.Get.Replace("{messageId}", message.Id.ToString());

            var response = new MessageResponse { Id = message.Id };
            return Created(locationUri, response);
        }
    }

}
