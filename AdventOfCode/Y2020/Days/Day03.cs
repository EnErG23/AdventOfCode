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

            result = CheckSlope(inputs, 3, 1);

            #endregion

            var ms = Math.Round((DateTime.Now - start).TotalMilliseconds);

            if (result > 0) Answer1 = result.ToString();
            return $"Part 1 ({ms}ms): {result} ";
        }

        static string Part2()
        {
            long result = 1;

            var start = DateTime.Now;

            #region Solution

            result = 1 * CheckSlope(inputs, 1, 1) * CheckSlope(inputs, 3, 1) * CheckSlope(inputs, 5, 1) * CheckSlope(inputs, 7, 1) * CheckSlope(inputs, 1, 2);

            #endregion

            var ms = Math.Round((DateTime.Now - start).TotalMilliseconds);

            if (result > 0) Answer2 = result.ToString();
            return $"Part 2 ({ms}ms): {result} ";
        }

        static long CheckSlope(List<string> inputs, int right, int down)
        {
            long result = 0;

            var maxX = inputs[0].Length - 1;
            var x = right;

            var skip = down;

            foreach (var input in inputs)
            {
                if (skip > 0)
                {
                    skip--;
                    continue;
                }

                result += input[x] == '#' ? 1 : 0;
                x = x + right > maxX ? (x + right) % maxX - 1 : x + right;   

                skip = down - 1;
            }

            return result;
        }

        // Visualize

        public static void Visualize(int part, bool test)
        {
            inputs = InputManager.GetInputAsStrings(day, test);

            Console.Clear();

            if (part == 1)
                VisualizePart1();
            else if (part == 2)
                VisualizePart2();
            else
            {
                VisualizePart1();
                VisualizePart2();
            }

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Cyan;
        }

        static void VisualizePart1()
        {
            Console.WriteLine($"Visualization for 2021.3.1");
            Console.WriteLine();

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.DarkBlue;

            VisualizeSlope(inputs, 3, 1);
        }

        static void VisualizePart2()
        {

        }

        static void VisualizeSlope(List<string> inputs, int right, int down)
        {
            var maxX = inputs[0].Length - 1;
            var x = right;
            var y = 0;

            var skip = down;

            foreach (var input in inputs)
            {
                var row = input.Replace('.', ' ');

                y++;

                if (skip > 0)
                {
                    Console.WriteLine(row);
                    skip--;
                    continue;
                }

                VisualizeRow(row, x);

                Thread.Sleep(100);

                x = x + right > maxX ? x + right - maxX - 1 : x + right;
                skip = down - 1;
            }
        }

        static void VisualizeRow(string row, int x)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write(row[..x]);

            if (row[x] == '#')
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write('X');
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write('O');
            }

            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write(row[(x + 1)..]);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine();
        }
    }
}