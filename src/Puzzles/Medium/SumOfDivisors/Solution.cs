namespace CodinGame.Puzzles.Medium.SumOfDivisors
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class Solution
    {
        public static void Main(string[] args)
        {
            long n = long.Parse(Console.ReadLine());

            long sum = 0;
            for (long i = n - 1; i >= 0; i--)
            {
                sum += GetDivisors(n--).Sum();
            }

            Console.WriteLine(sum);
        }

        public static IEnumerable<long> GetDivisors(long n)
        {
            var result = new List<long>();

            for (long i = 1; i <= Math.Sqrt(n) + 1; i++)
            {
                if (n % i == 0)
                {
                    // If divisors are equal, print only one
                    if (n / i == i)
                    {
                        result.Add(i);
                    }
                    else // Otherwise print both
                    {
                        result.Add(i);
                        result.Add(n / i);
                    }
                }
            }
            return result.Distinct();
        }
    }
}