using TwistedFizzBuzzLibrary;

namespace FizzBuzz
{
    class Program
    {
        static void Main(string[] args)
        {
            RangeFizzBuzz();
        }

        private static void RangeFizzBuzz()
        {
            Console.WriteLine("Enter the first number:");
            int initial = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter the final range number:");
            int range = int.Parse(Console.ReadLine());

            TwistedFizzBuzz.StandardFizzBuzz(initial, range);
        }
    }
}
