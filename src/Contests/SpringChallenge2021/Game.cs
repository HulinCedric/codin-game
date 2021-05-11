using System.Collections.Generic;
using System.Linq;

namespace CondinGame.Contests.SpringChallenge2021
{
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
}