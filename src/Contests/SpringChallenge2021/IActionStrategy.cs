namespace CodinGame.Contests.SpringChallenge2021
{
    internal interface IActionStrategy
    {
        Action SelectAction(Game game);
    }
}