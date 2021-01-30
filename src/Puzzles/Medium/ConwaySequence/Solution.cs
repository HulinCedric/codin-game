namespace CodinGame.Puzzles.Medium.ConwaySequence
{
    using System;
    using System.Linq;

    public class Solution
    {
        public static string GetDescription(string valuesString)
        {
            var values = valuesString.Split(' ').Select(x => int.Parse(x)).ToList();
            var lastVal = values.First();
            var count = 0;
            var result = string.Empty;

            for (int i = 0; i < values.Count; i++)
            {
                if (lastVal != values[i])
                {
                    result += count + " " + lastVal + " ";
                    count = 1;
                    lastVal = values[i];
                }
                else count++;

                if (i + 1 == values.Count)
                {
                    result += count + " " + values[i];
                }
            }

            return result.Trim();
        }

        public static void Main(string[] args)
        {
            var R = Console.ReadLine();
            int L = int.Parse(Console.ReadLine());
            for (int i = 0; i < L - 1; i++)
            {
                R = GetDescription(R);
            }

            Console.WriteLine(R);
        }
    }
}