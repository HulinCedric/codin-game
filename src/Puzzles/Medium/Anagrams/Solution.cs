namespace CodinGame.Puzzles.Medium.Anagrams
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Solution
    {
        public static void Main(string[] args)
        {
            var phrase = Phase4(Console.ReadLine());
            phrase = Phase3(phrase);
            phrase = Phase2(phrase);
            Console.WriteLine(Phase1(phrase));
        }

        public static string Phase1(string phrase)
        {
            // 2nd letter of the alphabet
            var letters = Enumerable.Range(0, 26).Where(i => i % 2 != 0).Select(i => (char)(i + 65));
            var queue = new Queue<char>();

            foreach (var character in phrase.Reverse().Where(c => letters.Contains(c)))
            {
                queue.Enqueue(character);
            }

            return Replace(phrase, letters, queue);
        }

        public static string Phase2(string phrase)
        {
            // 3rd letter of the alphabet
            var letters = Enumerable.Range(0, 26).Where(i => i % 3 == 2).Select(i => (char)(i + 65));
            var queue = new Queue<char>();

            var ph1 = phrase.Where(c => letters.Contains(c));
            foreach (var character in ph1.Skip(1))
            {
                queue.Enqueue(character);
            }
            if (ph1.Count() >= 2)
                queue.Enqueue(ph1.First());

            return Replace(phrase, letters, queue);
        }

        public static string Phase3(string phrase)
        {
            // 4th letter of the alphabet
            var letters = Enumerable.Range(0, 26).Where(i => i % 4 == 3).Select(i => (char)(i + 65));
            var queue = new Queue<char>();

            var ph2 = phrase.Where(c => letters.Contains(c));

            if (ph2.Count() >= 2)
            {
                queue.Enqueue(ph2.Last());
                foreach (var character in ph2.Take(ph2.Count() - 1))
                {
                    queue.Enqueue(character);
                }
            }

            return Replace(phrase, letters, queue);
        }

        public static string Phase4(string phrase)
        {
            // 4th letter of the alphabet
            var wordLength = phrase.Split(" ".ToCharArray()).Select(c => c.Length).Reverse();
            phrase = phrase.Replace(" ", string.Empty);
            var result = string.Empty;

            foreach (var length in wordLength)
            {
                result += phrase.Substring(0, length) + " ";
                phrase = phrase.Substring(length);
            }

            return result.Trim();
        }

        private static string Replace(string phrase, IEnumerable<char> letters, Queue<char> queue)
        {
            var result = phrase;
            for (int i = 0; i < phrase.Length; i++)
            {
                if (letters.Contains(phrase.ElementAt(i)))
                {
                    result = result.Remove(i, 1);
                    result = result.Insert(i, queue.Dequeue().ToString());
                }
            }
            return result;
        }
    }
}