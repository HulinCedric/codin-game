using System;
using System.Collections.Generic;
using System.Linq;

namespace CondinGame.Contests.SpringChallenge2021
{
    internal class Game
    {
        public const int SunPointsRequiredToCompleteTreeLifecycle = 4;
        private readonly IActionStrategy actionStrategy;
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
            actionStrategy = new CaptureCentralPlaceActionStrategy();
        }

        public IEnumerable<int> GetCellsIndexesOrderByRichness(IEnumerable<int> mineTreeCellIndexes)
            => from boardCell in board
                join mineTreeCellIndex in mineTreeCellIndexes on boardCell.index equals mineTreeCellIndex
                orderby boardCell.richess
                select boardCell.index;

        public IEnumerable<int> GetCollectableTreeCellIndexes()
            => GetMineTreeCellIndexes(3);

        public IEnumerable<int> GetMineTreeCellIndexes(int treeSize)
            => from tree in trees
                where tree.isMine && tree.size == treeSize
                select tree.cellIndex;

        public IEnumerable<int> GetMineTreeCellIndexesOrderBySize()
            => from tree in trees
                where tree.isMine
                orderby tree.size
                select tree.cellIndex;


        public IEnumerable<Cell> GetMineTreeCellsInRichnessCells()
            => from tree in trees
                join boardCell in board on tree.cellIndex equals boardCell.index
                where tree.isMine && boardCell.richess == 3
                select boardCell;


        public Action GetNextAction()
        {
            Console.Error.WriteLine($"Current Day: {day}");
            PrintPossibleActions();
            return actionStrategy.SelectAction(this);
        }

        public Tree GetTreeFromLocation(int cellIndex)
        {
            return trees.FirstOrDefault(t => t.cellIndex == cellIndex);
        }

        public bool IsThereAnyMineTreeInRichnessCells()
            => GetMineTreeCellsInRichnessCells().Any();

        private void PrintPossibleActions()
        {
            foreach (var action in possibleActions)
                Console.Error.WriteLine(action);
        }
    }
}