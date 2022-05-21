using System;

namespace StandardApi.Controllers.V1.Requests
{
    public class CreateMessageRequest
    {
        public Guid Id { get; set; }

        public string Text { get; set; }
    }
}
