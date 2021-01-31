namespace CodinGame.Puzzles.Medium.TextAlignment
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Solution
    {
        public static void Main(string[] args)
        {
            string alignment = Console.ReadLine();
            int N = int.Parse(Console.ReadLine());
            var lines = new List<string>();
            for (int i = 0; i < N; i++)
            {
                lines.Add(Console.ReadLine());
            }

            var biggestLineLength = lines.Max(c => c.Length);

            foreach (var line in lines)
            {
                switch (alignment)
                {
                    case "LEFT":
                        Console.WriteLine(line);
                        break;

                    case "RIGHT":
                        var ecart = biggestLineLength - line.Length;
                        for (int i = 0; i < ecart; i++)
                        {
                            Console.Write(" ");
                        }
                        Console.WriteLine(line);
                        break;

                    case "CENTER":
                        var ecartCenter = (biggestLineLength - line.Length) / 2;
                        for (int i = 0; i < ecartCenter; i++)
                        {
                            Console.Write(" ");
                        }
                        Console.WriteLine(line);
                        break;

                    case "JUSTIFY":
                        var ecartTotal = biggestLineLength - line.Length;
                        var ecartBetweenWord = ecartTotal / (line.Split(' ').Length - 2);
                        var words = line.Split(' ');
                        for (int i = 0; i < words.Length; i++)
                        {
                            Console.Write(words[i]);
                            if (i < words.Length - 1)
                            {
                                for (int j = 0; j < ecartBetweenWord + 1; j++)
                                {
                                    Console.Write(" ");
                                }
                            }
                        }

                        Console.WriteLine(string.Empty);

                        break;
                }
            }
        }
    }
}