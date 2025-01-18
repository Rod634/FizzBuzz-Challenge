using FizzBuzz.Services;
using Moq;
using Moq.Protected;
using System.Net;
using System.Text.Json;
using Xunit;

namespace FizzBuzz.FizzBuzzTests
{
    public class GlitchServiceTests
    {
        private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
        private readonly HttpClient _mockHttpClient;
        private readonly GlitchService _glitchService;


        public GlitchServiceTests()
        {
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            _mockHttpClient = new HttpClient(_mockHttpMessageHandler.Object);
            _glitchService = new GlitchService(_mockHttpClient);
        }


        [Fact]
        public async Task FetchAlternativeTokensFromApi_ShouldReturnCorrectOutput()
        {
            var apiResponse = new Dictionary<string, object>
            {
                { "word", "zap" },
                { "number", 3 }
            };

            var expected = new Dictionary<string, int>
            {
                { "zap", 3 }
            };
            var jsonResponse = JsonSerializer.Serialize(apiResponse);

            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(jsonResponse)
                });

            var result = await _glitchService.FetchAlternativeTokensFromApi();

            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task AlternativeTokensByApi_ShouldThrowFormatException()
        {
            var apiResponse = new Dictionary<string, object>
            {
                { "number", "zap" },
                { "numberTwo", 3 }
            };
            var jsonResponse = JsonSerializer.Serialize(apiResponse);

            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(jsonResponse)
                });

            var service = new GlitchService(_mockHttpClient);

            await Assert.ThrowsAsync<FormatException>(() => service.FetchAlternativeTokensFromApi());
        }

        [Fact]
        public async Task AlternativeTokensByApi_ShouldThrowExeption()
        {
            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.InternalServerError));

            var service = new GlitchService(_mockHttpClient);

            await Assert.ThrowsAsync<Exception>(() => service.FetchAlternativeTokensFromApi());
        }
    }
}
