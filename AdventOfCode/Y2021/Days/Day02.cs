using AdventOfCode.Models;
using AdventOfCode.Helpers;

namespace AdventOfCode.Y2021.Days
{
    public class Day02 : Day
    {
        public Day02(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
        {
            return PilotSubmarine(false).ToString();
        }

        public override string RunPart2()
        {
            return PilotSubmarine(true).ToString();
        }

        public long PilotSubmarine(bool useAim)
        {
            var x = 0;
            var y = 0;
            var aim = 0;

            foreach (var input in Inputs)
            {
                var com = input.Split(' ');
                var dir = com[0];
                var u = Convert.ToInt32(com[1]);

                if (dir == "down")
                    if (useAim)
                        aim += u;
                    else
                        y += u;
                else if (dir == "up")
                    if (useAim)
                        aim -= u;
                    else
                        y -= u;    
                else
                {
                    x += u;

                    if (useAim)
                        y += u * aim;
                }
            }

            return x * y;
        }
    }
}