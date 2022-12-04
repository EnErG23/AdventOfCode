using AdventOfCode.Models;

namespace AdventOfCode.Y2022.Days
{
    public class Day03 : Day
    {
        public Day03(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
        {
            return Inputs.Select(i => (i.Substring(0, i.Length / 2), i.Substring(i.Length / 2, i.Length / 2)))
                .ToList()
                .Select(i => i.Item1.ToList().FirstOrDefault(c => i.Item2.Contains(c)))
                .Select(c => c - (char.IsUpper(c) ? 38 : 96))
                .Sum()
                .ToString();
        }

        public override string RunPart2()
        {
            return Enumerable.Range(0,Inputs.Count()/3)
                .Select(i => i * 3)
                .Select(i => Inputs[i].FirstOrDefault(c => Inputs[i+1].Contains(c) && Inputs[i+2].Contains(c)))
                .Select(c => c - (char.IsUpper(c) ? 38 : 96))
                .Sum()
                .ToString();
        }
    }
}