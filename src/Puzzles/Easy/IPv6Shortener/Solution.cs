namespace CodinGame.Puzzles.Easy.IPv6Shortener
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class IPv6AddressShortenerExtensions
    {
        private const string ZeroOnlyBlock = "0000";

        public static IEnumerable<string> RemoveLeadingZeros(this IEnumerable<string> addressBlocks)
            => addressBlocks.Select(addressBlock =>
                !addressBlock.All(v => v == '0') ?
                 addressBlock.TrimStart('0') :
                 addressBlock);

        public static IEnumerable<string> ShortZeroOnlyBlocks(this IEnumerable<string> addressBlocks)
            => addressBlocks.Select(addressBlock =>
                addressBlock == ZeroOnlyBlock ?
                "0" :
                addressBlock);

        public static IEnumerable<string> ShortLongestStreakZeroOnlyBlocks(this IEnumerable<string> addressBlocks)
        {
            var longestStreak = GetLongestStreak(addressBlocks);
            if (longestStreak.Length > ZeroOnlyBlock.Length)
            {
                addressBlocks =
                    string.Join(":", addressBlocks)
                    .Replace(longestStreak, ":")
                    .Replace(":::", "::")
                    .Split(":");
            }

            return addressBlocks;
        }

        private static string GetLongestStreak(IEnumerable<string> addressBlocks)
        {
            var longestStreak = 0;
            var currentStreak = 0;
            foreach (var addressBlock in addressBlocks)
            {
                if (addressBlock == ZeroOnlyBlock)
                {
                    currentStreak++;
                    if (currentStreak > longestStreak)
                    {
                        longestStreak = currentStreak;
                    }

                    continue;
                }

                currentStreak = 0;
            }

            return string.Join("", Enumerable.Repeat(ZeroOnlyBlock + ":", longestStreak))
                .TrimEnd(':');
        }
    }

    public class IPv6Address
    {
        private readonly string address;

        public IPv6Address(string address)
            => this.address = address;

        public string GetCompressedAddress()
            => string.Join(':',
                address
                .Split(":")
                .ShortLongestStreakZeroOnlyBlocks()
                .ShortZeroOnlyBlocks()
                .RemoveLeadingZeros());
    }

    public class Solution
    {
        public static void Main(string[] args)
            => Console.WriteLine(
                new IPv6Address(Console.ReadLine())
                .GetCompressedAddress());
    }
}