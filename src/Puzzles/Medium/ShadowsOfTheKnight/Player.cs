namespace CodinGame.Puzzles.Medium.ShadowsOfTheKnight
{
    using System;

    public class Player
    {
        public static void Main(string[] args)
        {
            string[] inputs;
            inputs = Console.ReadLine().Split(' ');
            int W = int.Parse(inputs[0]); // width of the building.
            int H = int.Parse(inputs[1]); // height of the building.
            int N = int.Parse(Console.ReadLine()); // maximum number of turns before game over.
            inputs = Console.ReadLine().Split(' ');
            int X0 = int.Parse(inputs[0]);
            int Y0 = int.Parse(inputs[1]);

            var topLeftX = 0;
            var topRightX = W;
            var topLeftY = 0;
            var bottomLeftY = H;

            while (true)
            {
                string bombDir = Console.ReadLine(); // the direction of the bombs from batman's current location (U, UR, R, DR, D, DL, L or UL)

                if (bombDir.Contains('D'))
                    topLeftY = Y0;

                if (bombDir.Contains('R'))
                    topLeftX = X0;

                if (bombDir.Contains('U'))
                    bottomLeftY = Y0;

                if (bombDir.Contains('L'))
                    topRightX = X0;

                X0 = (topLeftX + topRightX) / 2;
                Y0 = (topLeftY + bottomLeftY) / 2;

                Console.WriteLine(X0 + " " + Y0);
            }
        }
    }
}