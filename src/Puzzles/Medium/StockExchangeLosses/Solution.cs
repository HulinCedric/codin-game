namespace CodinGame.Puzzles.Medium.StockExchangeLosses
{
    using System;
    using System.Linq;

    public class Solution
    {
        public static void Main(string[] args)
        {
            Console.ReadLine();

            var inputs = Console.ReadLine().Split(' ').Select(x => int.Parse(x));
            var max = -1;
            var min = inputs.First();
            var result = 0;

            foreach (var n in inputs)
            {
                if (n >= max)
                {
                    max = n;
                    min = max;
                    continue;
                }

                if (n <= min)
                    min = n;

                if (max - min > result)
                    result = max - min;
            }

            Console.WriteLine(result * -1);
        }
    }
}