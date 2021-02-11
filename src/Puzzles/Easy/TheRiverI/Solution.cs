namespace CodinGame.Puzzles.Easy.TheRiverI
{
    using System;
    using System.Linq;

    public static class Solution
    {
        public static void Main(string[] args)
            => Console.WriteLine(
                GetMeetingPoint(
                    long.Parse(Console.ReadLine()),
                    long.Parse(Console.ReadLine())));

        private static long GetMeetingPoint(long firstRiver, long secondRiver)
        {
            while (firstRiver != secondRiver)
            {
                if (firstRiver < secondRiver)
                {
                    firstRiver = GetNextRiverNumber(firstRiver);
                }
                else
                {
                    secondRiver = GetNextRiverNumber(secondRiver);
                }
            }

            return firstRiver;
        }

        private static long GetNextRiverNumber(long riverNumber)
            => riverNumber + GetSumOfDigits(riverNumber);

        private static long GetSumOfDigits(long riverNumber)
            => riverNumber.ToString().Sum(r => r - '0');
    }
}