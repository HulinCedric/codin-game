namespace CodinGame.Puzzles.Hard.EncounterSurface
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public struct Point
    {
        public readonly double X;

        public readonly double Y;

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }
    }

    public class ConvexPolygon : IEnumerable<Point>
    {
        private readonly List<Point> corners;

        public ConvexPolygon(List<Point> points)
            => this.corners = ConvexPolygonHelper.GetSortedConvexPolygonPoints(points);

        /// <summary>
        /// Gets the area.
        /// </summary>
        /// <remarks>
        /// https://stackoverflow.com/a/16281192/7843402
        /// </remarks>
        public double GetArea()
        {
            if (!corners.Any())
            {
                return 0;
            }

            var areaCorners = corners.ToList();
            areaCorners.Add(corners.ElementAt(0));

            return Math.Abs(
                areaCorners.Take(areaCorners.Count - 1)
                .Select((p, i) => (areaCorners[i + 1].X - p.X) * (areaCorners[i + 1].Y + p.Y))
                .Sum() / 2);
        }

        public IEnumerator<Point> GetEnumerator()
            => corners.GetEnumerator();

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            => GetEnumerator();

        /// <summary>
        /// Determines whether [is in zone] [the specified point].
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>
        ///   <c>true</c> if [is in zone] [the specified point]; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// Use point in polygon test describe by
        /// https://jeffe.cs.illinois.edu/teaching/comptop/2009/notes/jordan-polygon-theorem.pdf
        /// </remarks>
        public bool IsInZone(Point point)
        {
            var isInZone = false;

            foreach (var i in Enumerable.Range(0, corners.Count))
            {
                var corner = corners[i];
                var nextCorner = corners[(i + 1) % corners.Count];

                var isBetweenCorners = IsBetweenCorners(point, corner, nextCorner);
                bool isCounterClockwise = IsCounterClockwise(point, corner, nextCorner);

                if (isBetweenCorners && isCounterClockwise)
                {
                    isInZone = !isInZone;
                }
            }

            return isInZone;
        }

        private static bool IsBetweenCorners(
            Point point,
            Point corner,
            Point nextCorner)
            => (corner.Y > point.Y) != (nextCorner.Y > point.Y);

        private static bool IsCounterClockwise(
            Point point,
            Point corner,
            Point nextCorner)
            => point.X < (nextCorner.X - corner.X) * (point.Y - corner.Y) / (nextCorner.Y - corner.Y) + corner.X;
    }

    public class Solution
    {
        public static void Main(string[] args)
        {
            var firstArmyNumberOfPoints = int.Parse(Console.ReadLine());
            var secondArmyNumberOfPoints = int.Parse(Console.ReadLine());

            var firstArmyZone = new ConvexPolygon(GetArmyZonePoints(firstArmyNumberOfPoints).ToList());
            var secondArmyZone = new ConvexPolygon(GetArmyZonePoints(secondArmyNumberOfPoints).ToList());

            var encounterSurface = EncounterSurfaceCalculator.GetEncouterSurface(firstArmyZone, secondArmyZone);

            Console.WriteLine(Math.Ceiling(encounterSurface.GetArea()));
        }

        private static IEnumerable<Point> GetArmyZonePoints(int numberOfPoints)
        {
            for (int i = 0; i < numberOfPoints; i++)
            {
                var zoneCoordinates = Console
                   .ReadLine()
                   .Split(' ')
                   .Select(rawCoordinate => double.Parse(rawCoordinate));
                yield return new Point(zoneCoordinates.First(), zoneCoordinates.Last());
            }
        }
    }

    /// <remarks>
    /// https://en.wikipedia.org/wiki/Curve_orientation#Orientation_of_a_simple_polygon
    /// </remarks>
    internal static class ConvexPolygonHelper
    {
        internal static List<Point> GetSortedConvexPolygonPoints(IEnumerable<Point> unsortPoints)
        {
            var centroid = GetCentroid(unsortPoints);
            var sortPoints = unsortPoints.ToList();
            sortPoints.Sort((a, b) =>
            {
                double a1 = (RadianToDegree(Math.Atan2(a.X - centroid.X, a.Y - centroid.Y)) + 360) % 360;
                double a2 = (RadianToDegree(Math.Atan2(b.X - centroid.X, b.Y - centroid.Y)) + 360) % 360;
                return (int)(a1 - a2);
            });

            return sortPoints;
        }

        private static Point GetCentroid(IEnumerable<Point> points)
        {
            var totalX = 0d;
            var totalY = 0d;
            foreach (var point in points)
            {
                totalX += point.X;
                totalY += point.Y;
            }
            var centerX = totalX / points.Count();
            var centerY = totalY / points.Count();
            return new Point(centerX, centerY);
        }

        private static double RadianToDegree(double angle)
            => angle * (180.0 / Math.PI);
    }

    /// <remarks>
    /// https://www.swtestacademy.com/intersection-convex-polygons-algorithm
    /// </remarks>
    internal static class EncounterSurfaceCalculator
    {
        public static ConvexPolygon GetEncouterSurface(ConvexPolygon firstArmyZone, ConvexPolygon secondArmyZone)
        {
            var encounterSurfacePoints = new HashSet<Point>();

            AddCornerOverlapPointsToEncounterSurfacePoints(firstArmyZone, secondArmyZone, encounterSurfacePoints);
            AddCornerOverlapPointsToEncounterSurfacePoints(secondArmyZone, firstArmyZone, encounterSurfacePoints);
            AddIntersectionPointsToEncounterSurfacePoints(firstArmyZone, secondArmyZone, encounterSurfacePoints);

            return new ConvexPolygon(encounterSurfacePoints.ToList());
        }

        private static void AddCornerOverlapPointsToEncounterSurfacePoints(ConvexPolygon firstPolygon, ConvexPolygon secondPolyhon, HashSet<Point> encounterSurfacePoints)
        {
            foreach (var firstPolygonCoordinate in firstPolygon.Where(firstArmyZoneCoordinate => secondPolyhon.IsInZone(firstArmyZoneCoordinate)))
            {
                encounterSurfacePoints.Add(firstPolygonCoordinate);
            }
        }

        private static void AddIntersectionPointsToEncounterSurfacePoints(ConvexPolygon firstArmyZone, ConvexPolygon secondArmyZone, HashSet<Point> encounterSurfacePoints)
        {
            for (int i = 0, next = 1; i < firstArmyZone.Count(); i++, next = (i + 1 == firstArmyZone.Count()) ? 0 : i + 1)
            {
                foreach (var item in GetIntersectionPoints(firstArmyZone.ElementAt(i), firstArmyZone.ElementAt(next), secondArmyZone))
                {
                    encounterSurfacePoints.Add(item);
                }
            }
        }

        private static Point? GetIntersectionPoint(Point l1p1, Point l1p2, Point l2p1, Point l2p2)
        {
            var A1 = l1p2.Y - l1p1.Y;
            var B1 = l1p1.X - l1p2.X;
            var C1 = A1 * l1p1.X + B1 * l1p1.Y;

            var A2 = l2p2.Y - l2p1.Y;
            var B2 = l2p1.X - l2p2.X;
            var C2 = A2 * l2p1.X + B2 * l2p1.Y;

            var det = A1 * B2 - A2 * B1;
            if (det == 0)
            {
                // Parallel lines
                return null;
            }
            else
            {
                var x = (B2 * C1 - B1 * C2) / det;
                var y = (A1 * C2 - A2 * C1) / det;
                var online1 = (
                    (Math.Min(l1p1.X, l1p2.X) < x || Math.Min(l1p1.X, l1p2.X) == x) &&
                    (Math.Max(l1p1.X, l1p2.X) > x || Math.Max(l1p1.X, l1p2.X) == x) &&
                    (Math.Min(l1p1.Y, l1p2.Y) < y || Math.Min(l1p1.Y, l1p2.Y) == y) &&
                    (Math.Max(l1p1.Y, l1p2.Y) > y || Math.Max(l1p1.Y, l1p2.Y) == y));
                var online2 = (
                    (Math.Min(l2p1.X, l2p2.X) < x || Math.Min(l2p1.X, l2p2.X) == x) &&
                    (Math.Max(l2p1.X, l2p2.X) > x || Math.Max(l2p1.X, l2p2.X) == x) &&
                    (Math.Min(l2p1.Y, l2p2.Y) < y || Math.Min(l2p1.Y, l2p2.Y) == y) &&
                    (Math.Max(l2p1.Y, l2p2.Y) > y || Math.Max(l2p1.Y, l2p2.Y) == y));

                if (online1 && online2)
                {
                    return new Point(x, y);
                }
            }

            // Intersection is at out of at least one segment.
            return null;
        }

        private static IEnumerable<Point> GetIntersectionPoints(Point l1p1, Point l1p2, ConvexPolygon polygon)
        {
            List<Point> intersectionPoints = new List<Point>();
            for (int i = 0; i < polygon.Count(); i++)
            {
                int next = (i + 1 == polygon.Count()) ? 0 : i + 1;

                var intersectionPoint = GetIntersectionPoint(l1p1, l1p2, polygon.ElementAt(i), polygon.ElementAt(next));
                if (intersectionPoint != null)
                {
                    intersectionPoints.Add(intersectionPoint.Value);
                }
            }

            return intersectionPoints;
        }
    }
}