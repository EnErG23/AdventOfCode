using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Y2021.Days
{
    public static class Day02
    {
        static int day = 2;
        static List<string>? inputs;

        public static string? Answer1 { get; set; }
        public static string? Answer2 { get; set; }

        public static void Run(int part, bool test)
        {
            inputs = InputManager.GetInputAsStrings(day, test);

            var start = DateTime.Now;

            string part1 = "";
            string part2 = "";

            switch (part)
            {
                case 1:
                    part1 = Part1();
                    break;
                case 2:
                    part2 = Part2();
                    break;
                default:
                    part1 = Part1();
                    part2 = Part2();
                    break;
            }

            var ms = Math.Round((DateTime.Now - start).TotalMilliseconds);

            Console.WriteLine($"Day {day} ({ms}ms):");
            if (part1 != "") Console.WriteLine($"    {part1}");
            if (part2 != "") Console.WriteLine($"    {part2}");
        }

        static string Part1()
        {
            long result = 0;

            var start = DateTime.Now;

            #region Solution
                        
            var x = 0;
            var y = 0;            

            foreach (var input in inputs)
            {
                var com = input.Split(' ');
                var dir = com[0];
                var u = Convert.ToInt32(com[1]);

                switch (dir)
                {
                    case "down":
                        y += u;
                        break;
                    case "up":
                        y -= u;
                        break;
                    default:
                        x += u;
                        break;
                }
            }

            result = x * y;

            #endregion

            var ms = Math.Round((DateTime.Now - start).TotalMilliseconds);

            if (result > 0) Answer1 = result.ToString();
            return $"Part 1 ({ms}ms): {result} ";
        }

        static string Part2()
        {
            long result = 0;

            var start = DateTime.Now;

            #region Solution

            var x = 0;
            var y = 0;
            var aim = 0;

            foreach (var input in inputs)
            {
                var com = input.Split(' ');
                var dir = com[0];
                var u = Convert.ToInt32(com[1]);

                switch (dir)
                {
                    case "down":
                        aim += u;
                        break;
                    case "up":
                        aim -= u;
                        break;
                    default:
                        x += u;
                        y += u * aim;
                        break;
                }
            }

            result = x * y;

            #endregion

            var ms = Math.Round((DateTime.Now - start).TotalMilliseconds);

            if (result > 0) Answer2 = result.ToString();
            return $"Part 2 ({ms}ms): {result} ";
        }
    }
}