﻿using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Y2019.Days
{
    public static class Day05
    {
        static int day = 5;
        static List<string>? inputs;

        public static string? Answer1 { get; set; }
        public static string? Answer2 { get; set; }

        public static void Run(int part, bool test)
        {
            var start = DateTime.Now;

            inputs = InputManager.GetInputAsStrings(day, test);

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

            result = IntCode(program, 1);

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

            var program = inputs[0].Split(',').Select(i => int.Parse(i)).ToList();

            result = IntCode(program, 5);

            #endregion

            var ms = Math.Round((DateTime.Now - start).TotalMilliseconds);

            if (result > 0) Answer2 = result.ToString();
            return $"Part 2 ({ms}ms): {result} ";
        }

        public static long IntCode(List<int> inputs, int input)
        {
            var i = 0;

            while (i < inputs.Count)
            {
                var instruction = inputs[i].ToString();

                while (instruction.Length < 5)
                    instruction = "0" + instruction;

                var opcode = instruction.Substring(3, 2);

                if (opcode == "99")
                    break;

                var modeParam1 = instruction.Substring(2, 1);
                var modeParam2 = instruction.Substring(1, 1);
                var modeParam3 = instruction.Substring(0, 1);

                var param1 = 0;
                var param2 = 0;
                var param3 = 0;

                switch (opcode)
                {
                    case "01":
                        param1 = modeParam1 == "1" ? inputs[i + 1] : inputs[inputs[i + 1]];
                        param2 = modeParam2 == "1" ? inputs[i + 2] : inputs[inputs[i + 2]];

                        inputs[inputs[i + 3]] = param1 + param2;
                        i += 4;
                        break;
                    case "02":
                        param1 = modeParam1 == "1" ? inputs[i + 1] : inputs[inputs[i + 1]];
                        param2 = modeParam2 == "1" ? inputs[i + 2] : inputs[inputs[i + 2]];

                        inputs[inputs[i + 3]] = param1 * param2;
                        i += 4;
                        break;
                    case "03":
                        inputs[inputs[i + 1]] = input;
                        i += 2;
                        break;
                    case "04":
                        param1 = modeParam1 == "1" ? inputs[i + 1] : inputs[inputs[i + 1]];

                        if (param1 > 0)
                            return param1;
                        i += 2;
                        break;
                    case "05":
                        param1 = modeParam1 == "1" ? inputs[i + 1] : inputs[inputs[i + 1]];
                        param2 = modeParam2 == "1" ? inputs[i + 2] : inputs[inputs[i + 2]];

                        i = param1 != 0 ? param2 : i += 3;
                        break;
                    case "06":
                        param1 = modeParam1 == "1" ? inputs[i + 1] : inputs[inputs[i + 1]];
                        param2 = modeParam2 == "1" ? inputs[i + 2] : inputs[inputs[i + 2]];

                        i = param1 == 0 ? param2 : i += 3;
                        break;
                    case "07":
                        param1 = modeParam1 == "1" ? inputs[i + 1] : inputs[inputs[i + 1]];
                        param2 = modeParam2 == "1" ? inputs[i + 2] : inputs[inputs[i + 2]];
                        param3 = modeParam3 == "1" ? inputs[i + 3] : inputs[inputs[i + 3]];

                        inputs[inputs[i + 3]] = param1 < param2 ? 1 : 0;
                        i += 4;
                        break;
                    case "08":
                        param1 = modeParam1 == "1" ? inputs[i + 1] : inputs[inputs[i + 1]];
                        param2 = modeParam2 == "1" ? inputs[i + 2] : inputs[inputs[i + 2]];
                        param3 = modeParam3 == "1" ? inputs[i + 3] : inputs[inputs[i + 3]];

                        inputs[inputs[i + 3]] = param1 == param2 ? 1 : 0;
                        i += 4;
                        break;
                    default:
                        i++;
                        break;
                }
            }

            return inputs[0];
        }
    }
}