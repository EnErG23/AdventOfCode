using AdventOfCode.Models;

namespace AdventOfCode.Y2021.Days
{
    public class Day07 : Day
    {
        private const int day = 7;

        public Day07(bool test) : base(day, test) { }

        public override string RunPart1()
        {
            var crabs = Inputs[0].Split(",").Select(i => int.Parse(i)).ToList();

            List<int> fuel = new List<int>(new int[crabs.Max()]);

            for (int f = 0; f < fuel.Count; f++)
                for (int c = 0; c < crabs.Count; c++)
                    fuel[f] += Math.Abs(f - crabs[c]);

            return fuel.Min().ToString();
        }

        public override string RunPart2()
        {
            var crabs = Inputs[0].Split(",").Select(i => int.Parse(i)).ToList();

            List<long> fuel = new List<long>(new long[crabs.Max()]);

            for (int f = 0; f < fuel.Count; f++)
                for (int c = 0; c < crabs.Count; c++)
                    if (f != crabs[c])                    
                        fuel[f] += GetFuel(crabs[c], f);

            return fuel.Min().ToString();
        }

        private long GetFuel(int from, int to)
        {
            long result = 0;

            for (int i = 1; i <= Math.Abs(from - to); i++)
                result += i;

            return result;
        }
    }
}