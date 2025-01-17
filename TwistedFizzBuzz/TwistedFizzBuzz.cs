using System.Text.Json;

namespace TwistedFizzBuzzLibrary
{
    public class TwistedFizzBuzz
    {
        //Accept user input for a range of numbers and returns their FizzBuzz output
        public static void StandardFizzBuzz(int init, int final)
        {
            var range = fixRange(init, final);
            init = range[0];
            final = range[1];
            for(int i = init; i <= final; i++)
            {
                GetFizzBuzzValue(i);
            }
        }

        //Accept user input of a non-sequential set of numbers and returns their FizzBuzz output.
        public static void NonSenquentialFIzzBuzz(IEnumerable<int> numberList)
        {
            foreach (var item in numberList)
            {
                GetFizzBuzzValue(item);
            }
        }

        //Accept user input for alternative tokens instead of "Fizz" and "Buzz" and alternative divisors instead of 3 and 5.
        public static void AlternaTiveTokens(Dictionary<string, int> alternativeTokens, int init, int final) {
            var range = fixRange(init, final);
            init = range[0];
            final = range[1];

            for (int i = init; i <= final; i++)
            {
                string result = string.Concat(alternativeTokens.Where(at => i % at.Value == 0).Select(at => at.Key));
                Console.WriteLine(result.Length > 0 ? result : i);
            }
        }

        //Accept user input for API generated tokens provided by https://pie-healthy-swift.glitch.me/
        public static async Task AlternativeTokensByApi(int init, int final)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "TwistedFizzBuzzApp/1.0");
            var response = await client.GetStringAsync("https://pie-healthy-swift.glitch.me/word");
            var data = JsonSerializer.Deserialize<Dictionary<string, object>>(response);

            if (data != null && data.ContainsKey("word") && data.ContainsKey("number"))
            {
                string word = data["word"].ToString();
                int number = int.Parse(data["number"].ToString());
                var alternativeTokens  = new Dictionary<string, int>()
                {
                    { word, number }
                };
                AlternaTiveTokens(alternativeTokens, init, final);
            }
        }

        //Return a FizzBuz Token or the original number
        private static void GetFizzBuzzValue(int number)
        {
            if (number % 3 == 0 && number % 5 == 0)
            {
                Console.WriteLine("FizzBuzz");
            }
            else if (number % 3 == 0)
            {
                Console.WriteLine("Fizz");
            }
            else if (number % 5 == 0)
            {
                Console.WriteLine("Buzz");
            }
            else
            {
                Console.WriteLine(number);
            }
        }

        private static IList<int> fixRange(int start, int end)
        {
            var range = new List<int>() { start, end};
            range.Sort();
            return range;
        }
    }
}