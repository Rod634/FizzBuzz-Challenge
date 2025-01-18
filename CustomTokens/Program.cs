using Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using TwistedFizzBuzzLibrary.interfaces;

namespace CustomTokens
{
    class Program
    {
        //3
        static void Main(string[] args)
        {
            var serviceProvider = DependencyInjection.ConfigureServices();
            var twistedFizzBuzzLibrary = serviceProvider.GetService<ITwistedFizzBuzz>();

            var customTokens = new Dictionary<string, int>()
            {
                { "Fizz", 5 },
                { "Buzz", 9},
                { "Bar", 27}
            };

            var tokens = twistedFizzBuzzLibrary.AlternaTiveTokens(customTokens, -20, 127);
            foreach (var token in tokens)
            {
                Console.WriteLine(token);
            }
        }
    }
}