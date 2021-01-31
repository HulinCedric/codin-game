namespace CodinGame.Puzzles.Medium.War
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Solution
    {
        public static int GetCardValue(string card)
        {
            if (int.TryParse(card.Substring(0, card.Length - 1), out var val))
                return val;
            else
            {
                if (card[0] == 'J')
                    return 11;
                else if (card[0] == 'Q')
                    return 12;
                else if (card[0] == 'K')
                    return 13;
                else if (card[0] == 'A')
                    return 14;
            }
            throw new InvalidOperationException();
        }

        public static int GetWinner(string card1, string card2)
        {
            if (GetCardValue(card1) > GetCardValue(card2))
                return 1;
            else if (GetCardValue(card1) < GetCardValue(card2))
                return 2;
            else
                return 0;
        }

        public static void Main(string[] args)
        {
            LinkedList<string> cardsP1 = new LinkedList<string>();
            LinkedList<string> cardsP2 = new LinkedList<string>();
            LinkedList<string> cardsOnTableP1 = new LinkedList<string>();
            LinkedList<string> cardsOnTableP2 = new LinkedList<string>();


            int n = int.Parse(Console.ReadLine()); // the number of cards for player 1
            for (int i = 0; i < n; i++)
            {
                string cardp1 = Console.ReadLine(); // the n cards of player 1
                cardsP1.AddLast(cardp1);
            }
            int m = int.Parse(Console.ReadLine()); // the number of cards for player 2
            for (int i = 0; i < m; i++)
            {
                string cardp2 = Console.ReadLine(); // the m cards of player 2
                cardsP2.AddLast(cardp2);
            }

            int rounds = 0;
            while (true)
            {
                // The game ends when one player no longer has cards
                if (!cardsP1.Any())
                {
                    Console.WriteLine($"2 {rounds}");
                    return;
                }
                else if (!cardsP2.Any())
                {
                    Console.WriteLine($"1 {rounds}");
                    return;
                }

                bool playingWar;
                do
                {
                    playingWar = false;

                    // Each player draws a card
                    cardsOnTableP1.AddLast(cardsP1.First.Value);
                    cardsOnTableP2.AddLast(cardsP2.First.Value);
                    cardsP1.RemoveFirst();
                    cardsP2.RemoveFirst();

                    // Play the round
                    int roundWinner = GetWinner(cardsOnTableP1.Last.Value, cardsOnTableP2.Last.Value);
                    if (roundWinner == 1)
                    {
                        foreach (var card in cardsOnTableP1)
                        {
                            cardsP1.AddLast(card);
                        }

                        foreach (var card in cardsOnTableP2)
                        {
                            cardsP1.AddLast(card);
                        }

                        cardsOnTableP1.Clear();
                        cardsOnTableP2.Clear();
                    }
                    else if (roundWinner == 2)
                    {
                        foreach (var card in cardsOnTableP1)
                        {
                            cardsP2.AddLast(card);
                        }

                        foreach (var card in cardsOnTableP2)
                        {
                            cardsP2.AddLast(card);
                        }

                        cardsOnTableP1.Clear();
                        cardsOnTableP2.Clear();
                    }
                    else // war!
                    {
                        // If we don't have enough cards => draw
                        if ((cardsP1.Count < 4) || (cardsP2.Count < 4))
                        {
                            Console.WriteLine("PAT");
                            return;
                        }

                        // Place 3 cards face down
                        for (int i = 0; i < 3; ++i)
                        {
                            cardsOnTableP1.AddLast(cardsP1.First.Value);
                            cardsOnTableP2.AddLast(cardsP2.First.Value);
                            cardsP1.RemoveFirst();
                            cardsP2.RemoveFirst();
                        }

                        // Draw the next cards
                        playingWar = true;
                    }
                } while (playingWar);

                rounds++;
            }
        }
    }
}