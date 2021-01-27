namespace CodinGame.Puzzles.Easy.JackSilverTheCasino
{
    using System;
    using System.Linq;

    class Solution
    {
        static void Main(string[] args)
        {
            var rounds = int.Parse(Console.ReadLine());
            var cash = int.Parse(Console.ReadLine());
            for (var i = 0; i < rounds; i++)
            {
                var play = Console.ReadLine().Split(" ".ToCharArray());
                var ball = int.Parse(play.ElementAt(0));
                var bet = Convert.ToInt32(Math.Ceiling(cash * 0.25));
                switch (play.ElementAt(1))
                {
                    case "PLAIN":
                        cash += int.Parse(play.ElementAt(2)) == ball ? bet * 35 : -bet;
                        break;

                    case "EVEN":
                        cash += ball % 2 == 0 && ball != 0 ? bet : -bet;
                        break;

                    case "ODD":
                        cash += ball % 2 != 0 ? bet : -bet;
                        break;
                }
            }

            Console.WriteLine(cash);
        }
    }
}