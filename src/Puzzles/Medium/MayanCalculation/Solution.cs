namespace CodinGame.Puzzles.Medium.MayanCalculation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;



    public class Solution
    {
        public class Number
        {
            public Number(long number, IList<string> representation)
            {
                NumberInBase10 = number;
                matrix = representation;
            }

            public IList<string> matrix { get; set; }

            public long NumberInBase10 { get; private set; }

            public string GetRepresentation()
            {
                return string.Concat(matrix);
            }
        }

        public static long GetNumber(int H, IList<Number> numbers)
        {
            var firstNumbersRep = new List<string>();
            int S1 = int.Parse(Console.ReadLine());
            for (int i = 0; i < S1; i++)
            {
                firstNumbersRep.Add(Console.ReadLine());
            }

            var numberOfNumber = S1 / H;
            var calPuis = numberOfNumber - 1;
            var result = 0;
            for (int i = 0; i < numberOfNumber; i++)
            {
                var matrix = new List<string>();
                for (int j = 0; j < H; j++)
                {
                    matrix.Add(string.Concat(firstNumbersRep[(i * H) + j] + Environment.NewLine));
                }
                result += Convert.ToInt32(numbers.Where(x => x.matrix.SequenceEqual(matrix)).First().NumberInBase10 * Math.Pow(20, calPuis--));
            }
            return Convert.ToInt32(result);
        }

        public static void Main(string[] args)
        {
            var numbers = new List<Number>();
            string[] inputs = Console.ReadLine().Split(' ');
            int L = int.Parse(inputs[0]);
            int H = int.Parse(inputs[1]);
            var listRepresentation = new List<string>();
            for (int i = 0; i < H; i++)
            {
                listRepresentation.Add(Console.ReadLine());
            }

            var numberOfNumber = listRepresentation.First().Length / L;
            for (int i = 0; i < numberOfNumber; i++)
            {
                var matrix = new List<string>();
                for (int j = 0; j < H; j++)
                {
                    matrix.Add(string.Concat(listRepresentation[j].Skip(i * L).Take(L)) + Environment.NewLine);
                }
                numbers.Add(new Number(i, matrix));
            }

            var first = GetNumber(H, numbers);

            var second = GetNumber(H, numbers);

            string operation = Console.ReadLine();
            long resultOperation = 0;

            switch (operation)
            {
                case "+":
                    resultOperation = first + second;
                    break;

                case "-":
                    resultOperation = first - second;
                    break;

                case "/":
                    resultOperation = first / second;
                    break;

                case "*":
                    resultOperation = first * second;
                    break;

                default:
                    break;
            }

            var resultBase20 = Encode(resultOperation, 20);
            foreach (var num in resultBase20)
            {
                Console.Write(numbers.Where(x => x.NumberInBase10 == num).First().GetRepresentation());
            }
        }

        private static IEnumerable<long> Encode(long input, int baseToConvert)
        {
            if (input < 0) throw new ArgumentOutOfRangeException("input", input, "input cannot be negative");

            var result = new Stack<long>();
            if (input == 0) result.Push(0);

            while (input != 0)
            {
                result.Push(input % baseToConvert);
                input /= baseToConvert;
            }
            return result;
        }
    }
}