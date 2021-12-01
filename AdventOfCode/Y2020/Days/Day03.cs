using AdventOfCode.Helpers;

namespace AdventOfCode.Y2020.Days
{
    public static class Day03
    {
        static int day = 3;
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

            var c = 30;

            var i = 3;
            foreach (var input in inputs.Skip(1))
            {
                if (input[i] == '#')
                {
                    result++;
                }

                i += 3;

                if (i > c)
                {
                    i -= (c + 1);
                }
            }

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

            result = 1;
            var c = 30;

            //1-1
            var tempTrees = 0;
            var i = 1;
            foreach (var input in inputs.Skip(1))
            {
                if (input[i] == '#')
                {
                    tempTrees++;
                }

                i += 1;

                if (i > c)
                {
                    i -= (c + 1);
                }
            }
            result = result * tempTrees;

            //3-1
            tempTrees = 0;
            i = 3;
            foreach (var input in inputs.Skip(1))
            {
                if (input[i] == '#')
                {
                    tempTrees++;
                }

                i += 3;

                if (i > c)
                {
                    i -= (c + 1);
                }
            }
            result = result * tempTrees;

            //5-1
            tempTrees = 0;
            i = 5;
            foreach (var input in inputs.Skip(1))
            {
                if (input[i] == '#')
                {
                    tempTrees++;
                }

                i += 5;

                if (i > c)
                {
                    i -= (c + 1);
                }
            }
            result = result * tempTrees;

            //7-1
            tempTrees = 0;
            i = 7;
            foreach (var input in inputs.Skip(1))
            {
                if (input[i] == '#')
                {
                    tempTrees++;
                }

                i += 7;

                if (i > c)
                {
                    i -= (c + 1);
                }
            }
            result = result * tempTrees;

            //1-2
            tempTrees = 0;
            i = 1;
            bool skip = false;
            foreach (var input in inputs.Skip(2))
            {
                if (skip)
                {
                    skip = false;
                    continue;
                }

                if (input[i] == '#')
                {
                    tempTrees++;
                }

                i += 1;

                if (i > c)
                {
                    i -= (c + 1);
                }

                skip = true;
            }
            result = result * tempTrees;

            #endregion

            var ms = Math.Round((DateTime.Now - start).TotalMilliseconds);

            if (result > 0) Answer2 = result.ToString();
            return $"Part 2 ({ms}ms): {result} ";
        }
    }
}