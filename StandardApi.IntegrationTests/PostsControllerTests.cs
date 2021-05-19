using FluentAssertions;
using StandardApi.Contracts;
using StandardApi.Contracts.V1.Requests;
using StandardApi.Domain;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace StandardApi.IntegrationTests
{
    public class PostsControllerTests : IntegrationTest
    {
        [Fact]
        public async Task GetAll_WithtouAnyMessage_Returns_EmptyResponse()
        {
            // Arrange
            await AuthenticateAsync();

            // Act
            var response = await _client.GetAsync(ApiRoutes.Messages.GetAll);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var data = await response.Content.ReadAsAsync<List<Message>>();

            data.Should().BeEmpty();
        }

        [Fact]
        public async Task Get_ReturnsMessage_WhenMessageExistsInDatabase()
        {
            // Arrange
            await AuthenticateAsync();

            // Act
            var createdPost = await CreateMessageAsync(new CreateMessageRequest { Text = "Test post" });

            var response = await _client.GetAsync(ApiRoutes.Messages.Get.Replace("{messageId}", createdPost.Id.ToString()));

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var returnedMessage = await response.Content.ReadAsAsync<Message>();

            returnedMessage.Id.Should().Be(returnedMessage.Id);

            returnedMessage.Text.Should().Be("Test post");
        }
    }
}
