namespace CodinGame.Puzzles.Medium.Bender
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    public enum Direction
    {
        South,
        East,
        North,
        West
    }

    public static class Solution
    {
        public static void Main()
        {
            var teleporteurs = new List<Tuple<int, int>>();
            string[] inputs = Console.ReadLine().Split(' ');
            var lineCount = int.Parse(inputs[0], CultureInfo.InvariantCulture);
            var columnsCount = int.Parse(inputs[1], CultureInfo.InvariantCulture);
            var area = new char[columnsCount, lineCount];
            Bender bender = null;
            for (int i = 0; i < lineCount; i++)
            {
                var row = Console.ReadLine().ToCharArray();
                for (int j = 0; j < row.Length; j++)
                {
                    area[j, i] = row[j];
                    if (row[j] == '@')
                    {
                        row[j] = ' ';
                        bender = new Bender(j, i);
                    }

                    if (row[j] == 'T')
                    {
                        teleporteurs.Add(new Tuple<int, int>(j, i));
                    }
                }
            }

            Teleporteur.Teleporteurs = teleporteurs;

            var directions = new List<string>();
            var direction = string.Empty;
            do
            {
                direction = bender.GetNextDirection(area);

                directions.Add(direction);
            }
            while (!string.IsNullOrEmpty(direction) && !direction.Equals("LOOP"));

            if (directions.Contains("LOOP"))
            {
                Console.WriteLine("LOOP");
            }
            else
            {
                foreach (var d in directions)
                {
                    Console.WriteLine(d);
                }
            }
        }
    }

    public static class Teleporteur
    {
        public static IList<Tuple<int, int>> Teleporteurs { get; set; }

        public static Tuple<int, int> GetTeleporteur(Tuple<int, int> teleporteur)
        {
            foreach (var t in Teleporteurs)
            {
                if (teleporteur.Item1 == t.Item1 && teleporteur.Item2 == t.Item2)
                    continue;
                return t;
            }
            return null;
        }
    }

    public class Bender
    {
        private bool hasFindKillRoom = false;
        private IList<State> historicalPosition = new List<State>();
        private bool isInverse = false;
        private bool isModeCasseur = false;

        public Bender(int x, int y)
        {
            Direction = Direction.South;
            Position = new Tuple<int, int>(x, y);
        }

        public Direction Direction { get; set; }

        public Tuple<int, int> Position { get; set; }

        public string GetNextDirection(char[,] area)
        {
            if (hasFindKillRoom)
            {
                return string.Empty;
            }

            if (IsBeer(Position, area))
            {
                isModeCasseur = !isModeCasseur;
            }

            if (IsInverseur(Position, area))
            {
                isInverse = !isInverse;
            }

            if (IsTeleporteur(Position, area))
            {
                Position = Teleporteur.GetTeleporteur(Position);
            }

            var isUpdateDirection = IsUpdateDirection(Position, area);
            if (isUpdateDirection)
            {
                Direction = GetDirectionCell(area);
            }

            var nextPosition = GetNextPosition(area);
            if (nextPosition == null)
            {
                return "LOOP";
            }
            else
            {
                Position = nextPosition;
            }

            var pos = new State
            {
                Position = Position,
                Direction = Direction,
                ModeCasseur = isModeCasseur,
                Inverse = isInverse
            };

            if (historicalPosition.Where(x => x.Equals(pos)).Count() > 2)
                return "LOOP";

            historicalPosition.Add(pos);

            return Direction.ToString().ToUpperInvariant();
        }

        public Tuple<int, int> GetNextPosition(char[,] area)
        {
            var isObstacle = true;
            var countTry = 0;
            var countTryMax = 5;
            Tuple<int, int> nextPosition = null;
            var hasBeginFindPositonObstacle = false;
            while (isObstacle && countTry < countTryMax)
            {
                nextPosition = GetTheoricalPositon();

                hasFindKillRoom = IsKillRoom(nextPosition, area);
                if (hasFindKillRoom)
                {
                    return nextPosition;
                }

                isObstacle = IsObstacle(nextPosition, area);

                if (isObstacle)
                {
                    if (!hasBeginFindPositonObstacle)
                    {
                        hasBeginFindPositonObstacle = true;
                        Direction = isInverse ? Direction.South : Direction.West;
                    }

                    UpdateNextDirection();
                }

                countTry++;
            }

            return isObstacle ? null : nextPosition;
        }

        private Direction GetDirectionCell(char[,] area)
        {
            if (!IsValid(Position, area))
            {
                throw new InvalidOperationException();
            }

            return (area[Position.Item1, Position.Item2]) switch
            {
                'N' => Direction.North,
                'E' => Direction.East,
                'W' => Direction.West,
                'S' => Direction.South,
                _ => throw new InvalidOperationException(),
            };
        }

        private Tuple<int, int> GetTheoricalPositon()
            => Direction switch
            {
                Direction.South => new Tuple<int, int>(Position.Item1, Position.Item2 + 1),
                Direction.East => new Tuple<int, int>(Position.Item1 + 1, Position.Item2),
                Direction.North => new Tuple<int, int>(Position.Item1, Position.Item2 - 1),
                Direction.West => new Tuple<int, int>(Position.Item1 - 1, Position.Item2),
                _ => throw new InvalidOperationException(),
            };

        private bool IsBeer(Tuple<int, int> position, char[,] area)
        {
            if (!IsValid(position, area))
            {
                return false;
            }

            if (area[position.Item1, position.Item2] == 'B')
            {
                return true;
            }

            return false;
        }

        private bool IsInverseur(Tuple<int, int> position, char[,] area)
        {
            if (!IsValid(position, area))
            {
                return false;
            }

            if (area[position.Item1, position.Item2] == 'I')
            {
                return true;
            }

            return false;
        }

        private bool IsKillRoom(Tuple<int, int> position, char[,] area)
        {
            if (!IsValid(position, area))
            {
                return false;
            }

            if (area[position.Item1, position.Item2] == '$')
            {
                return true;
            }

            return false;
        }

        private bool IsObstacle(Tuple<int, int> position, char[,] area)
        {
            if (!IsValid(position, area))
            {
                return true;
            }

            if (area[position.Item1, position.Item2] == 'X')
            {
                if (isModeCasseur)
                {
                    area[position.Item1, position.Item2] = ' ';
                    return false;
                }

                return true;
            }

            if (area[position.Item1, position.Item2] == '#')
            {
                return true;
            }

            return false;
        }

        private bool IsTeleporteur(Tuple<int, int> position, char[,] area)
        {
            if (!IsValid(position, area))
            {
                return false;
            }

            if (area[position.Item1, position.Item2] == 'T')
            {
                return true;
            }

            return false;
        }

        private bool IsUpdateDirection(Tuple<int, int> position, char[,] area)
        {
            if (!IsValid(position, area))
            {
                return false;
            }

            if (area[position.Item1, position.Item2] == 'N' ||
                area[position.Item1, position.Item2] == 'E' ||
                area[position.Item1, position.Item2] == 'W' ||
                area[position.Item1, position.Item2] == 'S')
            {
                return true;
            }

            return false;
        }

        private bool IsValid(Tuple<int, int> position, char[,] area)
        {
            if (position.Item1 < 0 || position.Item1 > area.GetLength(0))
            {
                return false;
            }

            if (position.Item2 < 0 || position.Item1 > area.GetLength(1))
            {
                return false;
            }

            return true;
        }

        private void UpdateNextDirection()
        {
            var direction = Direction;
            if (!isInverse)
            {
                direction = Direction + 1;
                if ((int)direction > 3)
                {
                    direction = 0;
                }
            }
            else
            {
                direction = Direction - 1;
                if ((int)direction < 0)
                {
                    direction = Direction.West;
                }
            }

            Direction = direction;
        }
    }

    public class State : IEquatable<State>
    {
        public Direction Direction { get; set; }
        public bool Inverse { get; set; }
        public bool ModeCasseur { get; set; }
        public Tuple<int, int> Position { get; set; }

        public bool Equals(State other)
        {
            if (other == null) return false;
            return Position.Item1 == other.Position.Item1 &&
                   Position.Item2 == other.Position.Item2 &&
                   ModeCasseur == other.ModeCasseur &&
                   Inverse == other.Inverse &&
                   Direction == other.Direction;
        }
    }
}