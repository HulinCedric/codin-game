namespace CodinGame.Puzzles.Easy.TheDescent
{
    using System;

    /**
     * Auto-generated code below aims at helping you parse
     * the standard input according to the problem statement.
     **/
    class Player
    {
        static void Main(string[] args)
        {
            // game loop
            while (true)
            {
                int mountainH = 0;
                var targetMountain = 0;
                for (int i = 0; i < 8; i++)
                {
                    var temp = int.Parse(Console.ReadLine()); // represents the height of one mountain, from 9 to 0.
                    if (temp > mountainH)
                    {
                        mountainH = temp;
                        targetMountain = i;
                    }
                }

                // Write an action using Console.WriteLine()
                // To debug: Console.Error.WriteLine("Debug messages...");

                Console.WriteLine(targetMountain); // The number of the mountain to fire on.
            }
        }
    }
}