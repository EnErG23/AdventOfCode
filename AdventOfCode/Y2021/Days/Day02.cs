using AdventOfCode.Models;
using AdventOfCode.Helpers;

namespace AdventOfCode.Y2021.Days
{
    public class Day02 : Day
    {
        private const int day = 2;

        public Day02(bool test) : base(day, test) { }

        public override string RunPart1()
        {
            long result = 0;

            var x = 0;
            var y = 0;

            foreach (var input in Inputs)
            {
                var com = input.Split(' ');
                var dir = com[0];
                var u = Convert.ToInt32(com[1]);

                if (dir == "down")
                    y += u;
                else if (dir == "up")
                    y -= u;
                else
                    x += u;
            }

            result = x * y;

            return result.ToString();
        }

        public override string RunPart2()
        {
            long result = 0;

            var x = 0;
            var y = 0;
            var aim = 0;

            foreach (var input in Inputs)
            {
                var com = input.Split(' ');
                var dir = com[0];
                var u = Convert.ToInt32(com[1]);

                if (dir == "down")
                    aim += u;
                else if (dir == "up")
                    aim -= u;
                else
                {
                    x += u;
                    y += u * aim;
                }
            }

            result = x * y;

            return result.ToString();
        }
    }
}