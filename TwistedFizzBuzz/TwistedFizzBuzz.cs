using System.Linq;

namespace TwistedFizzBuzzLibrary
{
    public class TwistedFizzBuzz
    {
        public static void StandardFizzBuzz(int init, int final)
        {
            var range = fixRange(init, final);
            init = range[0];
            final = range[1];
            while (init < final)
            {
                GetFizzBuzzValue(init);

                init++;
            }
        }

        public static void NoSenquentialFIzzBuzz(IEnumerable<int> numberList)
        {
            foreach (var item in numberList)
            {
                GetFizzBuzzValue(item);
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