namespace CodinGame.Puzzles.Easy.Defibrillators
{
    using System;

    class Solution
    {
        static void Main(string[] args)
        {
            var lonA = double.Parse(Console.ReadLine().Replace(',', '.'));
            var latA = double.Parse(Console.ReadLine().Replace(',', '.'));
            int N = int.Parse(Console.ReadLine());

            var adresse = string.Empty;
            var distance = double.MaxValue;
            for (int i = 0; i < N; i++)
            {
                var DEFIB = Console.ReadLine().Split(';');

                var lonB = double.Parse(DEFIB[4].Replace(',', '.'));
                var latB = double.Parse(DEFIB[5].Replace(',', '.'));

                var currentDistance = (Math.Sqrt(Math.Pow((lonB - lonA) * Math.Cos((latA + latB) / 2), 2) + Math.Pow((latB - latA), 2))) * 6371;

                if (distance > currentDistance)
                {
                    distance = currentDistance;
                    adresse = DEFIB[1];
                }
            }

            Console.WriteLine(adresse);
        }
    }
}