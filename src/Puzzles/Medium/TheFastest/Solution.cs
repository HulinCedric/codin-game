namespace CodinGame.Puzzles.Medium.TheFastest
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    public class Solution
    {
        public static void Main(string[] args)
        {
            var N = int.Parse(Console.ReadLine());
            var list = new List<DateTime>();

            for (int i = 0; i < N; i++)
            {
                list.Add(DateTime.ParseExact(Console.ReadLine(), "HH:mm:ss", CultureInfo.InvariantCulture));
            }
            
            list.Sort();

            Console.WriteLine(list.First().ToString("HH:mm:ss"));
        }
    }
}