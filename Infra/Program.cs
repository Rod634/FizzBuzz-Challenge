using FizzBuzz.Services;
using FizzBuzz.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using TwistedFizzBuzzLibrary;
using TwistedFizzBuzzLibrary.interfaces;

namespace Infrastructure
{
    public class DependencyInjection
    {
        // Dependency Injection
        public static ServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddHttpClient()  
                .AddSingleton<ITwistedFizzBuzz, TwistedFizzBuzz>()  
                .AddSingleton<IGlitchService, GlitchService>()
                .BuildServiceProvider();
        }
    }
}
