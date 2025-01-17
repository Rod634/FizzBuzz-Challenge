using TwistedFizzBuzzLibrary;

namespace FizzBuzzTests
{
    public class FizzBuzzTests
    {
        [Fact]
        public void FizzBuzzStandard_ShouldReturnCorrectOutput()
        {
            var result = TwistedFizzBuzz.StandardFizzBuzz(1, 5);
            Assert.Equal(new List<string> { "1", "2", "Fizz", "4", "Buzz" }, result);
        }
    }
}