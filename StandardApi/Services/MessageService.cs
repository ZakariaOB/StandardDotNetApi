using Microsoft.EntityFrameworkCore;
using StandardApi.Data;
using StandardApi.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StandardApi.Services
{
    public class MessageService : IMessageService
    {
        private readonly DataContext _dataContext;

        public MessageService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Message> GetMessageByIdAsync(Guid messageId)
        {
            return await _dataContext.Messages.FirstOrDefaultAsync(item => item.Id == messageId);
        }

        public async Task<List<Message>> GetMessagesAsync()
        {
            return await _dataContext.Messages.ToListAsync();
        }

        public async Task<bool> UpdateMessageAsync(Message message)
        {
            _dataContext.Messages.Update(message);
            var updated = await _dataContext.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> CreateMessageAsync(Message message)
        {
            _dataContext.Messages.Add(message);
            var created = await _dataContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> DeleteMessageAsync(Guid messageId)
        {
            var message = await GetMessageByIdAsync(messageId);
            if (message == null)
                return false;

            _dataContext.Messages.Remove(message);
            var deleted = await _dataContext.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<bool> UserOwnMessage(Guid messageId, string userId)
        {
            var message = await _dataContext.Messages.AsNoTracking().SingleOrDefaultAsync(r => r.Id == messageId && r.UserId == userId);
            if (message == null)
            {
                return false;
            }
            if (message.UserId != userId)
            {
                return false;
            }
            return true;
        }
    }
}
