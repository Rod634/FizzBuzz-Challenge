using TwistedFizzBuzzLibrary;

namespace FizzBuzz
{
    class Program
    {
        static void Main(string[] args)
        {
            //RangeFizzBuzz();
            //FizzBuzzOfAList();
            //AlternativeTokens();
        }

        private static void RangeFizzBuzz()
        {
            TwistedFizzBuzz.StandardFizzBuzz(1, 100);
        }

        private static void FizzBuzzOfAList()
        {
            var list = new List<int>() { -5, 6, 300, 12, 15 };
            TwistedFizzBuzz.NonSenquentialFIzzBuzz(list);
        }

        private static void AlternativeTokens()
        {
            var dict = new Dictionary<int, string>()
            {
                { 7, "Poem" },
                { 17, "Writer" },
                { 21, "College" }

            };
            TwistedFizzBuzz.AlternaTiveTokens(dict, 1, 100);
        }
    }
}
