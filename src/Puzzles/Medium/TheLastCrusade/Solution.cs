namespace CodinGame.Puzzles.Medium.TheLastCrusade
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class AbstractPiece
    {
        protected int x;
        protected int y;

        public AbstractPiece(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static AbstractPiece GetPiece(int x, int y, int type)
        {
            switch (type)
            {
                case 1:
                    return new Piece1(x, y);

                case 2:
                    return new Piece2(x, y);

                case 3:
                    return new Piece3(x, y);

                case 4:
                    return new Piece4(x, y);

                case 5:
                    return new Piece5(x, y);

                case 6:
                    return new Piece6(x, y);

                case 7:
                    return new Piece7(x, y);

                case 8:
                    return new Piece8(x, y);

                case 9:
                    return new Piece9(x, y);

                case 10:
                    return new Piece10(x, y);

                case 11:
                    return new Piece11(x, y);

                case 12:
                    return new Piece12(x, y);

                case 13:
                    return new Piece13(x, y);

                default:
                    return new Piece0(x, y);
            }
        }

        public abstract KeyValuePair<int, int> GetLast(string pos);

        public abstract bool IsWay(string pos);
    }

    internal class Piece0 : AbstractPiece
    {
        public Piece0(int x, int y) : base(x, y)
        {
        }

        public override KeyValuePair<int, int> GetLast(string pos)
        {
            return new KeyValuePair<int, int>(x, y);
        }

        public override bool IsWay(string pos)
        {
            return false;
        }
    }

    internal class Piece1 : AbstractPiece
    {
        public Piece1(int x, int y) : base(x, y)
        {
        }

        public override KeyValuePair<int, int> GetLast(string pos)
        {
            return new KeyValuePair<int, int>(x, y + 1);
        }

        public override bool IsWay(string pos)
        {
            return true;
        }
    }

    internal class Piece10 : AbstractPiece
    {
        public Piece10(int x, int y) : base(x, y)
        {
        }

        public override KeyValuePair<int, int> GetLast(string pos)
        {
            if (string.Equals(pos, "TOP"))
                return new KeyValuePair<int, int>(x - 1, y);
            return new KeyValuePair<int, int>(x, y);
        }

        public override bool IsWay(string pos)
        {
            if (string.Equals(pos, "TOP"))
                return true;
            return false;
        }
    }

    internal class Piece11 : AbstractPiece
    {
        public Piece11(int x, int y) : base(x, y)
        {
        }

        public override KeyValuePair<int, int> GetLast(string pos)
        {
            return new KeyValuePair<int, int>(x + 1, y);
        }

        public override bool IsWay(string pos)
        {
            if (string.Equals(pos, "TOP"))
                return true;
            return false;
        }
    }

    internal class Piece12 : AbstractPiece
    {
        public Piece12(int x, int y) : base(x, y)
        {
        }

        public override KeyValuePair<int, int> GetLast(string pos)
        {
            if (string.Equals(pos, "RIGHT"))
                return new KeyValuePair<int, int>(x, y + 1);
            return new KeyValuePair<int, int>(x, y);
        }

        public override bool IsWay(string pos)
        {
            if (string.Equals(pos, "RIGHT"))
                return true;
            return false;
        }
    }

    internal class Piece13 : AbstractPiece
    {
        public Piece13(int x, int y) : base(x, y)
        {
        }

        public override KeyValuePair<int, int> GetLast(string pos)
        {
            if (string.Equals(pos, "LEFT"))
                return new KeyValuePair<int, int>(x, y + 1);
            return new KeyValuePair<int, int>(x, y);
        }

        public override bool IsWay(string pos)
        {
            if (string.Equals(pos, "LEFT"))
                return true;
            return false;
        }
    }

    internal class Piece2 : AbstractPiece
    {
        public Piece2(int x, int y) : base(x, y)
        {
        }

        public override KeyValuePair<int, int> GetLast(string pos)
        {
            if (string.Equals(pos, "LEFT"))
                return new KeyValuePair<int, int>(x + 1, y);
            if (string.Equals(pos, "RIGHT"))
                return new KeyValuePair<int, int>(x - 1, y);
            return new KeyValuePair<int, int>(x, y);
        }

        public override bool IsWay(string pos)
        {
            if (string.Equals(pos, "TOP"))
                return false;
            return true;
        }
    }

    internal class Piece3 : AbstractPiece
    {
        public Piece3(int x, int y) : base(x, y)
        {
        }

        public override KeyValuePair<int, int> GetLast(string pos)
        {
            return new KeyValuePair<int, int>(x, y + 1);
        }

        public override bool IsWay(string pos)
        {
            if (string.Equals(pos, "TOP"))
                return true;
            return false;
        }
    }

    internal class Piece4 : AbstractPiece
    {
        public Piece4(int x, int y) : base(x, y)
        {
        }

        public override KeyValuePair<int, int> GetLast(string pos)
        {
            if (string.Equals(pos, "TOP"))
                return new KeyValuePair<int, int>(x - 1, y);
            if (string.Equals(pos, "RIGHT"))
                return new KeyValuePair<int, int>(x, y + 1);
            return new KeyValuePair<int, int>(x, y);
        }

        public override bool IsWay(string pos)
        {
            if (string.Equals(pos, "TOP") || string.Equals(pos, "RIGHT"))
                return true;
            return false;
        }
    }

    internal class Piece5 : AbstractPiece
    {
        public Piece5(int x, int y) : base(x, y)
        {
        }

        public override KeyValuePair<int, int> GetLast(string pos)
        {
            if (string.Equals(pos, "TOP"))
                return new KeyValuePair<int, int>(x + 1, y);
            if (string.Equals(pos, "LEFT"))
                return new KeyValuePair<int, int>(x, y + 1);
            return new KeyValuePair<int, int>(x, y);
        }

        public override bool IsWay(string pos)
        {
            if (string.Equals(pos, "TOP") || string.Equals(pos, "LEFT"))
                return true;
            return false;
        }
    }

    internal class Piece6 : AbstractPiece
    {
        public Piece6(int x, int y) : base(x, y)
        {
        }

        public override KeyValuePair<int, int> GetLast(string pos)
        {
            if (string.Equals(pos, "LEFT"))
                return new KeyValuePair<int, int>(x + 1, y);
            if (string.Equals(pos, "RIGHT"))
                return new KeyValuePair<int, int>(x - 1, y);
            return new KeyValuePair<int, int>(x, y);
        }

        public override bool IsWay(string pos)
        {
            if (string.Equals(pos, "LEFT") || string.Equals(pos, "RIGHT"))
                return true;
            return false;
        }
    }

    internal class Piece7 : AbstractPiece
    {
        public Piece7(int x, int y) : base(x, y)
        {
        }

        public override KeyValuePair<int, int> GetLast(string pos)
        {
            if (string.Equals(pos, "TOP") || string.Equals(pos, "RIGHT"))
                return new KeyValuePair<int, int>(x, y + 1);

            return new KeyValuePair<int, int>(x, y);
        }

        public override bool IsWay(string pos)
        {
            if (string.Equals(pos, "TOP") || string.Equals(pos, "RIGHT"))
                return true;
            return false;
        }
    }

    internal class Piece8 : AbstractPiece
    {
        public Piece8(int x, int y) : base(x, y)
        {
        }

        public override KeyValuePair<int, int> GetLast(string pos)
        {
            if (string.Equals(pos, "LEFT") || string.Equals(pos, "RIGHT"))
                return new KeyValuePair<int, int>(x, y + 1);
            return new KeyValuePair<int, int>(x, y);
        }

        public override bool IsWay(string pos)
        {
            if (string.Equals(pos, "LEFT") || string.Equals(pos, "RIGHT"))
                return true;
            return false;
        }
    }

    internal class Piece9 : AbstractPiece
    {
        public Piece9(int x, int y) : base(x, y)
        {
        }

        public override KeyValuePair<int, int> GetLast(string pos)
        {
            if (string.Equals(pos, "TOP") || string.Equals(pos, "LEFT"))
                return new KeyValuePair<int, int>(x, y + 1);
            return new KeyValuePair<int, int>(x, y);
        }

        public override bool IsWay(string pos)
        {
            if (string.Equals(pos, "TOP") || string.Equals(pos, "LEFT"))
                return true;
            return false;
        }
    }

    internal class Player
    {
        private static void Main(string[] args)
        {
            string[] inputs;
            inputs = Console.ReadLine().Split(' ');
            int W = int.Parse(inputs[0]); // number of columns.
            int H = int.Parse(inputs[1]); // number of rows.
            AbstractPiece[,] matrice = new AbstractPiece[W, H];
            for (int i = 0; i < H; i++)
            {
                // represents a line in the grid and contains W integers. Each integer represents one room of a given type.
                var LINE = Console.ReadLine().Split(' ').Select(x => int.Parse(x.ToString())).ToList();

                for (int j = 0; j < LINE.Count(); j++)
                {
                    Console.Error.WriteLine(LINE.ElementAt(j));
                    matrice[j, i] = AbstractPiece.GetPiece(j, i, LINE.ElementAt(j));
                }
            }

            int EX = int.Parse(Console.ReadLine()); // the coordinate along the X axis of the exit (not useful for this first mission, but must be read).

            // game loop
            while (true)
            {
                inputs = Console.ReadLine().Split(' ');

                int XI = int.Parse(inputs[0]);
                int YI = int.Parse(inputs[1]);
                string POS = inputs[2];
                Console.Error.WriteLine(XI + " " + YI + " " + POS);
                var coord = matrice[XI, YI].GetLast(POS);
                
                Console.WriteLine(coord.Key + " " + coord.Value);
            }
        }
    }
}