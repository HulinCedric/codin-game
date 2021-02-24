namespace CodinGame.Puzzles.VeryHard.RecurringDecimals
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    /// <remarks>
    /// Solution adaptation from https://www.xarg.org/puzzle/codingame/recurring-decimals
    /// </remarks>
    public class Solution
    {
        public static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            var i = 0;
            var a = new Dictionary<int, int> { { 0, 1 } };
            var off = 0;
            var ret = "";
            do
            {
                var t = a[i] * 10;
                ret += t / n | 0;
                a[++i] = t % n;
                off = a.FirstOrDefault(x => x.Value == a[i]).Key;
            }
            while (a[i] != 0 && i == off);

            if (a[i] != 0)
            {
                Console.WriteLine($"0.{ret.Substring(0, off)}({ret.Substring(off, ret.Length - off)})");
            }
            else
            {
                Console.WriteLine($"0.{ret}");
            }
        }
    }
}