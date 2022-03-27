using FluentAssertions;
using Xunit;

namespace CodinGame.Puzzles.Easy.RectanglePartition
{
    public class AcceptanceTest
    {
        [Fact]
        public void Acceptance_test()
        {
            const int width = 10;
            const int height = 5;
            var rectangle = new Rectangle(width, height);

            rectangle.AddMeasurementOnXAxis(2);
            rectangle.AddMeasurementOnXAxis(5);

            rectangle.AddMeasurementOnYAxis(3);

            var numberOfSquare = rectangle.GiveMeNumberOfSquare();

            numberOfSquare.Should().Be(4);
        }
    }

    public class RectangleShould
    {
        [Fact]
        public void Not_be_a_square_when_width_not_equals_height()
        {
            const int width = 10;
            const int height = 5;
            var rectangle = new Rectangle(width, height);

            var numberOfSquare = rectangle.GiveMeNumberOfSquare();

            numberOfSquare.Should().Be(0);
        }

        [Fact]
        public void Be_a_square_when_width_equals_height()
        {
            const int width = 10;
            const int height = 10;
            var rectangle = new Rectangle(width, height);

            var numberOfSquare = rectangle.GiveMeNumberOfSquare();

            numberOfSquare.Should().Be(1);
        }

        [Fact]
        public void Be_split_in_two_square_when_width_cut_in_middle_and_height_equals_middle_width()
        {
            const int width = 10;
            const int height = 5;
            var rectangle = new Rectangle(width, height);

            rectangle.AddMeasurementOnXAxis(5);

            var numberOfSquare = rectangle.GiveMeNumberOfSquare();

            numberOfSquare.Should().Be(2);
        }

        [Fact]
        public void Be_split_in_2_square_when_height_cut_in_middle_and_width_equals_middle_height()
        {
            const int width = 5;
            const int height = 10;
            var rectangle = new Rectangle(width, height);

            rectangle.AddMeasurementOnYAxis(5);

            var numberOfSquare = rectangle.GiveMeNumberOfSquare();

            numberOfSquare.Should().Be(2);
        }

        [Fact]
        public void Be_split_in_4_square_when_height_and_width_cut_in_the_middle()
        {
            const int width = 10;
            const int height = 10;
            var rectangle = new Rectangle(width, height);

            rectangle.AddMeasurementOnXAxis(5);
            rectangle.AddMeasurementOnYAxis(5);

            var numberOfSquare = rectangle.GiveMeNumberOfSquare();

            numberOfSquare.Should().Be(5);
        }

        [Fact]
        public void Be_split_in_5_square_of_2x2_when_cut_at_4_position_on_X()
        {
            const int width = 10;
            const int height = 2;
            var rectangle = new Rectangle(width, height);

            rectangle.AddMeasurementOnXAxis(2);
            rectangle.AddMeasurementOnXAxis(4);
            rectangle.AddMeasurementOnXAxis(6);
            rectangle.AddMeasurementOnXAxis(8);

            var numberOfSquare = rectangle.GiveMeNumberOfSquare();

            numberOfSquare.Should().Be(5);
        }

        [Fact]
        public void Count_55_squares_when_a_10x10_square_is_split_in_2x2_squares()
        {
            const int width = 10;
            const int height = 10;
            var rectangle = new Rectangle(width, height);

            rectangle.AddMeasurementOnXAxis(2);
            rectangle.AddMeasurementOnXAxis(4);
            rectangle.AddMeasurementOnXAxis(6);
            rectangle.AddMeasurementOnXAxis(8);

            rectangle.AddMeasurementOnYAxis(2);
            rectangle.AddMeasurementOnYAxis(4);
            rectangle.AddMeasurementOnYAxis(6);
            rectangle.AddMeasurementOnYAxis(8);

            var numberOfSquare = rectangle.GiveMeNumberOfSquare();

            numberOfSquare.Should().Be(55);
        }
    }
}