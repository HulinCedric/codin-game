using System;
using System.Collections.Generic;
using System.Linq;

namespace CondinGame.Contests.SpringChallenge2021
{
    internal class Cell
    {
        public int index;
        public int[] neighbours;
        public int richess;

        public Cell(int index, int richess, int[] neighbours)
        {
            this.index = index;
            this.richess = richess;
            this.neighbours = neighbours;
        }
    }

    internal class Tree
    {
        public int cellIndex;
        public bool isDormant;
        public bool isMine;
        public int size;

        public Tree(int cellIndex, int size, bool isMine, bool isDormant)
        {
            this.cellIndex = cellIndex;
            this.size = size;
            this.isMine = isMine;
            this.isDormant = isDormant;
        }
    }

    internal class Action
    {
        public const string WAIT = "WAIT";
        public const string SEED = "SEED";
        public const string GROW = "GROW";
        public const string COMPLETE = "COMPLETE";
        public int sourceCellIdx;
        public int targetCellIdx;

        public string type;

        public Action(string type, int sourceCellIdx, int targetCellIdx)
        {
            this.type = type;
            this.targetCellIdx = targetCellIdx;
            this.sourceCellIdx = sourceCellIdx;
        }

        public Action(string type, int targetCellIdx)
            : this(type, 0, targetCellIdx)
        {
        }

        public Action(string type)
            : this(type, 0, 0)
        {
        }

        public static Action Parse(string action)
        {
            var parts = action.Split(" ");
            return parts[0] switch
            {
                WAIT => new Action(WAIT),
                SEED => new Action(SEED, int.Parse(parts[1]), int.Parse(parts[2])),
                GROW => new Action(GROW, int.Parse(parts[1])),
                COMPLETE => new Action(COMPLETE, int.Parse(parts[1])),
                _ => new Action(parts[0], int.Parse(parts[1]))
            };
        }

        public override string ToString()
            => type switch
            {
                WAIT => type,
                SEED => $"{type} {sourceCellIdx} {targetCellIdx}",
                GROW => $"{type} {targetCellIdx}",
                COMPLETE => $"{type} {targetCellIdx}",
                _ => $"{type} {targetCellIdx}"
            };
    }

    internal class Game
    {
        public const int SunPointsRequiredToCompleteTreeLifecycle = 4;
        public List<Cell> board;
        public int day;
        public int myScore, opponentScore;
        public int mySun, opponentSun;
        public int nutrients;
        public bool opponentIsWaiting;
        public List<Action> possibleActions;
        public List<Tree> trees;

        public Game()
        {
            board = new List<Cell>();
            possibleActions = new List<Action>();
            trees = new List<Tree>();
        }

        public Action GetNextAction()
        {
            var possibleSeedActions = possibleActions.Where(action => action.type == Action.SEED).ToList();
            var possibleGrowActions = possibleActions.Where(action => action.type == Action.GROW).ToList();

            #region Take Central positon

            // TODO Strategy => Place the trees at the richness cells and take position.
            if (possibleSeedActions.Any())
            {
                var richnessSeedCellsIndexes =
                    (from boardCell in board
                        join mineTreeCellIndex in possibleSeedActions.Select(a => a.targetCellIdx) on boardCell.index
                            equals
                            mineTreeCellIndex
                        where boardCell.richess == 3
                        select boardCell.index).ToList();

                if (richnessSeedCellsIndexes.Any())
                    // TODO Compute the best candidate action depending sun cost  
                    return possibleSeedActions
                        .Last(a => a.targetCellIdx == richnessSeedCellsIndexes.Last());
            }

            if (possibleGrowActions.Any())
            {
                var richnessGrowCellsIndexes =
                    (from boardCell in board
                        join mineTreeCellIndex in possibleGrowActions.Select(a => a.targetCellIdx) on boardCell.index
                            equals
                            mineTreeCellIndex
                        where boardCell.richess == 3
                        select boardCell.index).ToList();

                if (richnessGrowCellsIndexes.Any())
                    // TODO Compute the best candidate action depending sun cost  
                    return possibleGrowActions
                        .Last(a => a.targetCellIdx == richnessGrowCellsIndexes.Last());
            }

            if (!IsThereAnyMineTreeInRichnessCells())

                if (possibleGrowActions.Any())
                {
                    var richnessGrowCellsIndexes =
                        (from boardCell in board
                            join mineTreeCellIndex in possibleGrowActions.Select(a => a.targetCellIdx) on boardCell
                                    .index
                                equals
                                mineTreeCellIndex
                            where boardCell.richess == 2
                            select boardCell.index).ToList();

                    if (richnessGrowCellsIndexes.Any())
                        // TODO Compute the best candidate action depending sun cost  
                        return possibleGrowActions
                            .Last(a => a.targetCellIdx == richnessGrowCellsIndexes.Last());
                }

            #endregion


            // TODO Compute action cost in sun point

            if (mySun >= SunPointsRequiredToCompleteTreeLifecycle)
            {
                var collectableTrees = GetCollectableTreeCellIndexes().ToList();
                if (collectableTrees.Any())
                {
                    var richnessCells = GetCellsIndexesOrderByRichness(collectableTrees);
                    return new Action(Action.COMPLETE, richnessCells.Last());
                }
            }

            // TODO Compute how many sun point ?
            if (mySun >= SunPointsRequiredToCompleteTreeLifecycle)
            {
                var treeOrderedBySize = GetMineTreeCellIndexesOrderBySize().ToList();
                var treeOrderedBySizeAndByRichness = GetCellsIndexesOrderByRichness(treeOrderedBySize);
                return new Action(Action.GROW, treeOrderedBySizeAndByRichness.Last());
            }

            if (possibleSeedActions.Any())
            {
                var seedCellsIndexesOrderByRichness =
                    GetCellsIndexesOrderByRichness(possibleSeedActions.Select(a => a.targetCellIdx));

                // TODO Compute the best candidate action depending sun cost  
                return possibleSeedActions
                    .Last(a => a.targetCellIdx == seedCellsIndexesOrderByRichness.Last());
            }


            return new Action(Action.WAIT);
        }

