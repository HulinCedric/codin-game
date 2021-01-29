namespace CodinGame.Puzzles.Medium.Scrabble
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class Solution
    {
        private static IDictionary<IEnumerable<char>, int> scrabbleRate = new Dictionary<IEnumerable<char>, int>
        {
            { new List<char>  { 'e', 'a', 'i', 'o', 'n', 'r', 't', 'l', 's', 'u' }, 1  },
            { new List<char>  { 'd', 'g' },                                         2  },
            { new List<char>  { 'b', 'c', 'm', 'p' },                               3  },
            { new List<char>  { 'f', 'h', 'v', 'w', 'y' },                          4  },
            { new List<char>  { 'k' },                                              5  },
            { new List<char>  { 'j', 'x' },                                         8  },
            { new List<char>  { 'q', 'z' },                                         10 }
        };

        public static bool IsPossibleWord(IEnumerable<char> letters, string word)
        {
            while (true)
            {
                if (!letters.Any())
                    if (word.Length > 0)
                        return false;
                    else
                        return true;
                var character = letters.Take(1).First();
                letters = string.Concat(letters.Skip(1));
                if (!word.Contains(character))
                    continue;
                else
                {
                    var regex = new Regex(Regex.Escape(character.ToString()));
                    word = regex.Replace(word, string.Empty, 1);
                }
            }
        }

        public static int LetterRate(char character)
        {
            return scrabbleRate.Where(listCharacter => listCharacter.Key.Contains(character)).Select(rate => rate.Value).First();
        }

        public static void Main(string[] args)
        {
            int N = int.Parse(Console.ReadLine());

            var dictionnaryWords = Enumerable.Range(0, N).Select(x => Console.ReadLine()).ToList();

            var letters = Console.ReadLine();

            var possibleWord = dictionnaryWords.Where(word => word.Length <= letters.Length)
                                               .Where(word => word.All(character => letters.Contains(character)))
                                               .Where(word => IsPossibleWord(letters, word)).ToList();

            var bestWordRated = possibleWord.OrderByDescending(x => WordRate(x)).First();

            Console.WriteLine(bestWordRated);
        }

        public static int WordRate(string word)
        {
            return word.Sum(LetterRate);
        }
    }
}