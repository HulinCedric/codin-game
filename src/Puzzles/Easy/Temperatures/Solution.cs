namespace CodinGame.Puzzles.Easy.Temperatures
{
    using System;
    using System.Linq;

    class Solution
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            var temps = Console.ReadLine()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse); // the n temperatures expressed as integers ranging from -273 to 5526

            int comparer = int.MaxValue;
            int result = 0;
            foreach (var current in temps)
            {
                if (Math.Abs(current) < comparer)
                {
                    comparer = Math.Abs(current);
                    result = current;
                }
            }

            if (result < 0 && temps.Contains(Math.Abs(result)))
            {
                result = Math.Abs(result);
            }

            Console.WriteLine(result);
        }
    }
}