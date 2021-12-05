﻿using AdventOfCode.Helpers;

namespace AdventOfCode.Y2020.Days
{
    public static class Day10
    {
        static int day = 10;
        static List<int>? inputs;

        public static string? Answer1 { get; set; }
        public static string? Answer2 { get; set; }

        public static void Run(int part, bool test)
        {
            inputs = InputManager.GetInputAsInts(day, test);

            inputs.Add(0);
            inputs = inputs.OrderBy(i => i).ToList();
            inputs.Add(inputs[inputs.Count() - 1] + 3);

            var start = DateTime.Now;

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

            var a = 0;
            var ones = 0;
            var threes = 0;

            foreach (var intInput in inputs.Skip(1))
            {
                var difference = intInput - inputs[a];

                if (difference == 1) ones++;
                else if (difference == 3) threes++;

                a++;
            }

            result = ones * threes;

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

            result = 1;

            var count = 0;
            var a = 0;

            foreach (var l in inputs.Skip(1).Take(inputs.Count() - 2))
            {
                if (inputs[a + 2] - inputs[a] < 4)
                {
                    count++;
                }
                else
                {
                    if (count > 0)
                    {
                        var fact = count;

                        for (var i = count - 1; i >= 1; i--)
                        {
                            fact = fact * i;
                        }

                        if (l - inputs[a - count] < 4)
                        {
                            fact++;
                        }

                        if (count > 1)
                        {
                            fact++;
                        }

                        result *= fact;

                        count = 0;
                    }
                }

                a++;
            }

            #endregion

            var ms = Math.Round((DateTime.Now - start).TotalMilliseconds);

            if (result > 0) Answer2 = result.ToString();
            return $"Part 2 ({ms}ms): {result} ";
        }
    }
}