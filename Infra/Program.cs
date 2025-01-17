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
                // Registra o HttpClient corretamente, gerenciando sua vida útil de forma correta
                .AddHttpClient()  // Fábrica HttpClient, garantindo o compartilhamento correto
                .AddSingleton<ITwistedFizzBuzz, TwistedFizzBuzz>()  // Registra o serviço ITwistedFizzBuzz
                .BuildServiceProvider();  // Cria e retorna o ServiceProvider
        }
    }
}
