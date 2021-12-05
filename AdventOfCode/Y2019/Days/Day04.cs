using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Y2019.Days
{
    public static class Day04
    {
        static int day = 4;
        static List<string>? inputs;
        static List<int>? range;

        public static string? Answer1 { get; set; }
        public static string? Answer2 { get; set; }

        public static void Run(int part, bool test)
        {
            inputs = InputManager.GetInputAsStrings(day, test);
            range = inputs[0].Split("-").Select(i => int.Parse(i)).ToList();

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

            for (int i = range[0]; i < range[1]; i++)
            {
                var password = i.ToString();

                var req4 = true;

                for (int j = 1; j < password.Length; j++)
                    if (password[j - 1] > password[j])
                    {
                        req4 = false;
                        break;
                    }

                if (req4)
                    for (int j = 1; j < password.Length; j++)
                        if (password[j - 1] == password[j])
                        {
                            result++;
                            break;
                        }
            }

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

            for (int i = range[0]; i < range[1]; i++)
            {
                var password = i.ToString();

                var req3 = false;
                var req4 = true;
                var req5 = false;

                for (int j = 0; j < password.Length - 1; j++)
                    if (password[j] > password[j + 1])
                    {
                        req4 = false;
                        break;
                    }

                if (req4)
                {
                    for (int j = 0; j < password.Length - 1; j++)
                    {
                        if (password.Where(p => p == password[j]).Count() > 1)
                            req3 = true;
                        if (password.Where(p => p == password[j]).Count() == 2)
                            req5 = true;
                    }
                    if (req3 & req5)
                        result++;
                }
            }

            #endregion

            var ms = Math.Round((DateTime.Now - start).TotalMilliseconds);

            if (result > 0) Answer2 = result.ToString();
            return $"Part 2 ({ms}ms): {result} ";
        }
    }
}