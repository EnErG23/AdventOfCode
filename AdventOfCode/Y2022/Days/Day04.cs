using AdventOfCode.Models;

namespace AdventOfCode.Y2022.Days
{
    public class Day04 : Day
    {
        private struct Assignment { public int S1; public int S2; }
        private readonly IEnumerable<(Assignment, Assignment)> _assignmentPairs;

        public Day04(int year, int day, bool test) : base(year, day, test)
        {
            _assignmentPairs = Inputs.Select(i => (new Assignment
            {
                S1 = int.Parse(i.Split(",")[0].Split("-")[0]),
                S2 = int.Parse(i.Split(",")[0].Split("-")[1])
            }, new Assignment
            {
                S1 = int.Parse(i.Split(",")[1].Split("-")[0]),
                S2 = int.Parse(i.Split(",")[1].Split("-")[1])
            }));
        }

        public override string RunPart1()
        {
            return _assignmentPairs
                .Count(a => (a.Item1.S1 <= a.Item2.S1 && a.Item1.S2 >= a.Item2.S2) || (a.Item1.S1 >= a.Item2.S1 && a.Item1.S2 <= a.Item2.S2))
                .ToString();
        }

        public override string RunPart2()
        {
            return _assignmentPairs
                .Count(a => !(a.Item1.S1 > a.Item2.S2 || a.Item1.S2 < a.Item2.S1))
                .ToString();
        }
    }
}