using AdventOfCode.Models;

namespace AdventOfCode.Y2021.Days
{
    public class Day06 : Day
    {
        private long[] fishesToAddAfterIDays = new long[9];

        public Day06(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
        {
            Inputs[0].Split(",").Select(i => int.Parse(i)).ToList().ForEach(f => fishesToAddAfterIDays[f]++);

            return BreedFishes(80).ToString();
        }

        public override string RunPart2()
        {
            if (fishesToAddAfterIDays.Sum() > 0)
                fishesToAddAfterIDays = new long[9];

            Inputs[0].Split(",").Select(i => int.Parse(i)).ToList().ForEach(f => fishesToAddAfterIDays[f]++);

            return BreedFishes(256).ToString();
        }

        private long BreedFishes(int days)
        {
            for (int d = 1; d <= days; d++)
            {
                long fishesToAdd = fishesToAddAfterIDays[0];

                for (int f = 0; f < 8; f++)
                    fishesToAddAfterIDays[f] = fishesToAddAfterIDays[f + 1];

                fishesToAddAfterIDays[6] += fishesToAdd;
                fishesToAddAfterIDays[8] = fishesToAdd;
            }

            return fishesToAddAfterIDays.Sum();
        }
    }
}