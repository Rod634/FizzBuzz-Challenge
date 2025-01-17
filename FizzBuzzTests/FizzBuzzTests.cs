using Moq;

using TwistedFizzBuzzLibrary.interfaces;
using Xunit;

namespace FizzBuzzTests
{
    public class FizzBuzzTests
    {
        private readonly Mock<ITwistedFizzBuzz> _twistedFizzBuzzMock;

        public FizzBuzzTests()
        {
            _twistedFizzBuzzMock = new Mock<ITwistedFizzBuzz>();
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

    }
}