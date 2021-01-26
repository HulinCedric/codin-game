namespace CodinGame.Puzzles.Easy.PowerOfThor
{
    using System;

    /**
     * Auto-generated code below aims at helping you parse
     * the standard input according to the problem statement.
     * ---
     * Hint: You can use the debug stream to print initialTX and initialTY, if Thor seems not follow your orders.
     **/
    class Player
    {
        static void Main(string[] args)
        {
            string[] inputs = Console.ReadLine().Split(' ');
            int lightX = int.Parse(inputs[0]); // the X position of the light of power
            int lightY = int.Parse(inputs[1]); // the Y position of the light of power
            int initialTX = int.Parse(inputs[2]); // Thor's starting X position
            int initialTY = int.Parse(inputs[3]); // Thor's starting Y position

            var currentTX = initialTX;
            var currentTY = initialTY;

            // game loop
            while (true)
            {
                int remainingTurns = int.Parse(Console.ReadLine()); // The remaining amount of turns Thor can move. Do not remove this line.

                if (currentTY < lightY && currentTY + 1 < 18)
                {
                    Console.Write("S");
                    currentTY += 1;
                }
                else if (currentTY > lightY && currentTY - 1 >= 0)
                {
                    Console.Write("N");
                    currentTY -= 1;
                }

                if (currentTX < lightX && currentTX + 1 < 40)
                {
                    Console.Write("E");
                    currentTX += 1;
                }
                else if (currentTX > lightX && currentTX - 1 >= 0)
                {
                    Console.Write("W");
                    currentTX -= 1;
                }

                Console.WriteLine("");
            }
        }
    }
}