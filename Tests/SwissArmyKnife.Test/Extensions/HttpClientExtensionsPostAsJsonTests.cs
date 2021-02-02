using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using SCM.SwissArmyKnife.Extensions;
using Xunit;

namespace ScadaMinds.SwissArmyKnife.Tests
{

    public class HttpClientExtensionsPostAsJsonTests
    {
        [Fact]
        public async Task PostAsJson_ShouldReturnAsJson()
        {
            // Arrange
            var originalDictionary = new Dictionary<string, string>
            {
                {"foo", "bar"}
            };
            var json = JsonConvert.SerializeObject(originalDictionary);

            var handlerMock = HttpMessageHandlerMock(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(json)
            });

            var client = new HttpClient(handlerMock.Object);

            // Act
            var dictionaryFromClient = await client.PostAsJsonAsync<Dictionary<string, string>>("http://doesntmatter.com");

            // Assert
            dictionaryFromClient.Should().BeEquivalentTo(originalDictionary);
        }

        [Fact]
        public void PostAsJson_ShouldThrowError_WithBodyInIt_OnNonSuccessfulHttpCode()
        {
            // Arrange
            var errorResponse = "some error response";

            var handlerMock = HttpMessageHandlerMock(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NotFound,
                Content = new StringContent(errorResponse)
            });

            var client = new HttpClient(handlerMock.Object);

            // Act
            Func<Task> action = async () => await client.PostAsJsonAsync<Dictionary<string, string>>("http://doesntmatter.com");

            // Assert
            action.Should().Throw<HttpRequestException>().WithMessage($"*{errorResponse}*");
        }

        [Fact]
        public void PostAsJson_ShouldThrowError_WithBodyInIt_OnJsonParseException()
        {
            // Arrange
            var serverResponse = "{invalid json}";

            var handlerMock = HttpMessageHandlerMock(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(serverResponse)
            });

            var client = new HttpClient(handlerMock.Object);

            // Act
            Func<Task> action = async () => await client.PostAsJsonAsync<Dictionary<string, string>>("http://doesntmatter.com");

            // Assert
            action.Should().Throw<JsonException>().WithMessage($"*{serverResponse}*");
        }

        [Fact]
        public void PostAsJson_ShouldThrowError_WithTruncatedBodyInIt_OnNonSuccessfulHttpCode()
        {
            // Arrange
            var errorResponse = "123456789";

            var handlerMock = HttpMessageHandlerMock(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NotFound,
                Content = new StringContent(errorResponse)
            });

            var client = new HttpClient(handlerMock.Object);

            // Act
            Func<Task> action = async () => await client.PostAsJsonAsync<Dictionary<string, string>>("http://doesntmatter.com", null, 5);

            // Assert
            // Error body only contains 12345 and then a quote '
            action.Should().Throw<HttpRequestException>().WithMessage($"*12345...*");
        }


        // from https://gingter.org/2018/07/26/how-to-mock-httpclient-in-your-net-c-unit-tests/
        private static Mock<HttpMessageHandler> HttpMessageHandlerMock(HttpResponseMessage response)
        {
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                // Setup the PROTECTED method to mock
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    //TODO here we do no validation that the posted content is correct
                    //But we probably don't need to.
                    ItExpr.Is<HttpRequestMessage>(message =>
                        message.Method == HttpMethod.Post
                    ),
                    ItExpr.IsAny<CancellationToken>()
                )
                // prepare the expected response of the mocked http call
                .ReturnsAsync(response)
                .Verifiable();
            return handlerMock;
        }
    }
}
