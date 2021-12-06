using AdventOfCode.Helpers;
using System.Diagnostics;

namespace AdventOfCode.Y2020.Days
{
    public static class Day08
    {
        static int day = 8;
        static List<string>? inputs;

        public static string? Answer1 { get; set; }
        public static string? Answer2 { get; set; }

        public static void Run(int part, bool test)
        {
            Stopwatch sw = Stopwatch.StartNew();

            inputs = InputManager.GetInputAsStrings(day, test);

            string part1 = "";
            string part2 = "";

            switch (part)
            {
                case 1:
                    part1 = Part1();
                    break;
                case 2:
                    part2 = Part2(test);
                    break;
                default:
                    part1 = Part1();
                    part2 = Part2(test);
                    break;
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

            var ranLines = new List<int>();

            var i = 0;

            while (true)
            {
                if (ranLines.Contains(i))
                {
                    break;
                }

                ranLines.Add(i);

                var command = inputs[i].Substring(0, 3);

                switch (command)
                {
                    case "acc":
                        var accValue = Convert.ToInt32(inputs[i].Substring(5));
                        if (inputs[i].Contains("+"))
                        {
                            result += accValue;
                        }
                        else
                        {
                            result -= accValue;
                        }
                        i++;
                        break;
                    case "jmp":
                        var jmpValue = Convert.ToInt32(inputs[i].Substring(5));
                        if (inputs[i].Contains("+"))
                        {
                            i += jmpValue;
                        }
                        else
                        {
                            i -= jmpValue;
                        }
                        break;
                    default:
                        i++;
                        break;
                }
            }

            #endregion

            sw.Stop();
			var ms = sw.Elapsed.TotalMilliseconds;

            if (result > 0) Answer1 = result.ToString();
            return $"Part 1 ({ms}ms): {result} ";
        }

        static string Part2(bool test)
        {
            Stopwatch sw = Stopwatch.StartNew();

            long result = 0;

            #region Solution

            for (int j = 0; j < inputs.Count() - 1; j++)
            {
                result = 0;

                inputs = InputManager.GetInputAsStrings(day, test);

                if (inputs[j].Contains("jmp"))
                {
                    inputs[j] = inputs[j].Replace("jmp", "nop");
                }
                else if (inputs[j].Contains("nop"))
                {
                    inputs[j] = inputs[j].Replace("nop", "jmp");
                }
                else
                {
                    continue;
                }

                var ranLines = new List<int>();

                var i = 0;
                var found = false;

                while (true)
                {
                    if (i > inputs.Count() - 1)
                    {
                        found = true;
                        break;
                    }

                    if (ranLines.Contains(i))
                    {
                        break;
                    }

                    ranLines.Add(i);

                    var command = inputs[i].Substring(0, 3);

                    switch (command)
                    {
                        case "acc":
                            var accValue = Convert.ToInt32(inputs[i].Substring(5));
                            if (inputs[i].Contains("+"))
                            {
                                result += accValue;
                            }
                            else
                            {
                                result -= accValue;
                            }
                            i++;
                            break;
                        case "jmp":
                            var jmpValue = Convert.ToInt32(inputs[i].Substring(5));
                            if (inputs[i].Contains("+"))
                            {
                                i += jmpValue;
                            }
                            else
                            {
                                i -= jmpValue;
                            }
                            break;
                        default:
                            i++;
                            break;
                    }
                }

                if (found) break;
            }

            #endregion

            sw.Stop();
			var ms = sw.Elapsed.TotalMilliseconds;

            if (result > 0) Answer2 = result.ToString();
            return $"Part 2 ({ms}ms): {result} ";
        }
    }
}