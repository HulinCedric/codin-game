namespace CodinGame.Puzzles.Medium.QueneauNumbers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Solution
    {
        public static IEnumerable<int> GetSpiralSequence(IEnumerable<int> sequence)
        {
            var workingSequence = sequence.ToList();
            while (workingSequence.Count() > 0)
            {
                var result = workingSequence.Last();
                workingSequence.Remove(workingSequence.Last());
                workingSequence.Reverse();
                yield return result;
            }
        }

        private static void Main(string[] args)
        {
            var startSequence = Enumerable.Range(1, int.Parse(Console.ReadLine()));
            var results = new List<string>();

            var workingSequence = startSequence;
            for (int i = 0; i < startSequence.Count(); i++)
            {
                workingSequence = GetSpiralSequence(workingSequence);
                results.Add(string.Join(",", workingSequence));
            }

            if (results.Last() != string.Join(",", startSequence))
            {
                Console.WriteLine("IMPOSSIBLE");
            }
            else
            {
                foreach (var item in results)
                {
                    Console.WriteLine(item);
                }
            }
        }
    }
}