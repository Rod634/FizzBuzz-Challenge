using TwistedFizzBuzzLibrary;

namespace FizzBuzz
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //RangeFizzBuzz();
            //FizzBuzzOfAList();
            //AlternativeTokens();
            await AlternativeTokensByApiAsync();
        }

        private static void RangeFizzBuzz()
        {
            TwistedFizzBuzz.StandardFizzBuzz(1, 50);
        }

        private static void FizzBuzzOfAList()
        {
            var list = new List<int>() { -5, 6, 300, 12, 15 };
            TwistedFizzBuzz.NonSenquentialFIzzBuzz(list);
        }

        private static void AlternativeTokens()
        {
            var dict = new Dictionary<string, int>()
            {
                { "Poem", 7 },
                { "Writer", 17 },
                { "College", 21 }

            };
            TwistedFizzBuzz.AlternaTiveTokens(dict, 1, 100);
        }

        private static async Task AlternativeTokensByApiAsync()
        {
            await TwistedFizzBuzz.AlternativeTokensByApi(1, 100);
        }
    }
}
