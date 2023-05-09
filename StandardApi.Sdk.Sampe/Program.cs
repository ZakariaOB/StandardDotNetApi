using Refit;
using StandardApi.Contracts.V1.Requests;
using System;
using System.Threading.Tasks;

namespace StandardApi.Sdk.Sampe
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var cachedToken = string.Empty;

            var identityApi = RestService.For<IIdentityApi>(hostUrl: "https://localhost:5001");
            var messageApi = RestService.For<IMessageApi>(hostUrl: "https://localhost:5001", new RefitSettings 
            { 
                AuthorizationHeaderValueGetter = () => Task.FromResult(cachedToken)
            });

            var registerResponse = await identityApi.RegisterAsync(new UserRegistrationRequest 
            { 
                Email = "sdkaccount@gmail.com",
                Password = "Test1234!?"
            });
            var loginResponse = await identityApi.LoginAsync(new UserLoginRequest 
            {
                Email = "sdkaccount@gmail.com",
                Password = "Test1234!?"
            });

            cachedToken = loginResponse.Content.Token;


            var allMessages = await messageApi.GetAllMessagesAsync();

            var createdMessage = await messageApi.CreateAsync(new CreateMessageRequest
            {
                Text = "CreatedSdk"
            });

            var retrieveMessage = await messageApi.GetAsync(createdMessage.Content.Id);
        }
    }
}
