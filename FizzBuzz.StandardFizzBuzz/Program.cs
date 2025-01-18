using Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using TwistedFizzBuzzLibrary.interfaces;

namespace StandardFizzBuzz
{
    class Program
    {
        //2
        static void Main(string[] args)
        {
            var serviceProvider = DependencyInjection.ConfigureServices();
            var _twistedFizzBuzzLibrary = serviceProvider.GetService<ITwistedFizzBuzz>();
            var tokens = _twistedFizzBuzzLibrary.StandardFizzBuzz(1, 100);
            foreach (var token in tokens)
            {
                Console.WriteLine(token);
            }
        }
    }
}