namespace CodinGame.Contests.SpringChallenge2021
{
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
}