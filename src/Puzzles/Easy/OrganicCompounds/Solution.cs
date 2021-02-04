namespace CodinGame.Puzzles.Easy.OrganicCompounds
{
    using System;
    using System.Linq;
    using System.Collections.Generic;


    public class Solution
    {
        private const int elementLength = 3;

        public static void Main(string[] args)
        {
            (var totalHydroCarbon, var totalBond) = GetCompoundContent();
            var validity = ComputeCondensedFormulaValidity(totalHydroCarbon, totalBond);

            if (validity)
            {
                Console.WriteLine("VALID");
            }
            else
            {
                Console.WriteLine("INVALID");
            }
        }

        private static (int, int) GetCompoundContent()
        {
            var numberOfLines = int.Parse(Console.ReadLine());

            var totalHydroCarbon = 0;
            var totalBond = 0;
            foreach (var element in from compound in GetCompoundLineRepresentation(numberOfLines)
                                    from element in GetCompoundElements(compound)
                                    select element)
            {
                switch (element)
                {
                    case var el when el.StartsWith("CH"):
                        totalHydroCarbon += 4 - int.Parse(el.Last().ToString());
                        break;

                    case var el when el.StartsWith("("):
                        totalBond += 2 * int.Parse(el.ElementAt(1).ToString());
                        break;
                }
            }

            return (totalHydroCarbon, totalBond);
        }

        private static bool ComputeCondensedFormulaValidity(int totalHydroCarbon, int totalBond)
            => totalHydroCarbon == totalBond;

        private static IEnumerable<string> GetCompoundLineRepresentation(int numberOfLines)
            => Enumerable.Range(0, numberOfLines)
            .Select(i => Console.ReadLine());

        private static IEnumerable<string> GetCompoundElements(string compound)
            => Enumerable.Range(0, compound.Length / elementLength)
            .Select(i => compound.Substring(i * elementLength, elementLength))
            .Where(element => !string.IsNullOrWhiteSpace(element));
    }
}