namespace CodinGame.Puzzles.Medium.TheGift
{
    using System;

    internal class Solution
    {
        private static void Main(string[] args)
        {
            var oodNumber = int.Parse(Console.ReadLine());
            var giftPrice = int.Parse(Console.ReadLine());

            // holds the budget for each ood
            var oodBudgets = new int[oodNumber];

            // total buget for all oods combined
            var totalBudget = 0;

            for (int i = 0; i < oodNumber; i++)
            {
                oodBudgets[i] = int.Parse(Console.ReadLine());
                totalBudget += oodBudgets[i];
            }

            if (totalBudget < giftPrice)
            {
                // the total budget is not enough to cover the cost of the gift
                Console.WriteLine("IMPOSSIBLE");
                return;
            }

            // if we have enough money, let's see how to best distribute the cost
            var oodsLeft = oodNumber;                           // keeps track of how many oods still have monely left
            var oodPays = new int[oodNumber];                   // keeps track of how much each ood will pay

            // as long as we haven't yet covered the cost of the gift,
            // and the cost requires more than 1 unit from each ood that still has money left
            // .. keep splitting the cost ..
            while (giftPrice > oodsLeft)
            {
                var fair = giftPrice / oodsLeft;                // what would be an ideal fair split given # of oods that still have money

                for (var i = oodNumber - 1; i >= 0; i--)
                {
                    if (oodBudgets[i] == 0) continue;           // this ood is out of money, skip him..

                    var pays = Math.Min(oodBudgets[i], fair);   // how much will this ood contribute?

                    oodBudgets[i] -= pays;                      // update his budget, after payment
                    if (oodBudgets[i] == 0) oodsLeft--;         // if he's out of money now, reduce # of ood left
                    giftPrice -= pays;                          // also reduce the remainder cost

                    oodPays[i] += pays;                         // and update the total payed by this ood
                }
            }

            // if we're here, and there is still some cost left,
            // it must only require unit money from each ood left
            for (var i = oodNumber - 1; i >= 0 && giftPrice > 0; i--)
            {
                if (oodBudgets[i] == 0) continue;               // this ood is out of money, skip'm..

                oodBudgets[i]--;                                // pays a unit..
                giftPrice--;                                    //   cost is reduced
                oodPays[i]++;                                   //   and update his total amount to pay
            }

            // we have to list of how much each ood pays
            // but we have to present it in ascending order, so sort it..
            Array.Sort(oodPays);

            // we're done (just print out each payment)
            for (int i = 0; i < oodNumber; i++)
                Console.WriteLine(oodPays[i]);
        }
    }
}