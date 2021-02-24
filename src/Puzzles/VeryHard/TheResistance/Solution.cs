namespace CodinGame.Puzzles.VeryHard.TheResistance
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class MorseConverter
    {
        public static string ToMorse(string word)
            => string.Join(
                string.Empty,
                word.Select(c => ToMorse(c)));

        private static string ToMorse(char character)
            => character switch
            {
                'A' => ".-",
                'B' => "-...",
                'C' => "-.-.",
                'D' => "-..",
                'E' => ".",
                'F' => "..-.",
                'G' => "--.",
                'H' => "....",
                'I' => "..",
                'J' => ".---",
                'K' => "-.-",
                'L' => ".-..",
                'M' => "--",
                'N' => "-.",
                'O' => "---",
                'P' => ".--.",
                'Q' => "--.-",
                'R' => ".-.",
                'S' => "...",
                'T' => "-",
                'U' => "..-",
                'V' => "...-",
                'W' => ".--",
                'X' => "-..-",
                'Y' => "-.--",
                'Z' => "--..",
                _ => string.Empty,
            };
    }

    public class MorseDictionary
    {
        private readonly List<string> words = new List<string>();

        public void AddWord(string word)
            => words.Add(MorseConverter.ToMorse(word));

        public long GetNumberOfPossibleWordsInSentence(string sentence)
            => GetMemoizedNumberOfPossibleWordsInSentence(sentence, 0, new Dictionary<int, long>());

        private long GetMemoizedNumberOfPossibleWordsInSentence(
            string sentence,
            int index,
            Dictionary<int, long> memoizedNumberOfWords)
        {
            if (memoizedNumberOfWords.ContainsKey(index))
            {
                return memoizedNumberOfWords[index];
            }

            var numberOfWords = GetNumberOfPossibleWordsInSentence(sentence, index, memoizedNumberOfWords);

            memoizedNumberOfWords.Add(index, numberOfWords);

            return numberOfWords;
        }

        private long GetNumberOfPossibleWordsInSentence(
            string sentence,
            int index,
            Dictionary<int, long> memoizedNumberOfWords)
        {
            if (index == sentence.Length)
            {
                return 1;
            }

            var numberOfWords = 0L;
            foreach (var word in words)
            {
                var wordInSentence = sentence.Substring(index, Math.Min(word.Length, sentence.Length - index));
                if (wordInSentence == word)
                {
                    numberOfWords += GetMemoizedNumberOfPossibleWordsInSentence(sentence, index + word.Length, memoizedNumberOfWords);
                }
            }

            return numberOfWords;
        }
    }

    public class Solution
    {
        public static void Main(string[] args)
        {
            var morseWord = Console.ReadLine();
            var numberOfWord = int.Parse(Console.ReadLine());

            var morseDictionary = new MorseDictionary();
            for (int i = 0; i < numberOfWord; i++)
            {
                morseDictionary.AddWord(Console.ReadLine());
            }

            Console.WriteLine(morseDictionary.GetNumberOfPossibleWordsInSentence(morseWord));
        }
    }
}