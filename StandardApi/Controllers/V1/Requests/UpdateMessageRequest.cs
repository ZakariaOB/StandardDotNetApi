using Microsoft.AspNetCore.Mvc;

namespace StandardApi.Controllers.V1.Requests
{
    public class UpdateMessageRequest
    {
        [FromQuery]
        public string Text { get; set; }
    }
}
