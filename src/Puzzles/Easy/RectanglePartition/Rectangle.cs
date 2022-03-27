using System.Collections.Generic;

namespace CodinGame.Puzzles.Easy.RectanglePartition
{
    public class Rectangle
    {
        private readonly List<int> measurementOnXAxis = new List<int>();
        private readonly List<int> measurementOnYAxis = new List<int>();

        public Rectangle(int width, int height)
        {
            measurementOnYAxis.Add(0);
            measurementOnXAxis.Add(0);

            measurementOnXAxis.Add(width);
            measurementOnYAxis.Add(height);
        }

        public void AddMeasurementOnXAxis(int measurement)
            => measurementOnXAxis.Add(measurement);

        public void AddMeasurementOnYAxis(int measurement)
            => measurementOnYAxis.Add(measurement);

        public int GiveMeNumberOfSquare()
        {
            var measuredDistancesOnXAxis = GetAllMeasuredDistances(measurementOnXAxis);
            var measuredDistancesOnYAxis = GetAllMeasuredDistances(measurementOnYAxis);

            return SumAllEqualDistances(measuredDistancesOnXAxis, measuredDistancesOnYAxis);
        }

        private static List<int> GetAllMeasuredDistances(IEnumerable<int> measurements)
        {
            var sortedMeasurements = new List<int>(measurements);
            sortedMeasurements.Sort();

            var measuredDistances = new List<int>();
            for (var i = 0; i < sortedMeasurements.Count; i++)
            for (var j = i + 1; j < sortedMeasurements.Count; j++)
                measuredDistances.Add(sortedMeasurements[j] - sortedMeasurements[i]);

            return measuredDistances;
        }

        private static int SumAllEqualDistances(
            IReadOnlyCollection<int> measuredDistancesOnXAxis,
            IReadOnlyCollection<int> measuredDistancesOnYAxis)
        {
            var squareCount = 0;

            foreach (var distanceX in measuredDistancesOnXAxis)
            foreach (var distanceY in measuredDistancesOnYAxis)
                if (distanceX == distanceY)
                    squareCount++;

            return squareCount;
        }
    }
}