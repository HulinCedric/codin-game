namespace CodinGame.Puzzles.Medium.ThereIsNoSpoon
{
    using System;

    class Player
    {
        private static bool[,] matrixAlreadyFound;

        private static void FindSiblingCell(bool[,] matrix, int rowIndex, int columnIndex)
        {
            matrixAlreadyFound[rowIndex, columnIndex] = true;
            Console.Write("{0} {1} ", columnIndex, rowIndex);

            var rightFind = false;
            int righIndex = columnIndex + 1;
            for (; righIndex < matrix.GetLength(1); righIndex++)
            {
                if (matrix[rowIndex, righIndex])
                {
                    rightFind = true;
                    break;
                }
            }

            var bottomFind = false;
            var bottomIndex = rowIndex + 1;
            for (; bottomIndex < matrix.GetLength(0); bottomIndex++)
            {
                if (matrix[bottomIndex, columnIndex])
                {
                    bottomFind = true;
                    break;
                }
            }

            if (!rightFind)
            {
                righIndex = -1;
                rowIndex = -1;
            }

            if (!bottomFind)
            {
                columnIndex = -1;
                bottomIndex = -1;
            }

            Console.Write(righIndex + " " + rowIndex + " ");
            Console.WriteLine(columnIndex + " " + bottomIndex);

            if (rightFind && !matrixAlreadyFound[rowIndex, righIndex])
            {
                FindSiblingCell(matrix, rowIndex, righIndex);
            }

            if (bottomFind && !matrixAlreadyFound[bottomIndex, columnIndex])
            {
                FindSiblingCell(matrix, bottomIndex, columnIndex);
            }
        }

        static void Main(string[] args)
        {
            var width = int.Parse(Console.ReadLine());
            var height = int.Parse(Console.ReadLine());
            var matrix = new bool[height, width];
            matrixAlreadyFound = new bool[height, width];

            for (int rowIndex = 0; rowIndex < height; rowIndex++)
            {
                var line = Console.ReadLine().ToCharArray();
                for (int columnIndex = 0; columnIndex < line.Length; columnIndex++)
                {
                    matrix[rowIndex, columnIndex] = line[columnIndex] == '.' ? false : true;
                }
            }

            for (int rowIndex = 0; rowIndex < matrix.GetLength(0); rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < matrix.GetLength(1); columnIndex++)
                {
                    if (matrix[rowIndex, columnIndex] && !matrixAlreadyFound[rowIndex, columnIndex])
                        FindSiblingCell(matrix, rowIndex, columnIndex);
                }
            }
        }
    }
}