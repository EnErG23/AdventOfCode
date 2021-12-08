using AdventOfCode.Models;
using AdventOfCode.Helpers;

namespace AdventOfCode.Y2019.Days
{
    public class Day01 : Day
    {
        static List<int>? inputs;

        public Day01(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
        {
            inputs = Inputs.Select(i => int.Parse(i)).ToList();

            long result = 0;

            inputs.ForEach(i => result += i / 3 - 2);

            return result.ToString();
        }

        public override string RunPart2()
        {
            if (inputs is null)
                inputs = Inputs.Select(i => int.Parse(i)).ToList();

            long result = 0;

            foreach (var input in inputs)
            {
                var fuel = input;
                while (true)
                {
                    fuel = fuel / 3 - 2;
                    if (fuel < 1)
                        break;
                    result += fuel;
                }
            }

            return result.ToString();
        }
    }
}