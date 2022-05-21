using Microsoft.AspNetCore.Mvc;
using StandardApi.Contracts;
using StandardApi.Domain.V1;
using StandardApi.Domain.V2;
using System;
using System.Collections.Generic;

namespace StandardApi.Controllers.V1
{
    [ApiController]
    [Route("api/products")]
    [ApiVersion("1.0", Deprecated = true)]
    [ApiVersion("2.0")]
    public class ProductsController : ControllerBase
    {
        private List<Message> _messages;

        private List<MessageV2> _messagesV2;


        public ProductsController()
        {
            _messages = new List<Message>();
            for (int i = 0; i < 10; i++)
            {
                _messages.Add(new Message
                {
                    Id = Guid.NewGuid()
                });
            }
            _messagesV2 = new List<MessageV2>();
            for (int i = 0; i < 10; i++)
            {
                _messagesV2.Add(new MessageV2
                {
                    MessageIdentifier = $"Message identifier : { i }"
                }); ;
            }
        }

        [HttpGet("all")]
        [MapToApiVersion("1.0")]
        public IActionResult GetAllV1()
        {
            return Ok(this._messages);
        }

        [HttpGet("all")]
        [MapToApiVersion("2.0")]
        public IActionResult GetAllV2()
        {
            return Ok(this._messages);
        }
    }
}
