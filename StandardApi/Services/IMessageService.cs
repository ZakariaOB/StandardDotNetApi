using StandardApi.Domain;
using System;
using System.Collections.Generic;

namespace StandardApi.Services
{
    public interface IMessageService
    {
        List<Message> GetMessages();

        Message GetMessageById(Guid messageId);
    }
}
