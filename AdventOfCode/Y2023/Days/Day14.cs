using AdventOfCode.Models;

namespace AdventOfCode.Y2023.Days
{
    public class Day14 : Day
    {
        public Day14(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
        {
            int totalLoad = 0;
            List<(int, int)> stuckRocks = new();

            for (int r = 0; r < Inputs.Count; r++)
                for (int c = 0; c < Inputs[r].Length; c++)
                    if (Inputs[r][c] == 'O')
                    {
                        int load = Inputs.Count - r;

                        for (int rd = r - 1; rd >= -1; rd--)
                        {
                            if (rd == -1 || Inputs[rd][c] == '#' || stuckRocks.Contains((rd, c)))
                            {
                                stuckRocks.Add((rd + 1, c));
                                break;
                            }

                            load++;
                        }

                        Console.WriteLine($"({r},{c}) => {load}");

                        totalLoad += load;
                    }

            return totalLoad.ToString();
        }

        public override string RunPart2()
        {
            return "undefined";
        }
    }
}