        private IEnumerable<int> GetCellsIndexesOrderByRichness(IEnumerable<int> mineTreeCellIndexes)
            => from boardCell in board
                join mineTreeCellIndex in mineTreeCellIndexes on boardCell.index equals mineTreeCellIndex
                orderby boardCell.richess
                select boardCell.index;

        private IEnumerable<int> GetCollectableTreeCellIndexes()
            => GetMineTreeCellIndexes(3);

        private IEnumerable<int> GetMineTreeCellIndexes(int treeSize)
            => from tree in trees
                where tree.isMine && tree.size == treeSize
                select tree.cellIndex;

        private IEnumerable<int> GetMineTreeCellIndexesOrderBySize()
            => from tree in trees
                where tree.isMine
                orderby tree.size
                select tree.cellIndex;


        private IEnumerable<Cell> GetMineTreeCellsInRichnessCells()
            => from tree in trees
                join boardCell in board on tree.cellIndex equals boardCell.index
                where tree.isMine && boardCell.richess == 3
                select boardCell;

        private bool IsThereAnyMineTreeInRichnessCells()
            => GetMineTreeCellsInRichnessCells().Any();
    }

    internal class Player
    {
        private static void Main(string[] args)
        {
            string[] inputs;

            var game = new Game();

            var numberOfCells = int.Parse(Console.ReadLine()); // 37
            for (var i = 0; i < numberOfCells; i++)
            {
                inputs = Console.ReadLine().Split(' ');
                var index = int.Parse(inputs[0]); // 0 is the center cell, the next cells spiral outwards
                var richness = int.Parse(inputs[1]); // 0 if the cell is unusable, 1-3 for usable cells
                var neigh0 = int.Parse(inputs[2]); // the index of the neighbouring cell for each direction
                var neigh1 = int.Parse(inputs[3]);
                var neigh2 = int.Parse(inputs[4]);
                var neigh3 = int.Parse(inputs[5]);
                var neigh4 = int.Parse(inputs[6]);
                var neigh5 = int.Parse(inputs[7]);
                int[] neighs =
                {
                    neigh0, neigh1, neigh2, neigh3, neigh4, neigh5
                };
                var cell = new Cell(index, richness, neighs);
                game.board.Add(cell);
            }

            // game loop
            while (true)
            {
                game.day = int.Parse(Console.ReadLine()); // the game lasts 24 days: 0-23
                game.nutrients = int.Parse(Console.ReadLine()); // the base score you gain from the next COMPLETE action
                inputs = Console.ReadLine().Split(' ');
                game.mySun = int.Parse(inputs[0]); // your sun points
                game.myScore = int.Parse(inputs[1]); // your current score
                inputs = Console.ReadLine().Split(' ');
                game.opponentSun = int.Parse(inputs[0]); // opponent's sun points
                game.opponentScore = int.Parse(inputs[1]); // opponent's score
                game.opponentIsWaiting = inputs[2] != "0"; // whether your opponent is asleep until the next day

                game.trees.Clear();
                var numberOfTrees = int.Parse(Console.ReadLine()); // the current amount of trees
                for (var i = 0; i < numberOfTrees; i++)
                {
                    inputs = Console.ReadLine().Split(' ');
                    var cellIndex = int.Parse(inputs[0]); // location of this tree
                    var size = int.Parse(inputs[1]); // size of this tree: 0-3
                    var isMine = inputs[2] != "0"; // 1 if this is your tree
                    var isDormant = inputs[3] != "0"; // 1 if this tree is dormant
                    var tree = new Tree(cellIndex, size, isMine, isDormant);
                    game.trees.Add(tree);
                }

                game.possibleActions.Clear();
                var numberOfPossibleMoves = int.Parse(Console.ReadLine());
                for (var i = 0; i < numberOfPossibleMoves; i++)
                {
                    var possibleMove = Console.ReadLine();
                    Console.Error.WriteLine(possibleMove);
                    game.possibleActions.Add(Action.Parse(possibleMove));
                }

                var action = game.GetNextAction();
                Console.WriteLine(action);
            }
        }
    }
}