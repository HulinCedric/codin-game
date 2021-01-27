namespace CodinGame.Puzzles.Easy.BracketsExtremeEdition
{
    using System;
    using System.Linq;

    class Solution
    {
        static void Main(string[] args)
        {
            var expression = string.Concat(Console.ReadLine().Where(c => "(){}[]".Contains(c)));

            while (true)
            {
                var temp = expression;
                expression = expression.Replace("[]", string.Empty).Replace("()", string.Empty).Replace("{}", string.Empty);
                if (temp == expression) { break; }
            }

            Console.WriteLine((expression.Length == 0).ToString().ToLower());
        }
    }
}