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

        public bool UpdateMessage(Message message)
        {
            var exists = GetMessageById(message.Id) != null;

            if (!exists)
                return false;

            int index = _messages.FindIndex(x => x.Id == message.Id);
            _messages[index] = message;

            return true;
        }

        public bool DeleteMessage(Guid messageId)
        {
            var message = GetMessageById(messageId);
            if (message == null)
                return false;

            _messages.Remove(message);
            return true;
        }
    }
}
