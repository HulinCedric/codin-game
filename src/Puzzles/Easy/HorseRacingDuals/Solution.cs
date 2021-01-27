namespace CodinGame.Puzzles.Easy.HorseRacingDuals
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    class Solution
    {
        static void Main(string[] args)
        {
            int N = int.Parse(Console.ReadLine());
            var puissances = new List<int>();
            for (int i = 0; i < N; i++)
            {
                puissances.Add(int.Parse(Console.ReadLine()));
            }

            var pp = puissances.OrderByDescending(x => x).ToArray();

            var result = int.MaxValue;
            for (int i = 0; i < pp.Length; i++)
            {
                if (i + 1 < pp.Length)
                {
                    var current = pp[i] - pp[i + 1];
                    if (current < result)
                        result = current;
                }
            }

            Console.WriteLine(result);
        }
    }
}