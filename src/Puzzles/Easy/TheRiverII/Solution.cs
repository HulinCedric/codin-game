namespace CodinGame.Puzzles.Easy.TheRiverII
{
    using System;
    using System.Linq;

    public static class Solution
    {
        public static void Main(string[] args)
            => Console.WriteLine(
                CanBeMetByAnotherRiver(int.Parse(Console.ReadLine())) ?
                "YES" :
                "NO");

        private static bool CanBeMetByAnotherRiver(int river)
            => Enumerable.Range(0, river - 1)
            .Any(anotherRiver => GetNextRiverNumber(anotherRiver) == river);

        private static long GetNextRiverNumber(long riverNumber)
            => riverNumber + GetSumOfDigits(riverNumber);

        private static long GetSumOfDigits(long riverNumber)
            => riverNumber.ToString().Sum(r => r - '0');
    }
}