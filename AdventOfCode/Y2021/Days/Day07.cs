using AdventOfCode.Models;

namespace AdventOfCode.Y2021.Days
{
    public class Day07 : Day
    {
        private List<int>? crabs;

        public Day07(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
        {
            crabs = Inputs[0].Split(",").Select(i => int.Parse(i)).OrderBy(i => i).ToList();

            int median = crabs[crabs.Count / 2];

            return crabs.Sum(crab => Math.Abs(crab - median)).ToString();
        }

        public override string RunPart2()
        {
            if (crabs == null)
                crabs = Inputs[0].Split(",").Select(i => int.Parse(i)).ToList();

            var average = crabs.Average();
            int flooredAverage = (int)Math.Floor(average);
            int ceiledAverage = (int)Math.Ceiling(average);

            return Math.Min(crabs.Sum(crab => Math.Abs(crab - flooredAverage) * (Math.Abs(crab - flooredAverage) + 1) / 2),
                            crabs.Sum(crab => Math.Abs(crab - ceiledAverage) * (Math.Abs(crab - ceiledAverage) + 1) / 2))
                       .ToString();
        }
    }
}