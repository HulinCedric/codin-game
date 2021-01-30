namespace CodinGame.Puzzles.Medium.MarsLander
{
    using System;

    public class MarsLander
    {
        private const double GRAVITY = 3.711;
        private const int MAX_DX = 20;
        private const int MAX_DY = 40;
        private const int SPEED_MARGIN = 5;
        private const int Y_MARGIN = 20;
        private int targetL, targetR, targetY;
        private int x, y, dx, dy, fuel, angle, power;

        /// <summary>
        /// Gets the angle to aim target.
        /// </summary>
        /// <returns></returns>
        public int GetAngleToAimTarget()
        {
            int angle = (int)(Math.Acos(GRAVITY / 4.0) * (180.0 / Math.PI));
            if (x < targetL)
                return -angle;
            else if (targetR < x)
                return angle;
            else
                return 0;
        }

        /// <summary>
        /// Gets the angle to slow.
        /// </summary>
        /// <returns></returns>
        public int GetAngleToSlow()
        {
            double speed = Math.Sqrt(dx * dx + dy * dy);
            return (int)(Math.Asin((double)dx / speed) * (180.0 / Math.PI));
        }

        /// <summary>
        /// Determines whether [has safe speed].
        /// </summary>
        /// <returns></returns>
        public bool HasSafeSpeed()
        {
            return Math.Abs(dx) <= MAX_DX - SPEED_MARGIN &&
                   Math.Abs(dy) <= MAX_DY - SPEED_MARGIN;
        }

        /// <summary>
        /// Initializes the specified x.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="dx">The dx.</param>
        /// <param name="dy">The dy.</param>
        /// <param name="fuel">The fuel.</param>
        /// <param name="angle">The angle.</param>
        /// <param name="power">The power.</param>
        public void Init(int x, int y, int dx, int dy, int fuel, int angle, int power)
        {
            this.x = x;
            this.y = y;
            this.dx = dx;
            this.dy = dy;
            this.fuel = fuel;
            this.angle = angle;
            this.power = power;
        }

        /// <summary>
        /// Determines whether this instance is finishing.
        /// </summary>
        /// <returns></returns>
        public bool IsFinishing()
        {
            return y < targetY + Y_MARGIN;
        }

        /// <summary>
        /// Determines whether [is goes in wrong direction].
        /// </summary>
        /// <returns></returns>
        public bool IsGoesInWrongDirection()
        {
            return (x < targetL && dx < 0) || (targetR < x && dx > 0);
        }

        /// <summary>
        /// Determines whether [is goes too fast horizontally].
        /// </summary>
        /// <returns></returns>
        public bool IsGoesTooFastHorizontally()
        {
            return Math.Abs(dx) > 4 * MAX_DX;
        }

        /// <summary>
        /// Determines whether [is goes too slow horizontally].
        /// </summary>
        /// <returns></returns>
        public bool IsGoesTooSlowHorizontally()
        {
            return Math.Abs(dx) < 2 * MAX_DX;
        }

        /// <summary>
        /// Determines whether [is over target].
        /// </summary>
        /// <returns></returns>
        public bool IsOverTarget()
        {
            return targetL <= x && x <= targetR;
        }

        /// <summary>
        /// Powers to hover.
        /// </summary>
        /// <returns>The thrust power needed to aim a null vertical speed</returns>
        public int PowerToHover()
        {
            return (dy >= 0) ? 3 : 4;
        }

        /// <summary>
        /// Sets the target.
        /// </summary>
        /// <param name="targetL">The target l.</param>
        /// <param name="targetR">The target r.</param>
        /// <param name="targetY">The target y.</param>
        public void SetTarget(int targetL, int targetR, int targetY)
        {
            this.targetL = targetL;
            this.targetR = targetR;
            this.targetY = targetY;
        }
    }

    public class Player
    {
        public static void Main(string[] args)
        {
            MarsLander ship = new MarsLander();
            string[] inputs;
            int surfaceN = int.Parse(Console.ReadLine()); // the number of points used to draw the surface of Mars.

            int prevX = -1;
            int prevY = -1;
            for (int i = 0; i < surfaceN; i++)
            {
                inputs = Console.ReadLine().Split(' ');
                int landX = int.Parse(inputs[0]); // X coordinate of a surface point. (0 to 6999)
                int landY = int.Parse(inputs[1]); // Y coordinate of a surface point. By linking all the points together in a sequential fashion, you form the surface of Mars.
                if (landY == prevY)
                {
                    ship.SetTarget(prevX, landX, landY);
                }
                else
                {
                    prevX = landX;
                    prevY = landY;
                }
            }

            // game loop
            while (true)
            {
                // The flight follows 2 steps :
                // - first the rover goes over the landing zone by
                //     -- slowing if it goes faster than 4*MAX_HS, or in the wrong direction
                //     -- accelerating while hovering until it reaches 2*MAX_HS if it goes in the right direction
                //     -- waiting hovering if it has a speed between 2*MAX_HS and 4*MAX_HS

                // - then it slows down to meet speed specification (going back
                // to step 1 if it goes out of the landing zone)
                inputs = Console.ReadLine().Split(' ');
                int X = int.Parse(inputs[0]);
                int Y = int.Parse(inputs[1]);
                int hSpeed = int.Parse(inputs[2]); // the horizontal speed (in m/s), can be negative.
                int vSpeed = int.Parse(inputs[3]); // the vertical speed (in m/s), can be negative.
                int fuel = int.Parse(inputs[4]); // the quantity of remaining fuel in liters.
                int rotate = int.Parse(inputs[5]); // the rotation angle in degrees (-90 to 90).
                int power = int.Parse(inputs[6]); // the thrust power (0 to 4).

                ship.Init(X, Y, hSpeed, vSpeed, fuel, rotate, power);

                if (!ship.IsOverTarget())
                {
                    if (ship.IsGoesInWrongDirection() || ship.IsGoesTooFastHorizontally())
                        Console.WriteLine(ship.GetAngleToSlow() + " 4");
                    else if (ship.IsGoesTooSlowHorizontally())
                        Console.WriteLine(ship.GetAngleToAimTarget() + " 4");
                    else
                        Console.WriteLine("0 " + ship.PowerToHover());
                }
                else
                {
                    if (ship.IsFinishing())
                        Console.WriteLine("0 3");
                    else if (ship.HasSafeSpeed())
                        Console.WriteLine("0 2");
                    else
                        Console.WriteLine(ship.GetAngleToSlow() + " 4");
                }
            }
        }
    }
}