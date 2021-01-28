namespace CodinGame.Puzzles.Medium.ElementaryCellularAutomaton
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Solution
    {
        private static string GetEvolution(string pattern, Dictionary<string, char> neighborhoods)
        {
            var result = new StringBuilder();

            var neighborhoodFirstKey = pattern.Last() + string.Concat(pattern.Skip(0).Take(2));
            result.Append(neighborhoods[neighborhoodFirstKey].ToString());

            for (int i = 1; i < pattern.Length - 1; i++)
            {
                var neighborhoodKey = string.Concat(pattern.Skip(i - 1).Take(3));
                result.Append(neighborhoods[neighborhoodKey].ToString());
            }

            var neighborhoodLastKey = string.Concat(pattern.Skip(pattern.Length - 2).Take(2)) + pattern.First();
            result.Append(neighborhoods[neighborhoodLastKey].ToString());

            return result.ToString();
        }

        public static void Main(string[] args)
        {
            var wolframCode = Convert.ToString(int.Parse(Console.ReadLine()), 2).Replace('0', '.').Replace('1', '@').PadLeft(8, '.');
            var numberOfEvolution = int.Parse(Console.ReadLine());
            var pattern = Console.ReadLine();

            var neighborhoods = new Dictionary<string, char>
        {
            { "@@@", wolframCode.ElementAt(0) },
            { "@@.", wolframCode.ElementAt(1) },
            { "@.@", wolframCode.ElementAt(2) },
            { "@..", wolframCode.ElementAt(3) },
            { ".@@", wolframCode.ElementAt(4) },
            { ".@.", wolframCode.ElementAt(5) },
            { "..@", wolframCode.ElementAt(6) },
            { "...", wolframCode.ElementAt(7) }
        };

            for (int i = 0; i < numberOfEvolution; i++)
            {
                Console.WriteLine(pattern);
                pattern = GetEvolution(pattern, neighborhoods);
            }
        }
    }
}