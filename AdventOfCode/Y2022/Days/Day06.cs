using AdventOfCode.Models;

namespace AdventOfCode.Y2022.Days
{
    public class Day06 : Day
    {
        public Day06(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
        {
            int result = 0;
            var datastream = Inputs.First();

            for (int i = 0; i < datastream.Length - 4; i++)
            {
                var possibleMarker = datastream.Substring(i, 4);

                if (possibleMarker.Distinct().Count() == possibleMarker.Length)
                {
                    result = i + 4;
                    break;
                }
            }

            return result.ToString();
        }

        public override string RunPart2()
        {
            int result = 0;
            var datastream = Inputs.First();

            for (int i = 0; i < datastream.Length - 14; i++)
            {
                var possibleMarker = datastream.Substring(i, 14);

                if (possibleMarker.Distinct().Count() == possibleMarker.Length)
                {
                    result = i + 14;
                    break;
                }
            }

            return result.ToString();
        }
    }
}