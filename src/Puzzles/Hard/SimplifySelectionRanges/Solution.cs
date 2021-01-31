namespace CodinGame.Puzzles.Hard.SimplifySelectionRanges
{
    using System;
    using System.Linq;

    class Solution
    {
        static void Main(string[] args)
        {
            string N = Console.ReadLine();

            var ordered = N
                .Replace("[", string.Empty)
                .Replace("]", string.Empty)
                .Split(',')
                .Select(c => int.Parse(c))
                .Where(c => 1 <= c && c <= 100)
                .Distinct()
                .OrderBy(c => c)
                .ToArray();

            var stri = string.Empty;
            var rangIsBegining = false;
            for (int i = 0; i < ordered.Count(); i++)
            {

                if (!rangIsBegining)
                {
                    stri += ordered[i];
                    if (i + 1 < ordered.Count() && ordered[i + 1] == ordered[i] + 1)
                        rangIsBegining = true;
                    else if (i + 1 < ordered.Count())
                        stri += ",";
                }
                else
                {
                    if (i + 1 < ordered.Count() && ordered[i + 1] == ordered[i] + 1)
                    {
                        if (stri.ToCharArray().LastOrDefault() != '-')
                            stri += "-";
                    }
                    else
                    {
                        if (stri.ToCharArray().LastOrDefault() != '-')
                        {
                            stri += ",";
                        }

                        stri += ordered[i];

                        if (i + 1 < ordered.Count())
                        {
                            stri += ",";
                        }

                        rangIsBegining = false;
                    }
                }
            }

            Console.WriteLine(stri);
        }
    }
}