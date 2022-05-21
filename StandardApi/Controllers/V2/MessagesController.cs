using Microsoft.AspNetCore.Mvc;
using StandardApi.Contracts;
using StandardApi.Domain.V2;
using System.Collections.Generic;

namespace StandardApi.Controllers.V2
{
    public class MessagesController : Controller
    {
        private List<MessageV2> _messages;

        public MessagesController()
        {
            _messages = new List<MessageV2>();
            for (int i = 0; i < 10; i++)
            {
                _messages.Add(new MessageV2
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
