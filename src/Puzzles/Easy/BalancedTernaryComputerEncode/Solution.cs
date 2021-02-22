namespace CodinGame.Puzzles.Easy.BalancedTernaryComputerEncode
{
    using System;

    public class Solution
    {
        public static void Main(string[] args)
            => Console.WriteLine(
                BalancedTernaryConverter.ToStringFromBase10(
                    int.Parse(Console.ReadLine())));
    }

    /// <remarks>
    /// Inspired by https://www.xarg.org/puzzle/codingame/balanced-ternary-computer-encode/
    /// </remarks>
    public static class BalancedTernaryConverter
    {
        private const int MaximumBase10Limit = 30_000;

        private static readonly int[] Values = new int[] { 0, 1, -1 };
        private static readonly char[] Representations = new char[] { '0', '1', 'T' };

        public static string ToStringFromBase10(int base10Number)
        {
            var balancedTernaryNumber = string.Empty;
            do
            {
                // Handle negative digits correctly
                var r = (base10Number + MaximumBase10Limit) % 3;

                // Handle carry
                base10Number -= Values[r];

                // Divide for base change
                base10Number = (base10Number / 3) | 0;

                // Select the correct digit and add in reverse order
                balancedTernaryNumber = $"{Representations[r]}{balancedTernaryNumber}";
            } while (base10Number != 0);

            return balancedTernaryNumber;
        }
    }
}