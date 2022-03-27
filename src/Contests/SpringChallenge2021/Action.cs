namespace CodinGame.Contests.SpringChallenge2021
{
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
}