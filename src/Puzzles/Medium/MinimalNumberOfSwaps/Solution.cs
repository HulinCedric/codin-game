namespace CodinGame.Puzzles.Medium.MinimalNumberOfSwaps
{
    using System;

    public class Solution
    {
        public static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            var inputs = Console.ReadLine().Replace(" ", string.Empty);

            var counter = 0;
            while (inputs.Contains("01"))
            {
                var index1 = inputs.LastIndexOf("1");
                var index0 = inputs.IndexOf("0");

                inputs = inputs.Remove(index1, 1);
                inputs = inputs.Insert(index1, "0");
                inputs = inputs.Remove(index0, 1);
                inputs = inputs.Insert(index0, "1");

                counter++;
            }

            Console.WriteLine(counter);
        }
    }
}