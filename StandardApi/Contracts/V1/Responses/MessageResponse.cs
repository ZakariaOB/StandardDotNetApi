using System;

namespace StandardApi.Contracts.V1.Responses
{
    public class MessageResponse
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public string Tags { get; set; }
    }
}
