namespace CodinGame.Puzzles.Hard.SevenSegmentDisplay
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Solution
    {
        private const string space = " ";
        private static IEnumerable<int> bottomBar = new List<int>() { 0, 2, 3, 5, 6, 8, 9 };
        private static IEnumerable<int> bottomLeftBar = new List<int>() { 0, 2, 6, 8 };
        private static IEnumerable<int> bottomRightBar = new List<int>() { 0, 1, 3, 4, 5, 6, 7, 8, 9 };
        private static IEnumerable<int> middleBar = new List<int>() { 2, 3, 4, 5, 6, 8, 9 };
        private static IEnumerable<int> topBar = new List<int>() { 0, 2, 3, 5, 6, 7, 8, 9 };
        private static IEnumerable<int> topLeftBar = new List<int>() { 0, 4, 5, 6, 8, 9 };
        private static IEnumerable<int> topRightBar = new List<int>() { 0, 1, 2, 3, 4, 7, 8, 9 };

        private static string GetHorizontalBar(string character, int size, bool hasBar)
        {
            var line = new StringBuilder().Append(space);

            for (int j = 0; j < size; j++)
                if (hasBar)
                    line.Append(character);
                else
                    line.Append(space);

            return line.Append(space).ToString();
        }

        private static string GetVerticalBar(string character, int size, bool hasLeftBar, bool hasRightBar)
        {
            var line = new StringBuilder();

            if (hasLeftBar)
                line.Append(character);
            else
                line.Append(space);

            for (int j = 0; j < size; j++)
                line.Append(space);

            if (hasRightBar)
                line.Append(character);
            else
                line.Append(space);

            return line.Append(space).ToString();
        }

        public static void Main(string[] args)
        {
            var numbers = Console.ReadLine().ToCharArray().Select(x => int.Parse(x.ToString()));
            var character = Console.ReadLine();
            var size = int.Parse(Console.ReadLine());
            var line = new StringBuilder();

            // Top Horizontal Bar
            foreach (var number in numbers)
            {
                line.Append(GetHorizontalBar(character, size, topBar.Contains(number))).Append(space);
            }
            Console.WriteLine(line.ToString().TrimEnd());

            // Top Vertical Bar
            for (int i = 0; i < size; i++)
            {
                line.Clear();
                foreach (var N in numbers)
                {
                    line.Append(GetVerticalBar(character, size, topLeftBar.Contains(N), topRightBar.Contains(N)));
                }
                Console.WriteLine(line.ToString().TrimEnd());
            }

            // Middle Horizontal Bar
            line.Clear();
            foreach (var number in numbers)
            {
                line.Append(GetHorizontalBar(character, size, middleBar.Contains(number))).Append(space);
            }
            Console.WriteLine(line.ToString().TrimEnd());

            // Bottom Vertical Bar
            for (int i = 0; i < size; i++)
            {
                line.Clear();
                foreach (var N in numbers)
                {
                    line.Append(GetVerticalBar(character, size, bottomLeftBar.Contains(N), bottomRightBar.Contains(N)));
                }
                Console.WriteLine(line.ToString().TrimEnd());
            }

            // Bottom Horizontal Bar
            line.Clear();
            foreach (var number in numbers)
            {
                line.Append(GetHorizontalBar(character, size, bottomBar.Contains(number))).Append(space);
            }
            Console.WriteLine(line.ToString().TrimEnd());
        }
    }
}