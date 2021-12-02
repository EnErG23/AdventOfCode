using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Y2019.Days
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
            long result = 0;

            var start = DateTime.Now;

            #region Solution

            var program = inputs[0].Split(',').Select(i => int.Parse(i)).ToList();

            result = IntCode(program, 4, 12, 2);

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

            var output = 19690720;

            for (int i = 0; i < inputs[0].Split(',').Length - 1; i++)
            {
                for (int j = 0; j < inputs[0].Split(',').Length - 1; j++)
                {
                    var program = inputs[0].Split(',').Select(i => int.Parse(i)).ToList();

                    if (IntCode(program, 4, i, j) == output)
                    {
                        result = 100 * i + j;
                        break;
                    }
                }
                if (result != 0)
                    break;
            }

            #endregion

            var ms = Math.Round((DateTime.Now - start).TotalMilliseconds);

            if (result > 0) Answer2 = result.ToString();
            return $"Part 2 ({ms}ms): {result} ";
        }

        public static long IntCode(List<int> inputs, int pointer, int noun, int verb)
        {
            inputs[1] = noun;
            inputs[2] = verb;

            for (int i = 0; i < inputs.Count - 1; i += pointer)
            {
                if (inputs[i] == 1)
                    inputs[inputs[i + 3]] = inputs[inputs[i + 1]] + inputs[inputs[i + 2]];
                else if (inputs[i] == 2)
                    inputs[inputs[i + 3]] = inputs[inputs[i + 1]] * inputs[inputs[i + 2]];
                else if (inputs[i] == 99)
                    break;
            }

            return inputs[0];
        }
    }
}