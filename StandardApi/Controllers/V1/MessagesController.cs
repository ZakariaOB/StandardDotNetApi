using Microsoft.AspNetCore.Mvc;
using StandardApi.Contracts;
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
    }
}
