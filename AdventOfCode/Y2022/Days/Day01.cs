using AdventOfCode.Models;

namespace AdventOfCode.Y2022.Days
{
    public class Day01 : Day
    {
        public Day01(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
        {
            return String.Join(';', Inputs)
                .Split(";;")
                .ToList()
                .Select(e => e.Split(';').Select(s => int.Parse(s)).Sum())
                .Max()
                .ToString();
        }

        public override string RunPart2()
        {
            return String.Join(';', Inputs)
                .Split(";;")
                .ToList()
                .Select(e => e.Split(';').Select(s => int.Parse(s)).Sum())
                .OrderByDescending(e => e)
                .Take(3)
                .Sum()
                .ToString();
        }
    }
}