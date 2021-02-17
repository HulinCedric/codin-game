namespace CodinGame.Puzzles.Easy.CreateTheLongestSequenceOf1s
{
    using System;
    using System.Linq;

    public class Solution
    {
        public static void Main(string[] args)
        {
            var sequenceOfOneLengths = Console.ReadLine()
                .Split("0")
                .Select(s => s.Length);

            var longestSequenceOfOneLength = sequenceOfOneLengths
                .Zip(sequenceOfOneLengths.Skip(1), (first, second) => (first + second) + 1)
                .Max();

            Console.WriteLine(longestSequenceOfOneLength);
        }
    }
}