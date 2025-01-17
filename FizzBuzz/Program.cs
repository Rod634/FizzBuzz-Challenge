using Infrastructure;
using Microsoft.Extensions.DependencyInjection;

using TwistedFizzBuzzLibrary.interfaces;

namespace FizzBuzz
{
    class Program
    {
        private static ITwistedFizzBuzz _twistedFizzBuzzLibrary;
        static async Task Main(string[] args)
        {
            var serviceProvider = DependencyInjection.ConfigureServices();
            _twistedFizzBuzzLibrary = serviceProvider.GetService<ITwistedFizzBuzz>();

            //RangeFizzBuzz();
            //FizzBuzzOfAList();
            //AlternativeTokens();
            await AlternativeTokensByApiAsync();
        }

        //1.1
        private static void RangeFizzBuzz()
        {
            var response = _twistedFizzBuzzLibrary.StandardFizzBuzz(1, 50);
            outPutTokens(response);
        }

        //1.2
        private static void FizzBuzzOfAList()
        {
            var list = new List<int>() { -5, 6, 300, 12, 15 };
            var response = _twistedFizzBuzzLibrary.NonSenquentialFIzzBuzz(list);
            outPutTokens(response);
        }

        //1.3
        private static void AlternativeTokens()
        {
            var dict = new Dictionary<string, int>()
            {
                { "Poem", 7 },
                { "Writer", 17 },
                { "College", 21 }

            };
            var response = _twistedFizzBuzzLibrary.AlternaTiveTokens(dict, 1, 100);
            outPutTokens(response);
        }

        //1.4
        private static async Task AlternativeTokensByApiAsync()
        {
            var response = await _twistedFizzBuzzLibrary.AlternativeTokensByApi(1, 100);
            outPutTokens(response);
        }

        private static void outPutTokens(IList<string> tokens)
        {
            foreach (var token in tokens)
            {
                Console.WriteLine(token);
            }
        }
    }
}
