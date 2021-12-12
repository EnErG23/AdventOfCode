using AdventOfCode.Models;

namespace AdventOfCode.Y2021.Days
{
    public class Day11 : Day
    {
        private List<List<int>>? octopuses;
        private List<(int, int)>? flashedOctopuses;

        public Day11(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
        {
            octopuses = new();
            Inputs.ForEach(input => octopuses.Add(input.ToList().Select(i => int.Parse(i.ToString())).ToList()));

            long result = 0;

            for (int i = 0; i < 100; i++)
            {
                flashedOctopuses = new();

                for (int r = 0; r < octopuses.Count; r++)
                    for (int c = 0; c < octopuses[r].Count; c++)
                        octopuses[r][c]++;

                long flashResult = 1;

                while (octopuses.Sum(r => r.Count(o => o > 9)) > 0 && flashResult > 0)
                {
                    flashResult = Flash();
                    result += flashResult;
                }

                for (int r = 0; r < octopuses.Count; r++)
                    for (int c = 0; c < octopuses[r].Count; c++)
                        if (octopuses[r][c] > 9)
                            octopuses[r][c] = 0;
            }

            return result.ToString();
        }

        public override string RunPart2()
        {
            octopuses = new();
            Inputs.ForEach(input => octopuses.Add(input.ToList().Select(i => int.Parse(i.ToString())).ToList()));

            long result = 0;

            var i = 1;

            while(true)
            {
                flashedOctopuses = new();

                for (int r = 0; r < octopuses.Count; r++)
                    for (int c = 0; c < octopuses[r].Count; c++)
                        octopuses[r][c]++;

                long flashResult = 1;

                while (octopuses.Sum(r => r.Count(o => o > 9)) > 0 && flashResult > 0)
                {
                    flashResult = Flash();
                    result += flashResult;
                }
                
                for (int r = 0; r < octopuses.Count; r++)
                    for (int c = 0; c < octopuses[r].Count; c++)
                        if (octopuses[r][c] > 9)
                            octopuses[r][c] = 0;

                if (flashedOctopuses.Count == octopuses.Sum(o => o.Count()))
                {
                    result = i;
                    break;
                }

                i++;
            }

            return result.ToString();
        }

        private long Flash()
        {
            long result = 0;

            for (int r = 0; r < octopuses.Count; r++)
            {
                for (int c = 0; c < octopuses[r].Count; c++)
                {
                    if (octopuses[r][c] > 9 && !flashedOctopuses.Contains((r, c)))
                    {
                        result++;
                        flashedOctopuses.Add((r, c));

                        for (int v = -1; v <= 1; v++)
                            for (int h = -1; h <= 1; h++)
                                if (r + v >= 0 && r + v < octopuses.Count && c + h >= 0 && c + h < octopuses[r].Count)
                                    octopuses[r + v][c + h]++;
                    }
                }
            }

            return result;
        }
    }
}