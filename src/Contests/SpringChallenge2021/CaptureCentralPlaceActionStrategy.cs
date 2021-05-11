using System.Linq;

namespace CondinGame.Contests.SpringChallenge2021
{
    internal class CaptureCentralPlaceActionStrategy
        : IActionStrategy
    {
        public Action SelectAction(Game game)
        {
            var possibleSeedActions = game.possibleActions.Where(action => action.type == Action.SEED).ToList();
            var possibleGrowActions = game.possibleActions.Where(action => action.type == Action.GROW).ToList();

            #region Take Central positon

            // TODO Strategy => Place the trees at the richness cells and take position.
            if (possibleSeedActions.Any())
            {
                var richnessSeedCellsIndexes =
                    (from boardCell in game.board
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
                    (from boardCell in game.board
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

            if (!game.IsThereAnyMineTreeInRichnessCells())

                if (possibleGrowActions.Any())
                {
                    var richnessGrowCellsIndexes =
                        (from boardCell in game.board
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

            if (game.mySun >= Game.SunPointsRequiredToCompleteTreeLifecycle)
            {
                var collectableTrees = game.GetCollectableTreeCellIndexes().ToList();
                if (collectableTrees.Any())
                {
                    var richnessCells = game.GetCellsIndexesOrderByRichness(collectableTrees);
                    return new Action(Action.COMPLETE, richnessCells.Last());
                }
            }

            // TODO Compute how many sun point ?
            if (game.mySun >= Game.SunPointsRequiredToCompleteTreeLifecycle)
            {
                var treeOrderedBySize = game.GetMineTreeCellIndexesOrderBySize().ToList();
                var treeOrderedBySizeAndByRichness = game.GetCellsIndexesOrderByRichness(treeOrderedBySize);
                return new Action(Action.GROW, treeOrderedBySizeAndByRichness.Last());
            }

            if (possibleSeedActions.Any())
            {
                var seedCellsIndexesOrderByRichness =
                    game.GetCellsIndexesOrderByRichness(possibleSeedActions.Select(a => a.targetCellIdx));

                // TODO Compute the best candidate action depending sun cost  
                return possibleSeedActions
                    .Last(a => a.targetCellIdx == seedCellsIndexesOrderByRichness.Last());
            }


            return new Action(Action.WAIT);
        }
    }
}