﻿using AdventOfCode.Helpers;

namespace AdventOfCode.Y2021.Days
{
    public static class Day01
    {
        static int day = 1;
        static List<int>? inputs;

        public static string? Answer1 { get; set; }
        public static string? Answer2 { get; set; }

        public static void Run(int part, bool test)
        {
            var start = DateTime.Now;

            inputs = InputManager.GetInputAsInts(day, test);

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

            var ms = Math.Round((DateTime.Now - start).TotalMilliseconds);

            Console.WriteLine($"Day {day} ({ms}ms):");
            if (part1 != "") Console.WriteLine($"    {part1}");
            if (part2 != "") Console.WriteLine($"    {part2}");
        }

        static string Part1()
        {
            var start = DateTime.Now;

            long result = 0;

            #region Solution

            for (int i = 1; i < inputs.Count; i++)
                if (inputs[i] > inputs[i - 1])
                    result++;

            #endregion

            var ms = Math.Round((DateTime.Now - start).TotalMilliseconds);

            if (result > 0) Answer1 = result.ToString();
            return $"Part 1 ({ms}ms): {result} ";
        }

        static string Part2()
        {
            var start = DateTime.Now;

            long result = 0;

            #region Solution

            for (int i = 3; i < inputs.Count; i++)
                if (inputs[i] > inputs[i - 3])
                    result++;

            #endregion

            var ms = Math.Round((DateTime.Now - start).TotalMilliseconds);

            if (result > 0) Answer2 = result.ToString();
            return $"Part 2 ({ms}ms): {result} ";
        }
    }
}