using AdventOfCode.Helpers;

namespace AdventOfCode.Y2020.Days
{
    public static class Day01
    {
        static int day = 1;
        static List<int>? inputs;

        public static string? Answer1 { get; set; }
        public static string? Answer2 { get; set; }

        public static void Run(int part, bool test)
        {
            inputs = InputManager.GetInputAsInts(day, test);

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

            foreach (int x in inputs)
            {
                foreach (int y in inputs)
                {
                    if (x + y == 2020) result = x * y;

                    if (result != 0) break;
                }
                if (result != 0) break;
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

            foreach (int x in inputs)
            {
                foreach (int y in inputs)
                {
                    foreach (int z in inputs)
                    {
                        if (x + y + z == 2020) result = x * y * z;

                        if (result != 0) break;
                    }
                    if (result != 0) break;
                }
                if (result != 0) break;
            }

            #endregion

            var ms = Math.Round((DateTime.Now - start).TotalMilliseconds);

            if (result > 0) Answer2 = result.ToString();
            return $"Part 2 ({ms}ms): {result} ";
        }
    }
}