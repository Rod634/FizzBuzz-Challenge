using TwistedFizzBuzzLibrary;

namespace CustomTokens
{
    class Program
    {
        //3
        static void Main(string[] args)
        {
            var customTokens = new Dictionary<string, int>()
            {
                { "Fizz", 5 },
                { "Buzz", 9},
                { "Bar", 27}
            };

            TwistedFizzBuzz.AlternaTiveTokens(customTokens, -20, 127);
        }
    }
}