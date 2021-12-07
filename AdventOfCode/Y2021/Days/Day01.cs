using AdventOfCode.Models;

namespace AdventOfCode.Y2021.Days
{
    public class Day01 : Day
    {
        private const int day = 1;
        private List<int>? depths;

        public Day01(bool test) : base(day, test) { }

        public override string RunPart1()
        {
            depths = Inputs.Select(i => int.Parse(i)).ToList();
            
            return CountIncreases(1).ToString();
        }

        public override string RunPart2()
        {
            if (depths is null)
                depths = Inputs.Select(i => int.Parse(i)).ToList();
            
            return CountIncreases(3).ToString();
        }

        public long CountIncreases(int window)
        {
            long result = 0;

            for (int i = window; i < depths.Count; i++)
                if (depths[i] > depths[i - window])
                    result++;

            return result;
        }
    }
}