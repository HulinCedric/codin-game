namespace CodinGame.Puzzles.Hard.RollerCoaster
{
    using System;
    using System.Collections.Generic;

    public class Solution
    {
        public static void Main(string[] args)
        {
            var inputs = Console.ReadLine().Split(' ');
            var attractionPlacesCount = int.Parse(inputs[0]);
            var attractionRidesCount = int.Parse(inputs[1]);
            var groupsCount = int.Parse(inputs[2]);

            var personsGroupsCount = new int[groupsCount];
            for (int i = 0; i < groupsCount; i++)
            {
                personsGroupsCount[i] = int.Parse(Console.ReadLine());
            }

            var rideResultsByQueuePosition = new Dictionary<int, RideResult>();
            var totalDirhamsEarned = 0L;
            var queuePosition = 0;

            for (var i = 0; i < attractionRidesCount; i++)
            {
                if (!rideResultsByQueuePosition.ContainsKey(queuePosition))
                {
                    rideResultsByQueuePosition.Add(
                        queuePosition,
                        GetDirhamsEarnedAndNewQueuePositionForARide(
                            groupsCount,
                            personsGroupsCount,
                            queuePosition,
                            attractionPlacesCount));
                }

                UpdateDirhamsAndQueuePosition(
                       rideResultsByQueuePosition[queuePosition],
                       ref totalDirhamsEarned,
                       ref queuePosition);
            }

            Console.WriteLine(totalDirhamsEarned);
        }

        private static void UpdateDirhamsAndQueuePosition(
            RideResult rideResult,
            ref long dirhamsCount,
            ref int currentQueuePosition)
        {
            dirhamsCount += rideResult.DirhamsEarned;
            currentQueuePosition = rideResult.QueuePosition;
        }

        private static RideResult GetDirhamsEarnedAndNewQueuePositionForARide(
            int groupsCount,
            int[] personsGroupsCount,
            int queuePosition,
            int attractionAvailablePlacesCount)
        {
            var groupsInAttractionCount = 0;
            var dirhamsEarned = 0L;

            while (attractionAvailablePlacesCount >= personsGroupsCount[queuePosition])
            {
                var isAllGroupsInAttraction = groupsInAttractionCount++ >= groupsCount;
                if (isAllGroupsInAttraction)
                    break;

                var personsCount = personsGroupsCount[queuePosition];
                dirhamsEarned += personsCount;
                attractionAvailablePlacesCount -= personsCount;

                if (++queuePosition >= groupsCount)
                    queuePosition = 0;
            }

            return new RideResult(dirhamsEarned, queuePosition);
        }
    }

    public class RideResult
    {
        public long DirhamsEarned { get; private set; }
        public int QueuePosition { get; private set; }

        public RideResult(long dirhamsEarned, int queuePosition)
        {
            DirhamsEarned = dirhamsEarned;
            QueuePosition = queuePosition;
        }
    }
}