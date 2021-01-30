namespace CodinGame.Puzzles.Medium.NetworkCabling
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class MathExtension
    {
        public static long GetMedian(this IList<int> source)
        {
            if (source.Count % 2 == 1)
            {
                var middle = (source.Count - 1) / 2;
                return source[middle];
            }
            else
            {
                var MidValue = source.Count / 2;
                return (source[MidValue - 1] + source[MidValue + 1]) / 2;
            }
        }
    }

    public class Solution
    {
        public static void Main(string[] args)
        {
            var list = Enumerable
                .Range(0, int.Parse(Console.ReadLine()))
                .Select(x => Console.ReadLine().Split(' '))
                .Select(x => new KeyValuePair<int, int>(
                    int.Parse(x[0]),
                    int.Parse(x[1])))
                .ToList();

            // The distance beetween first and last house (minX, maxX) for calculate the horizontal distance
            var horizontalCoordList = list.OrderBy(x => x.Key).Select(x => x.Key).ToList();
            var xFirstHouse = horizontalCoordList.First();
            var xLastHouse = horizontalCoordList.Last();
            long horizontalDistance = xLastHouse - xFirstHouse;

            // Addition of all vertical distance beetween middle distance
            var verticalCoordList = list.OrderBy(x => x.Value).Select(x => x.Value).ToList();

            // Calculate the median value on vertical coords
            long median = verticalCoordList.GetMedian();

            // Calculate the vertical total distance
            var verticalDistance = verticalCoordList.Sum(x => Math.Abs(x - median));

            // Addition of horizontal and vertical distance
            var totalDistance = Convert.ToInt64(horizontalDistance + verticalDistance);

            Console.WriteLine(totalDistance);
        }
    }
}