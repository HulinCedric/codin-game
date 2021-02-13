namespace CodinGame.Puzzles.Easy.HowTimeFlies
{
    using System;
    using System.Globalization;

    public class Solution
    {
        public static void Main(string[] args)
        {
            var begin = DateTime.ParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture);
            var end = DateTime.ParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture);

            var timeInterval = (end - begin);

            var years = (DateTime.MinValue + timeInterval).Year - 1;
            if (years > 0)
            {
                Console.Write("{0} year{1}, ", years, years > 1 ? "s" : "");
            }

            var months = (DateTime.MinValue + timeInterval).Month - 1;
            if (months > 0)
            {
                Console.Write("{0} month{1}, ", months, months > 1 ? "s" : "");
            }

            var days = timeInterval.Days;
            Console.WriteLine("total {0} days", days);
        }
    }
}