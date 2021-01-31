namespace CodinGame.Puzzles.Medium.Gravity
{
    using System;

    public class Solution
    {
        public static void Main(string[] args)
        {
            string[] inputs = Console.ReadLine().Split(' ');
            int width = int.Parse(inputs[0]);
            int height = int.Parse(inputs[1]);
            var result = new int[width];

            for (int i = 0; i < height; i++)
            {
                var line = Console.ReadLine().ToCharArray();
                for (int j = 0; j < width; j++)
                {
                    if (line[j] == '#')
                    {
                        result[j] += 1;
                    }
                }
            }

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (result[j] >= height - i)
                    {
                        Console.Write("#");
                    }
                    else
                    {
                        Console.Write(".");
                    }
                }
                
                Console.WriteLine("");
            }
        }
    }
}