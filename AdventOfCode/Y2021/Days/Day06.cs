using AdventOfCode.Helpers;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Y2021.Days
{
    public static class Day06
    {
        static int day = 6;
        static List<string>? inputs;
        static List<int>? fishes;

        public static string? Answer1 { get; set; }
        public static string? Answer2 { get; set; }

        public static void Run(int part, bool test)
        {
            Stopwatch sw = Stopwatch.StartNew();

            inputs = InputManager.GetInputAsStrings(day, test);

            fishes = inputs[0].Split(",").Select(i => int.Parse(i)).ToList();

            string part1 = "";
            string part2 = "";

            if (part == 1)
                part1 = Part1();
            else if (part == 2)
                part2 = Part2();
            else
            {
                part1 = Part1();
                part2 = Part2();
            }

            sw.Stop();
			var ms = sw.Elapsed.TotalMilliseconds;

            Console.WriteLine($"Day {day} ({ms}ms):");
            if (part1 != "") Console.WriteLine($"    {part1}");
            if (part2 != "") Console.WriteLine($"    {part2}");
        }

        static string Part1()
        {
            Stopwatch sw = Stopwatch.StartNew();

            long result = 0;

            #region Solution

            result = BreedFishes(80);

            #endregion

            sw.Stop();
			var ms = sw.Elapsed.TotalMilliseconds;

            if (result > 0) Answer1 = result.ToString();
            return $"Part 1 ({ms}ms): {result} ";
        }

        static string Part2()
        {
            Stopwatch sw = Stopwatch.StartNew();

            long result = 0;

            #region Solution

            result = BreedFishes(256);

            #endregion

            sw.Stop();
			var ms = sw.Elapsed.TotalMilliseconds;

            if (result > 0) Answer2 = result.ToString();
            return $"Part 2 ({ms}ms): {result} ";
        }

        static long BreedFishes2(int days)
        {
            long result = fishes.Count;
            var daysPassed = 1;

            var fishesToAddAfterIDays = new long[9];

            for (int i = 0; i < fishes.Count; i++)
                fishesToAddAfterIDays[fishes[i]]++;

            while (daysPassed <= days)
            {
                long fishesToAdd = fishesToAddAfterIDays[0];
                result += fishesToAdd;

                for (int i = 0; i < 8; i++)
                    fishesToAddAfterIDays[i] = fishesToAddAfterIDays[i + 1];

                fishesToAddAfterIDays[8] = 0;
                fishesToAddAfterIDays[6] += fishesToAdd;
                fishesToAddAfterIDays[8] += fishesToAdd;

                daysPassed++;
            }

            return result;
        }

        static long BreedFishes(int days)
        {
            var fishesToAddAfterIDays = new long[9];

            fishes.ForEach(f => fishesToAddAfterIDays[f]++);

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