using AdventOfCode.Helpers;

namespace AdventOfCode.Y2020.Days
{
    public static class Day11
    {
        static int day = 11;
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

            foreach (string input in inputs)
            {
                var i = input;
                int min = Convert.ToInt32(i.Substring(0, i.IndexOf('-')));

                i = i.Substring(i.IndexOf('-') + 1);
                int max = Convert.ToInt32(i.Substring(0, i.IndexOf(' ')));

                i = i.Substring(i.IndexOf(' ') + 1);
                char letter = Convert.ToChar(i.Substring(0, 1));

                i = i.Substring(i.IndexOf(' ') + 1);
                string password = i;

                var count = password.Count(s => s == letter);

                if (count >= min && count <= max)
                {
                    result++;
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

            foreach (string input in inputs)
            {
                var i = input;
                int pos1 = Convert.ToInt32(i.Substring(0, i.IndexOf('-')));

                i = i.Substring(i.IndexOf('-') + 1);
                int pos2 = Convert.ToInt32(i.Substring(0, i.IndexOf(' ')));

                i = i.Substring(i.IndexOf(' ') + 1);
                string letter = i.Substring(0, 1);

                i = i.Substring(i.IndexOf(' ') + 1);
                string password = i;

                var pos1HasChar = password.Substring(pos1 - 1, 1) == letter;
                var pos2HasChar = password.Substring(pos2 - 1, 1) == letter;

                if (pos1HasChar)
                {
                    if (!pos2HasChar)
                    {
                        result++;
                    }
                }
                else
                {
                    if (pos2HasChar)
                    {
                        result++;
                    }
                }
            }

            #endregion

            var ms = Math.Round((DateTime.Now - start).TotalMilliseconds);

            if (result > 0) Answer2 = result.ToString();
            return $"Part 2 ({ms}ms): {result} ";
        }
    }
}