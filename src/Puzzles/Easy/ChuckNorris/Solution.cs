namespace CodinGame.Puzzles.Easy.ChuckNorris
{
    using System;
    using System.Linq;
    using System.Text;

    class Solution
    {
        static void Main(string[] args)
        {
            string message = Console.ReadLine();

            var binaryMessage = Encoding.ASCII.GetBytes(message)
                                               .Select(c => Convert.ToString(c, 2).PadLeft(7, '0'))
                                               .Aggregate((current, next) => current + next);

            var n = 0;
            var temp = binaryMessage.ToCharArray().First();

            foreach (var charac in binaryMessage.ToCharArray())
            {
                if (charac == temp)
                {
                    n++;
                }
                else
                {
                    Write(temp, n);

                    Console.Write(" ");

                    temp = charac;
                    n = 1;
                }
            }

            Write(temp, n);
        }

        static void Write(char c, int n)
        {
            if (c == '1')
            {
                Console.Write("0 ");
            }
            else
            {
                Console.Write("00 ");
            }

            for (int i = 0; i < n; i++)
            {
                Console.Write("0");
            }
        }
    }
}