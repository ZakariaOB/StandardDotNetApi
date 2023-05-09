using StandardApi.Contracts.V1.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace StandardApi.SwaggerExamples.Responses
{
    public class MessageResponseExample : IExamplesProvider<MessageResponse>
    {
        public MessageResponse GetExamples()
        {
            return new MessageResponse
            {
                Text = "Hello as Response",
                Tags = "#1-TAG1 #2-TAG2 #3-TAG3"
            };
        }
    }
}
