using System;

namespace CodinGame.Puzzles.Easy.RectanglePartition
{
    internal class Solution
    {
        private static void Main(string[] args)
        {
            string[] inputs;
            inputs = Console.ReadLine().Split(' ');
            var rectangleWidth = int.Parse(inputs[0]);
            var rectangleHeight = int.Parse(inputs[1]);

            var rectangle = new Rectangle(rectangleWidth, rectangleHeight);

            var measurementXCount = int.Parse(inputs[2]);
            var measurementYCount = int.Parse(inputs[3]);
            inputs = Console.ReadLine().Split(' ');
            for (var i = 0; i < measurementXCount; i++)
            {
                var measurementOnXAxis = int.Parse(inputs[i]);
                rectangle.AddMeasurementOnXAxis(measurementOnXAxis);
            }

            inputs = Console.ReadLine().Split(' ');
            for (var i = 0; i < measurementYCount; i++)
            {
                var measurementOnYAxis = int.Parse(inputs[i]);
                rectangle.AddMeasurementOnYAxis(measurementOnYAxis);
            }

            var numberOfSquare = rectangle.GiveMeNumberOfSquare();
            
            Console.WriteLine(numberOfSquare);
        }
    }
}