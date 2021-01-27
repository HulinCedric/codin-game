namespace CodinGame.Puzzles.Easy.MIMEType
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    class Solution
    {
        static void Main(string[] args)
        {
            int N = int.Parse(Console.ReadLine()); // Number of elements which make up the association table.
            int Q = int.Parse(Console.ReadLine()); // Number Q of file names to be analyzed.

            var dicMime = new Dictionary<string, string>();
            for (int i = 0; i < N; i++)
            {
                string[] inputs = Console.ReadLine().Split(' ');

                string EXT = "." + inputs[0]; // file extension
                string MT = inputs[1]; // MIME type.

                dicMime.Add(EXT.ToLowerInvariant(), MT);
            }
            for (int i = 0; i < Q; i++)
            {
                string ext = Path.GetExtension(Console.ReadLine()).ToLowerInvariant();

                if (dicMime.ContainsKey(ext))
                {
                    Console.WriteLine(dicMime[ext]);
                }
                else
                {
                    Console.WriteLine("UNKNOWN");
                }
            }
        }
    }
}