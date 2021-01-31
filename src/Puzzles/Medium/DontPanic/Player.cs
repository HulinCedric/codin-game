namespace CodinGame.Puzzles.Medium.DontPanic
{
    using System;

    public class Player
    {
        private static int leadFloor = -1;
        private static int leadPos = -1;
        private static string leadDirection = string.Empty;
        private static int up = -1;

        private static void UpdateLead(int cloneFloor, int clonePos, string direction)
        {
            leadFloor = cloneFloor;
            leadPos = clonePos;
            leadDirection = direction;
        }

        private static bool IsLeadGoneBlock(int cloneFloor, int clonePos, string direction, int[] elevatorPosition)
        {
            if (cloneFloor == -1)
                return false;

            var elevator = elevatorPosition[cloneFloor];
            if (clonePos == elevator)
                return false;

            if (clonePos < elevator)
            {
                if (direction == "RIGHT")
                    return false;
                else
                    return true;
            }

            if (clonePos > elevator)
            {
                if (direction == "RIGHT")
                    return true;
                else
                    return false;
            }

            return false;
        }

        private static bool IsLeadDead(int cloneFloor, int clonePos, string direction)
        {
            var result = false;
            var futurPos = -1;
            var addPos = 1;

            if (leadPos == -1)
            {
                UpdateLead(cloneFloor, clonePos, direction);

                if (leadFloor == cloneFloor && up > clonePos)
                    return true;
                return false;
            }

            if (leadDirection != direction)
            {
                UpdateLead(cloneFloor, clonePos, direction);
                return false;
            }

            if (leadFloor != cloneFloor)
            {
                addPos = 0;
            }


            futurPos = leadDirection == "RIGHT" ? leadPos + addPos : leadPos - addPos;


            UpdateLead(cloneFloor, clonePos, direction);

            if (futurPos != clonePos)
            {
                UpdateLead(leadFloor, -1, direction);
                result = true;
            }


            return result;
        }

        public static void Main(string[] args)
        {
            string[] inputs;
            inputs = Console.ReadLine().Split(' ');
            int nbFloors = int.Parse(inputs[0]); // number of floors
            int width = int.Parse(inputs[1]); // width of the area
            int nbRounds = int.Parse(inputs[2]); // maximum number of rounds
            int exitFloor = int.Parse(inputs[3]); // floor on which the exit is found
            int exitPos = int.Parse(inputs[4]); // position of the exit on its floor
            int nbTotalClones = int.Parse(inputs[5]); // number of generated clones
            int nbAdditionalElevators = int.Parse(inputs[6]); // ignore (always zero)
            int nbElevators = int.Parse(inputs[7]); // number of elevators

            var elevatorPosition = new int[nbFloors];
            elevatorPosition[exitFloor] = exitPos;

            for (int i = 0; i < nbElevators; i++)
            {
                inputs = Console.ReadLine().Split(' ');
                int elevatorFloor = int.Parse(inputs[0]); // floor on which this elevator is found
                int elevatorPos = int.Parse(inputs[1]); // position of the elevator on its floor
                elevatorPosition[elevatorFloor] = elevatorPos;
            }

            // game loop
            while (true)
            {
                inputs = Console.ReadLine().Split(' ');
                int cloneFloor = int.Parse(inputs[0]); // floor of the leading clone
                int clonePos = int.Parse(inputs[1]); // position of the leading clone on its floor
                string direction = inputs[2]; // direction of the leading clone: LEFT or RIGHT
                var action = "WAIT";

                if (IsLeadGoneBlock(cloneFloor, clonePos, direction, elevatorPosition))
                {
                    action = "BLOCK";
                }

                Console.WriteLine(action); // action: WAIT or BLOCK
            }
        }
    }
}