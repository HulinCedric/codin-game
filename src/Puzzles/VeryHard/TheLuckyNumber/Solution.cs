namespace CodinGame.Puzzles.VeryHard.TheLuckyNumber
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Numerics;

    /// <remarks>
    /// Inspired by solution from https://github.com/informaticienzero/CodinGame/blob/master/Communautaires/The%20lucky%20number.py
    /// </remarks>
    public class Solution
    {
        /// <summary>Calculate the numbers of lucky numbers from 0 to 10^(max - 1).<summary>
        public static IDictionary<int, long> CalculatePossibleLuckyNumbers(long max)
        {
            var possibilities = new Dictionary<int, long> { { 0, 0 } };

            var index = 1;
            while (index < max)
            {
                possibilities[index] = 2 * (long)(BigInteger.Pow(9, index - 1)) + 8 * possibilities[index - 1];
                index++;
            }

            return possibilities;
        }

        /// <summary>Gets the list of all digits of a number, from left to right.<summary>
        public static List<int> GetDigits(long number)
            => number
            .ToString()
            .Select(c => (int)char.GetNumericValue(c))
            .ToList();

        /// <summary>Count the lucky numbers from 0 to number with a fast mathematic range.<summary>
        public static long CountLuckyNumberTo(long number)
        {
            var digits = GetDigits(number);
            var digitPosition = digits.Count;
            var luckyNumbersByDigitPosition = CalculatePossibleLuckyNumbers(digits.Count);
            var luckyNumbers = 0L;
            var previousLuckyDigit = -1;

            foreach (var digit in digits)
            {
                digitPosition--;

                luckyNumbers += CalculateLuckyNumberForDigitAndPosition(digit, digitPosition, luckyNumbersByDigitPosition, previousLuckyDigit);

                if (digit == 6 || digit == 8)
                {
                    if (previousLuckyDigit != -1 && previousLuckyDigit != digit)
                    {
                        // We'll find no more lucky numbers.
                        return luckyNumbers;
                    }

                    previousLuckyDigit = digit;
                }
            }

            return luckyNumbers;
        }

        /// <summary>Calculate the numbers of lucky numbers for a digit and its position.<summary>
        private static long CalculateLuckyNumberForDigitAndPosition(
            int digit,
            int digitPosition,
            IDictionary<int, long> luckyNumbersByDigitPosition,
            int previousLuckyDigit)
            => (digit, previousLuckyDigit) switch
            {
                // We're in the classic case.
                (7, -1) => 6 * luckyNumbersByDigitPosition[digitPosition] + (long)BigInteger.Pow(9, digitPosition),
                (8, -1) => 7 * luckyNumbersByDigitPosition[digitPosition] + (long)BigInteger.Pow(9, digitPosition),
                (9, -1) => 7 * luckyNumbersByDigitPosition[digitPosition] + 2 * (long)BigInteger.Pow(9, digitPosition),
                (_, -1) => digit * luckyNumbersByDigitPosition[digitPosition],

                // Things change now that we know we have a lucky digit before us.
                (6, _) => 6 * (long)BigInteger.Pow(9, digitPosition),
                (7, 6) => 7 * (long)BigInteger.Pow(9, digitPosition),
                (7, _) => 6 * (long)BigInteger.Pow(9, digitPosition),
                (8, _) v when v.previousLuckyDigit != 8 => 8 * (long)BigInteger.Pow(9, digitPosition),
                (8, _) => 7 * (long)BigInteger.Pow(9, digitPosition),
                (9, _) => 8 * (long)BigInteger.Pow(9, digitPosition),
                (_, _) => digit * (long)BigInteger.Pow(9, digitPosition)
            };

        public static void Main(string[] args)
        {
            var inputs = Console.ReadLine().Split(' ');
            var L = long.Parse(inputs[0]);
            var R = long.Parse(inputs[1]);

            var luckyNumbersForL = CountLuckyNumberTo(L);
            var luckyNumbersForR = CountLuckyNumberTo(R + 1);

            Console.WriteLine(luckyNumbersForR - luckyNumbersForL);
        }
    }
}