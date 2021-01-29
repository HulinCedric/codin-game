namespace CodinGame.Puzzles.Medium.DwarfsStandingOnTheShouldersOfDiants
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Solution
    {
        public static void Main(string[] args)
        {
            var relationships = new Dictionary<int, List<int>>();
            var numberOfRelationships = int.Parse(Console.ReadLine());
            
            for (int i = 0; i < numberOfRelationships; i++)
            {
                string[] inputs = Console.ReadLine().Split(' ');
                var influencer = int.Parse(inputs[0]);
                var influenced = int.Parse(inputs[1]);
                AddRelationship(relationships, influencer, influenced);
            }

            var longestRelationship = relationships.Keys
                                      .Select(influencer => 1 + RouteRelationships(relationships, influencer))
                                      .Max();

            Console.WriteLine(longestRelationship);
        }

        private static int RouteRelationships(Dictionary<int, List<int>> relationships, int influencer)
        {
            var longestRelationship = 0;
            if (relationships.ContainsKey(influencer))
            {
                longestRelationship = relationships[influencer]
                                      .Select(influenced => 1 + RouteRelationships(relationships, influenced))
                                      .Max();
            }
            return longestRelationship;
        }

        private static void AddRelationship(Dictionary<int, List<int>> relationships, int influencer, int influenced)
        {
            if (!relationships.ContainsKey(influencer))
            {
                relationships.Add(influencer, new List<int>());
            }
            relationships[influencer].Add(influenced);
        }
    }
}