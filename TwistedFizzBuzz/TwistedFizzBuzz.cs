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
            while (init <= final)
            {
                GetFizzBuzzValue(init);
                init++;
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
        public static void AlternaTiveTokens(Dictionary<int, string> alternativeTokens, int init, int final) {
            var range = fixRange(init, final);
            init = range[0];
            final = range[1];
            
            while(init <= final)
            {
                string result = string.Concat(alternativeTokens.Where(at => init % at.Key == 0).Select(at => at.Value));
                if(result.Length > 0 )
                {
                    Console.WriteLine(result);
                }
                else
                {
                    Console.WriteLine(init);
                }

                init++;
            }
        }

        private static void GetFizzBuzzValue(int number)
        {
            if (number % 3 == 0 && number % 5 == 0)
            {
                Console.WriteLine(Constants.FIZZ + Constants.BUZZ);
            }
            else if (number % 3 == 0)
            {
                Console.WriteLine(Constants.FIZZ);
            }
            else if (number % 5 == 0)
            {
                Console.WriteLine(Constants.BUZZ);
            }
            else
            {
                Console.WriteLine(number.ToString());
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