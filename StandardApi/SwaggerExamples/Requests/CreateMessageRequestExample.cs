using StandardApi.Contracts.V1.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace StandardApi.SwaggerExamples.Requests
{
    public class CreateMessageRequestExample : IExamplesProvider<CreateMessageRequest>
    {
        public CreateMessageRequest GetExamples()
        {
            return new CreateMessageRequest
            {
                Text = "Hello as TEXT"
            };
        }
    }
}
