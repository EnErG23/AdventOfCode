using AdventOfCode.Models;

namespace AdventOfCode.Y2022.Days
{
    public class Day04 : Day
    {
        public Day04(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
        {
            return Inputs.Select(i => (new
            {
                s1 = int.Parse(i.Split(",")[0].Split("-")[0]),
                s2 = int.Parse(i.Split(",")[0].Split("-")[1])
            }, new
            {
                s1 = int.Parse(i.Split(",")[1].Split("-")[0]),
                s2 = int.Parse(i.Split(",")[1].Split("-")[1])
            }))
                .Count(a => (a.Item1.s1 <= a.Item2.s1 && a.Item1.s2 >= a.Item2.s2) || (a.Item1.s1 >= a.Item2.s1 && a.Item1.s2 <= a.Item2.s2))
                .ToString();
        }

        public override string RunPart2()
        {
            return Inputs.Select(i => (new
            {
                s1 = int.Parse(i.Split(",")[0].Split("-")[0]),
                s2 = int.Parse(i.Split(",")[0].Split("-")[1])
            }, new
            {
                s1 = int.Parse(i.Split(",")[1].Split("-")[0]),
                s2 = int.Parse(i.Split(",")[1].Split("-")[1])
            }))
                .Count(a => !(a.Item1.s1 > a.Item2.s2 || a.Item1.s2 < a.Item2.s1))
                .ToString();
        }
    }
}