namespace CodinGame.Puzzles.Medium.TelephoneNumbers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Solution
    {
        public static void Main(string[] args)
        {
            var memoryPhone = new Node();
            var totalMemoryPhoneSize = Enumerable
                .Range(0, int.Parse(Console.ReadLine()))
                .Sum(x => GetMemorySizeForPhoneNumber(memoryPhone, Console.ReadLine()));
                
            Console.WriteLine(totalMemoryPhoneSize);
        }

        public class Node
        {
            public Dictionary<int, Node> Children { get; set; } = new Dictionary<int, Node>();
        }

        public static int GetMemorySizeForPhoneNumber(Node phoneMemory, string phoneNumber)
        {
            var memoryCount = 0;
            var workingMemory = phoneMemory;

            foreach (var number in phoneNumber)
            {
                if (!workingMemory.Children.Keys.Contains(number))
                {
                    workingMemory.Children.Add(number, new Node());
                    memoryCount++;
                }
                workingMemory = workingMemory.Children[number];
            }

            return memoryCount;
        }
    }
}