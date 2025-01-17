﻿using System.Collections.Generic;
using System.Text.Json;

namespace TwistedFizzBuzzLibrary
{
    public class TwistedFizzBuzz
    {
        //Accept user input for a range of numbers and returns their FizzBuzz output
        public static IList<string> StandardFizzBuzz(int init, int final)
        {
            (init, final) = FixRange(init, final);
            var fizzBuzzList = new List<string>();

            for (int i = init; i <= final; i++)
            {
                var value = GetFizzBuzzValue(i);
                fizzBuzzList.Add(value);
            }

            return fizzBuzzList;
        }

        //Accept user input of a non-sequential set of numbers and returns their FizzBuzz output.
        public static IList<string> NonSenquentialFIzzBuzz(IEnumerable<int> numberList)
        {
            var fizzBuzzList = new List<string>();

            if (!numberList.Any()) {
                throw new ArgumentException("The list can't be empty");
            }

            foreach (var item in numberList)
            {
                var value = GetFizzBuzzValue(item);
                fizzBuzzList.Add(value);
            }

            return fizzBuzzList;
        }

        //Accept user input for alternative tokens instead of "Fizz" and "Buzz" and alternative divisors instead of 3 and 5.
        public static IList<string> AlternaTiveTokens(Dictionary<string, int> alternativeTokens, int init, int final) 
        {
            var alternatveTokensList = new List<string>();

            if (!alternativeTokens.Any())
            {
                throw new ArgumentException("The AlternativeTokens can't be empty");
            }

            (init, final) = FixRange(init, final);

            for (int i = init; i <= final; i++)
            {
                string result = string.Concat(alternativeTokens.Where(at => i % at.Value == 0).Select(at => at.Key));
                result = result.Length > 0 ? result : i.ToString();
                alternatveTokensList.Add(result);
            }

            return alternatveTokensList;
        }

        //Accept user input for API generated tokens provided by https://pie-healthy-swift.glitch.me/
        public static async Task<IList<string>> AlternativeTokensByApi(int init, int final)
        {
            try
            {
                var alternativeTokens = await FetchAlternativeTokensFromApi();
                var alternatveTokensList = AlternaTiveTokens(alternativeTokens, init, final);

                return alternatveTokensList;
            }
            catch (Exception ex)
            {
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
        private static string GetFizzBuzzValue(int number)
        {
            if (number % 3 == 0 && number % 5 == 0)
            {
                return "FizzBuzz";
            }
            
            if (number % 3 == 0)
            {
                return "Fizz";
            }
            if (number % 5 == 0)
            {
                return "Buzz";
            }

            return number.ToString();
        }

        private static (int Start, int End) FixRange(int start, int end)
        {
            return start > end ? (end, start) : (start, end);
        }

        public static void outPutTokens(IList<string> tokens)
        {
            foreach(var token in tokens)
            {
                Console.WriteLine(token);
            }
        }
    }
}