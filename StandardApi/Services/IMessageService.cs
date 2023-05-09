using StandardApi.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StandardApi.Services
{
    public interface IMessageService
    {
        Task<List<Message>> GetMessagesAsync();

        Task<Message> GetMessageByIdAsync(Guid messageId);

        Task<bool> UpdateMessageAsync(Message message);

        Task<bool> CreateMessageAsync(Message message);

        Task<bool> DeleteMessageAsync(Guid messageId);

        Task<bool> UserOwnMessage(Guid messageId, string v);
    }
}
