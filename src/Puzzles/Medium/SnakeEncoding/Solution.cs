namespace CodinGame.Puzzles.Medium.SnakeEncoding
{
    using System;

    internal class Solution
    {
        private static void Encode(string[,] arr)
        {
            var N = arr.GetLength(0);
            var j = 0;
            var last = N % 2 == 0 ? arr[N - 1, N - 1] : arr[0, N - 1];

            while (j < N)
            {
                if (j % 2 == 0)
                {
                    for (int i = N - 1; i >= 0; i--)
                    {
                        var temp = arr[i, j];
                        arr[i, j] = last;
                        last = temp;
                    }
                }
                else
                {
                    for (int i = 0; i < N; i++)
                    {
                        var temp = arr[i, j];
                        arr[i, j] = last;
                        last = temp;
                    }
                }
                j++;
            }
        }

        private static void Main(string[] args)
        {
            int N = int.Parse(Console.ReadLine());
            int X = int.Parse(Console.ReadLine());
            var arr = new string[N, N];
            int j;
            for (int i = 0; i < N; i++)
            {
                var LINE = Console.ReadLine().ToCharArray();
                for (j = 0; j < N; j++)
                {
                    arr[i, j] = LINE[j].ToString();
                }
            }

            for (int i = 0; i < X; i++)
            {
                Encode(arr);
            }

            for (int i = 0; i < N; i++)
            {
                for (j = 0; j < N; j++)
                {
                    Console.Write(arr[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}