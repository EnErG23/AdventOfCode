using AdventOfCode.Helpers;
using System.Diagnostics;

namespace AdventOfCode.Y2020.Days
{
    public static class Day09
    {
        static int day = 9;
        static List<long>? inputs;

        public static string? Answer1 { get; set; }
        public static string? Answer2 { get; set; }

        public static void Run(int part, bool test)
        {
            Stopwatch sw = Stopwatch.StartNew();

            inputs = InputManager.GetInputAsLongs(day, test);

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

            var p = 25;
            var i = p;

            foreach (var input in inputs.Skip(p))
            {
                var preamble = inputs.GetRange(i - p, p);
                var found = false;

                foreach (var pre1 in preamble)
                {
                    foreach (var pre2 in preamble.Where(pr => pr != pre1))
                    {
                        if (pre1 + pre2 == input)
                        {
                            found = true;
                        }
                        if (found) break;
                    }
                    if (found) break;
                }
                if (!found)
                {
                    result = input;
                    break;
                }
                i++;
            }

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

            long firstError = 0;
            var p = 25;
            var i = p;

            foreach (var input in inputs.Skip(p))
            {
                var preamble = inputs.GetRange(i - p, p);
                var found = false;

                foreach (var pre1 in preamble)
                {
                    foreach (var pre2 in preamble.Where(pr => pr != pre1))
                    {
                        if (pre1 + pre2 == input)
                        {
                            found = true;
                        }
                        if (found) break;
                    }
                    if (found) break;
                }
                if (!found)
                {
                    firstError = input;
                    break;
                }
                i++;
            }

            long subTotal = 0;
            var a = 0;
            var length = 1;

            foreach (var input1 in inputs)
            {
                subTotal = 0;
                a = inputs.IndexOf(input1);
                length = 1;

                foreach (var input2 in inputs.GetRange(a, inputs.Count() - a))
                {
                    subTotal += input2;

                    if (subTotal > firstError)
                    {
                        break;
                    }

                    if (subTotal == firstError)
                    {
                        result = inputs.GetRange(a, length).OrderBy(inp => inp).First() + inputs.GetRange(a, length).OrderByDescending(inp => inp).First();
                        break;
                    }

                    length++;
                }

                if (result != 0)
                {
                    break;
                }

            }

            #endregion

            sw.Stop();
			var ms = sw.Elapsed.TotalMilliseconds;

            if (result > 0) Answer2 = result.ToString();
            return $"Part 2 ({ms}ms): {result} ";
        }
    }
}