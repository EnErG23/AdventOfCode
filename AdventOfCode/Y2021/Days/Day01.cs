using AdventOfCode.Models;

namespace AdventOfCode.Y2021.Days
{
    public class Day01 : Day
    {
        private const int day = 1;
        private List<int>? inputs;

        public Day01(bool test) : base(day, test) { }

        public override string RunPart1()
        {
            inputs = Inputs.Select(i => int.Parse(i)).ToList();

            long result = 0;

            for (int i = 1; i < inputs.Count; i++)
                if (inputs[i] > inputs[i - 1])
                    result++;

            return result.ToString();
        }

        public override string RunPart2()
        {
            if (inputs is null)
                inputs = Inputs.Select(i => int.Parse(i)).ToList();

            long result = 0;

            for (int i = 3; i < inputs.Count; i++)
                if (inputs[i] > inputs[i - 3])
                    result++;

            return result.ToString();
        }
    }
}