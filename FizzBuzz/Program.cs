using TwistedFizzBuzzLibrary;

namespace FizzBuzz
{
    class Program
    {
        static void Main(string[] args)
        {
            //RangeFizzBuzz();
            FizzBuzzOfAList();
        }

        private static void RangeFizzBuzz()
        {
            Console.WriteLine("Enter the first number:");
            int initial = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter the final range number:");
            int final = int.Parse(Console.ReadLine());

            TwistedFizzBuzz.StandardFizzBuzz(initial, final);
        }

        private static void FizzBuzzOfAList()
        {
            var list = new List<int>() { -5, 6, 300, 12, 15 };
            TwistedFizzBuzz.NoSenquentialFIzzBuzz(list);
        }
    }
}
