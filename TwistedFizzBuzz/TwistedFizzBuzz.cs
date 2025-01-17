using System.Text.Json;

namespace TwistedFizzBuzzLibrary
{
    public class TwistedFizzBuzz
    {
        //Accept user input for a range of numbers and returns their FizzBuzz output
        public static void StandardFizzBuzz(int init, int final)
        {
            (init, final) = FixRange(init, final);

            for (int i = init; i <= final; i++)
            {
                GetFizzBuzzValue(i);
            }
        }

        //Accept user input of a non-sequential set of numbers and returns their FizzBuzz output.
        public static void NonSenquentialFIzzBuzz(IEnumerable<int> numberList)
        {
            if(!numberList.Any()) {
                throw new ArgumentException("The list can't be empty");
            }

            foreach (var item in numberList)
            {
                GetFizzBuzzValue(item);
            }
        }

        //Accept user input for alternative tokens instead of "Fizz" and "Buzz" and alternative divisors instead of 3 and 5.
        public static void AlternaTiveTokens(Dictionary<string, int> alternativeTokens, int init, int final) {

            if (!alternativeTokens.Any())
            {
                throw new ArgumentException("The AlternativeTokens can't be empty");
            }

            (init, final) = FixRange(init, final);

            for (int i = init; i <= final; i++)
            {
                string result = string.Concat(alternativeTokens.Where(at => i % at.Value == 0).Select(at => at.Key));
                Console.WriteLine(result.Length > 0 ? result : i);
            }
        }

        //Accept user input for API generated tokens provided by https://pie-healthy-swift.glitch.me/
        public static async Task AlternativeTokensByApi(int init, int final)
        {
            try
            {
                var alternativeTokens = await FetchAlternativeTokensFromApi();
                AlternaTiveTokens(alternativeTokens, init, final);
            }
            catch (Exception ex)
            {
                // Repropaga a exceção para ser tratada em um nível superior
                throw new ApplicationException("Error processing alternative tokens", ex);
            }
        }

        private static async Task<Dictionary<string, int>> FetchAlternativeTokensFromApi()
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "TwistedFizzBuzzApp/1.0");

            try
            {
                string response = await client.GetStringAsync("https://pie-healthy-swift.glitch.me/word");
                var data = JsonSerializer.Deserialize<Dictionary<string, object>>(response);

                if (data == null || !data.ContainsKey("word") || !data.ContainsKey("number"))
                {
                    throw new FormatException("API Response format not valid");
                }

                string word = data["word"].ToString();
                int number = int.Parse(data["number"].ToString());

                return new Dictionary<string, int> { { word, number } };
            }
            catch (Exception ex)
            {
                throw new Exception("Fetch data API error", ex);
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

        private static (int Start, int End) FixRange(int start, int end)
        {
            return start > end ? (end, start) : (start, end);
        }
    }
}