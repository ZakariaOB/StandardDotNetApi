using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StandardApi.Contracts;
using StandardApi.Contracts.V1.Requests;
using StandardApi.Contracts.V1.Responses;
using StandardApi.Controllers.V1;
using StandardApi.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace StandardApi.IntegrationTests
{
    public class IntegrationTest
    {
        protected readonly HttpClient _client;

        protected IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>()
                            .WithWebHostBuilder(builder => 
                            {
                                builder.ConfigureServices(services =>
                                {
                                    var descriptor = services.SingleOrDefault(
                                            d => d.ServiceType ==
                                                typeof(DbContextOptions<DataContext>));
                                    services.Remove(descriptor);
                                    services.AddDbContext<DataContext>(options => 
                                    {
                                        options.UseInMemoryDatabase("TestDb");
                                    });
                                });
                            });
            _client = appFactory.CreateClient();
        }

        protected async Task AuthenticateAsync()
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync());
        }

        protected async Task<MessageResponse> CreateMessageAsync(CreateMessageRequest request)
        {
            var response = await _client.PostAsJsonAsync(ApiRoutes.Messages.Create, request);
            return await response.Content.ReadAsAsync<MessageResponse>();
        }

        private async Task<string> GetJwtAsync() 
        {
            var response = await _client.PostAsJsonAsync<UserRegistrationRequest>(ApiRoutes.Identity.Register, new UserRegistrationRequest
            {
                Email = "pzOmp@gvg.com",
                Password = "SomePVass!@23"
            });

            var registrationResponse = await response.Content.ReadAsAsync<AuthSuccessResponse>();
            return registrationResponse.Token;
        }
    }
}
