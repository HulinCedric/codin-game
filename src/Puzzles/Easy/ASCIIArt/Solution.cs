namespace CodinGame.Puzzles.Easy.ASCIIArt
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;

    class Solution
    {
        static void Main(string[] args)
        {
            int L = int.Parse(Console.ReadLine());
            int H = int.Parse(Console.ReadLine());

            string T = Console.ReadLine().ToUpperInvariant();

            T = new Regex("[^A-Z]").Replace(T, "[");

            for (int i = 0; i < H; i++)
            {
                string ROW = Console.ReadLine();

                string line = T
                    .ToCharArray()
                    .Select(c => (int)c - 'A')
                    .Aggregate(string.Empty, (l, x) => l + ROW.Substring(x * L, L));

                Console.WriteLine(line);
            }
        }
    }
}