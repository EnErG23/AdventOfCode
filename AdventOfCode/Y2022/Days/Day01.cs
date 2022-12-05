using AdventOfCode.Models;

namespace AdventOfCode.Y2022.Days
{
    public class Day01 : Day
    {
        private readonly IEnumerable<int> _elfCalories;

        public Day01(int year, int day, bool test) : base(year, day, test)
        {
            _elfCalories = String.Join(';', Inputs)
                .Split(";;")
                .ToList()
                .Select(e => e.Split(';').Select(s => int.Parse(s)).Sum());
        }

        public override string RunPart1()
        {
            return _elfCalories
                .Max()
                .ToString();
        }

        public override string RunPart2()
        {
            return _elfCalories
                .OrderByDescending(e => e)
                .Take(3)
                .Sum().
                ToString();
        }
    }
}