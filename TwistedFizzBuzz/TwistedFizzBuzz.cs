namespace TwistedFizzBuzzLibrary
{
    public class TwistedFizzBuzz
    {
        public static void StandardFizzBuzz(int init, int range)
        {
            while (init < range)
            {
                if(init % 3 == 0 && init % 5 == 0)
                {
                   Console.WriteLine(Constants.FIZZ + Constants.BUZZ);
                }
                else if(init % 3 == 0)
                {
                    Console.WriteLine(Constants.FIZZ);
                }
                else if (init % 5 == 0)
                {
                    Console.WriteLine(Constants.BUZZ);
                }
                else
                {
                    Console.WriteLine(init.ToString());
                }

                init++;
            }
        }
    }
}