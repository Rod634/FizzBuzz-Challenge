using FizzBuzz.Services.Interfaces;
using System.Text.Json;
using TwistedFizzBuzzLibrary.interfaces;

namespace TwistedFizzBuzzLibrary
{
    public class TwistedFizzBuzz : ITwistedFizzBuzz
    {
        private readonly IGlitchService _glitchService;

        public TwistedFizzBuzz(IGlitchService glitchService)
        {
            _glitchService = glitchService;
        }

        //Accept user input for a range of numbers and returns their FizzBuzz output
        public IList<string> StandardFizzBuzz(int init, int final)
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
        public IList<string> NonSenquentialFIzzBuzz(IEnumerable<int> numberList)
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
        public IList<string> AlternaTiveTokens(Dictionary<string, int> alternativeTokens, int init, int final) 
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
        public async Task<IList<string>> AlternativeTokensByApi(int init, int final)
        {
            try
            {
                var alternativeTokens = await _glitchService.FetchAlternativeTokensFromApi();
                var alternatveTokensList = AlternaTiveTokens(alternativeTokens, init, final);

                return alternatveTokensList;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error processing alternative tokens", ex);
            }
        }

        //Return a FizzBuz Token or the original number
        private string GetFizzBuzzValue(int number)
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

        private (int Start, int End) FixRange(int start, int end)
        {
            return start > end ? (end, start) : (start, end);
        }
    }
}