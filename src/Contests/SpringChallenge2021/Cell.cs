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
}