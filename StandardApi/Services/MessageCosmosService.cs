using Cosmonaut;
using Cosmonaut.Extensions;
using StandardApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StandardApi.Services
{
    public class MessageCosmosService : IMessageService
    {
        private readonly ICosmosStore<MessageCosmos> _cosmosStore;

        public MessageCosmosService(ICosmosStore<MessageCosmos> cosmosStore)
        {
            _cosmosStore = cosmosStore;
        }

        public async Task<bool> CreateMessageAsync(Message message)
        {
            var messageCosmos = new MessageCosmos
            {
                Id = Guid.NewGuid().ToString(),
                Text = message.Text
            };

            var response = await _cosmosStore.AddAsync(messageCosmos);
            message.Id = Guid.Parse(messageCosmos.Id);
            return response.IsSuccess;
        }

        public async Task<bool> DeleteMessageAsync(Guid messageId)
        {
            var response = await _cosmosStore.RemoveByIdAsync(messageId.ToString(), messageId.ToString());
            return response.IsSuccess;
        }

        public async Task<Message> GetMessageByIdAsync(Guid messageId)
        {
            var messageCosmos = await _cosmosStore.FindAsync(messageId.ToString(), messageId.ToString());

            return messageCosmos == null ? null : new Message { Id = Guid.Parse(messageCosmos.Id), Text = messageCosmos.Text}; 
        }

        public async Task<List<Message>> GetMessagesAsync()
        {
            var messages = await _cosmosStore.Query().ToListAsync();
            return messages.Select(m => new Message { Id = Guid.Parse(m.Id), Text = m.Text }).ToList();
        }

        public async Task<bool> UpdateMessageAsync(Message message)
        {
            var messageCosmos = new MessageCosmos
            {
                Id = message.Id.ToString(),
                Text = message.Text
            };

            var response = await _cosmosStore.UpdateAsync(messageCosmos);
            return response.IsSuccess;
        }
    }
}
