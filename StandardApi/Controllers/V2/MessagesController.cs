using Microsoft.AspNetCore.Mvc;
using StandardApi.Contracts;
using StandardApi.Domain.V2;
using System.Collections.Generic;

namespace StandardApi.Controllers.V2
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
                    MessageIdentifier = $"Message identifier : { i }"
                }); ;
            }
        }

        [HttpGet(ApiRoutes.Messages.GetAllVTwo)]
        public IActionResult GetAll()
        {
            return Ok(this._messages);
        }
    }
}
