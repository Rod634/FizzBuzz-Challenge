using Moq;
using Moq.Protected;
using System.Net;
using System.Text.Json;
using TwistedFizzBuzzLibrary.interfaces;
using Xunit;

namespace FizzBuzzTests
{
    public class FizzBuzzTests
    {
        private readonly Mock<ITwistedFizzBuzz> _twistedFizzBuzzMock;
        private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
        private readonly HttpClient _mockHttpClient;

        public FizzBuzzTests()
        {
            _twistedFizzBuzzMock = new Mock<ITwistedFizzBuzz>();
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            _mockHttpClient = new HttpClient(_mockHttpMessageHandler.Object);
        }

        [Theory]
        [InlineData(1, 5, new[] { "1", "2", "Fizz", "4", "Buzz" })]
        [InlineData(-3, 3, new[] { "Fizz", "-2", "-1", "FizzBuzz", "1", "2", "Fizz" })]
        [InlineData(10, 15, new[] { "Buzz", "11", "Fizz", "13", "14", "FizzBuzz" })]
        public void FizzBuzzStandard_ShouldReturnCorrectOutput(int init, int final, string[] expected)
        {
            _twistedFizzBuzzMock
                .Setup(service => service.StandardFizzBuzz(init, final))
                .Returns(expected);

            var result = _twistedFizzBuzzMock.Object.StandardFizzBuzz(init, final);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(new[] { 3, 5, 15 }, new[] { "Fizz", "Buzz", "FizzBuzz" })]
        [InlineData(new[] { -6, -10, 7 }, new[] { "Fizz", "Buzz", "7" })]
        [InlineData(new[] { 1, 2, 3, 4, 5 }, new[] { "1", "2", "Fizz", "4", "Buzz" })]
        public void FizzBuzzNonSequential_ShouldReturnCorrectOutput(int[] numberList, string[] expected)
        {
            _twistedFizzBuzzMock
                .Setup(service => service.NonSenquentialFIzzBuzz(It.IsAny<IEnumerable<int>>()))
                .Returns(expected);

            var result = _twistedFizzBuzzMock.Object.NonSenquentialFIzzBuzz(numberList);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void FizzBuzzNonSequential_ShouldThrowException()
        {
            var emptyList = new List<int>();
            _twistedFizzBuzzMock
                .Setup(service => service.NonSenquentialFIzzBuzz(It.IsAny<IEnumerable<int>>()))
                .Throws<ArgumentException>();

            Assert.Throws<ArgumentException>(() => _twistedFizzBuzzMock.Object.NonSenquentialFIzzBuzz(emptyList));
        }

        [Theory]
        [InlineData(1, 5, "Fizz", 3, "Buzz", 5, new string[] { "1", "2", "Fizz", "4", "Buzz" })]
        [InlineData(10, 15, "X", 2, "Y", 5, new string[] { "XY", "11", "X", "13", "X", "Y" })]
        public void AlternaTiveTokens_ShouldReturnCorrectOutput(int init, int final, string token1, int number1, string token2, int number2, string[] expected)
        {
            var alternativeTokens = new Dictionary<string, int>
            {
                { token1, number1 },
                { token2, number2 }
            };

            _twistedFizzBuzzMock
                .Setup(service => service.AlternaTiveTokens(It.IsAny<Dictionary<string, int>>(), init, final))
                .Returns(expected);

            var result = _twistedFizzBuzzMock.Object.AlternaTiveTokens(alternativeTokens, init, final);

            Assert.Equal(expected, result);
        }
        
        [Fact]
        public void AlternaTiveTokens_ShouldThrowException()
        {
            var emptyList = new Dictionary<string, int>();
            _twistedFizzBuzzMock
                .Setup(service => service.AlternaTiveTokens(It.IsAny<Dictionary<string, int>>(), 1, 10))
                .Throws<ArgumentException>();

            Assert.Throws<ArgumentException>(() => _twistedFizzBuzzMock.Object.AlternaTiveTokens(emptyList, 1, 10));
        }

        [Fact]
        public async Task AlternativeTokensByApi_ShouldReturnCorrectOutput()
        {
            var apiResponse = new Dictionary<string, object>
            {
                { "word", "Fizz" },
                { "number", 3 }
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


            var expected = new List<string> { "1", "2", "Fizz", "4", "5" };
            _twistedFizzBuzzMock
                .Setup(service => service.AlternaTiveTokens(It.IsAny<Dictionary<string, int>>(), 1, 5))
                .Returns(expected);


            var service = new TwistedFizzBuzzLibrary.TwistedFizzBuzz(_mockHttpClient);
            var result = await service.AlternativeTokensByApi(1, 5);

            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task AlternativeTokensByApi_ShouldThrowException_WhenApiFails()
        {
            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.InternalServerError));

            var service = new TwistedFizzBuzzLibrary.TwistedFizzBuzz(_mockHttpClient);

            await Assert.ThrowsAsync<ApplicationException>(() => service.AlternativeTokensByApi(1, 5));
        }


    }
}