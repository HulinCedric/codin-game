namespace CodinGame.Puzzles.Easy.DeadMensShot
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public struct Corner
    {
        public Corner(int x, int y)
        {
            X = x;
            Y = y;
        }

        public readonly int X;
        public readonly int Y;
    }

    public struct Shot
    {
        public Shot(double x, double y)
        {
            X = x;
            Y = y;
        }

        public readonly double X;
        public readonly double Y;
    }

    public class Target
    {
        private List<Corner> corners = new List<Corner>();

        public void AddCorner(int x, int y)
            => corners.Add(new Corner(x, y));

        /// <remarks>
        /// Use point in polygon test describe by 
        /// https://jeffe.cs.illinois.edu/teaching/comptop/2009/notes/jordan-polygon-theorem.pdf
        /// </remarks>
        public bool IsHitBy(Shot shot)
        {
            var isHit = false;

            foreach (var i in Enumerable.Range(0, corners.Count))
            {
                var corner = corners[i];
                var nextCorner = corners[(i + 1) % corners.Count];

                var isBetweenCorners = ShotIsBetweenCorners(shot, corner, nextCorner);
                bool isCounterClockwise = IsCounterClockwise(shot, corner, nextCorner);

                if (isBetweenCorners && isCounterClockwise)
                {
                    isHit = !isHit;
                }
            }

            return isHit;
        }

        private static bool ShotIsBetweenCorners(
            Shot shot,
            Corner corner,
            Corner nextCorner)
            => (corner.Y > shot.Y) != (nextCorner.Y > shot.Y);


        private static bool IsCounterClockwise(
            Shot shot,
            Corner corner,
            Corner nextCorner)
            => shot.X < (nextCorner.X - corner.X) * (shot.Y - corner.Y) / (nextCorner.Y - corner.Y) + corner.X;

    }

    public class Solution
    {
        static void Main(string[] args)
        {
            var target = GetTarget();
            var shots = GetShots();

            foreach (var shot in shots)
            {
                if (target.IsHitBy(shot))
                {
                    Console.WriteLine("hit");
                }
                else
                {
                    Console.WriteLine("miss");
                }
            }
        }

        private static Target GetTarget()
        {
            var target = new Target();
            int N = int.Parse(Console.ReadLine());
            for (int i = 0; i < N; i++)
            {
                var cornerCoordinates = Console
                    .ReadLine()
                    .Split(' ')
                    .Select(rawCoordinate => int.Parse(rawCoordinate));

                target.AddCorner(
                    cornerCoordinates.First(),
                    cornerCoordinates.Last());
            }

            return target;
        }

        private static IEnumerable<Shot> GetShots()
        {
            int M = int.Parse(Console.ReadLine());
            for (int i = 0; i < M; i++)
            {
                var shotCoordinates = Console
                   .ReadLine()
                   .Split(' ')
                   .Select(rawCoordinate => double.Parse(rawCoordinate));
                yield return new Shot(shotCoordinates.First(), shotCoordinates.Last());
            }
        }
    }
}