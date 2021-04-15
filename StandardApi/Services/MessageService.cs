using StandardApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StandardApi.Services
{
    public class MessageService : IMessageService
    {
        private List<Message> _messages;

        public MessageService()
        {
            _messages = new List<Message>();
            for (int i = 0; i < 10; i++)
            {
                _messages.Add(new Message
                {
                    Id = Guid.NewGuid(),
                    Text = $"MESSAGE - { i }"
                });
            }
        }

        public Message GetMessageById(Guid messageId)
        {
            return _messages.FirstOrDefault(item => item.Id == messageId);
        }

        public List<Message> GetMessages()
        {
            return _messages;
        }
    }
}
