using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Y2021.Days
{
    public static class Day05
    {
        static int day = 5;
        static List<string>? inputs;
        static List<List<int>>? grid;

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
            var start = DateTime.Now;

            long result = 0;

            #region Solution

            InputToGrid();

            DrawLines(false);

            result = grid.Sum(g => g.Count(c => c > 1));

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

            InputToGrid();

            DrawLines(true);

            result = grid.Sum(g => g.Count(c => c > 1));

            #endregion

            var ms = Math.Round((DateTime.Now - start).TotalMilliseconds);

            if (result > 0) Answer2 = result.ToString();
            return $"Part 2 ({ms}ms): {result} ";
        }

        static void InputToGrid()
        {
            grid = new List<List<int>>();

            (int maxX, int maxY) = (0, 0);

            foreach (var i in inputs)
                (maxX, maxY) = (Math.Max(maxX, Math.Max(int.Parse(i.Split(" ")[0].Split(",")[0]), int.Parse(i.Split(" ")[2].Split(",")[0]))), Math.Max(maxY, Math.Max(int.Parse(i.Split(" ")[0].Split(",")[1]), int.Parse(i.Split(" ")[2].Split(",")[1]))));

            for (int y = 0; y < maxY + 1; y++)
                grid.Add(new List<int>(new int[maxX + 1]));
        }

        static void DrawLines(bool drawDiags)
        {
            foreach (var input in inputs)
            {
                (int x1, int y1) = (int.Parse(input.Split(" ")[0].Split(",")[0]), int.Parse(input.Split(" ")[0].Split(",")[1]));
                (int x2, int y2) = (int.Parse(input.Split(" ")[2].Split(",")[0]), int.Parse(input.Split(" ")[2].Split(",")[1]));

                if (!drawDiags && x1 != x2 && y1 != y2)
                    continue;

                grid[y1][x1]++;

                while (x1 != x2 || y1 != y2)
                {
                    if (x1 > x2)
                        x1--;
                    else if (x1 < x2)
                        x1++;

                    if (y1 > y2)
                        y1--;
                    else if (y1 < y2)
                        y1++;

                    grid[y1][x1]++;
                }
            }
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
                Thread.Sleep(5000);
                VisualizePart2();
            }

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Cyan;
        }

        static void VisualizePart1()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            for (int i = 3; i > 0; i--)
            {
                Console.Clear();
                Console.WriteLine($"Visualization for 2021.5.1");
                Console.WriteLine($"Starting in {i}");
                Thread.Sleep(1000);
            }

            InputToGrid();

            DrawLinesWithVisualization(false);

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(grid.Sum(g => g.Count(c => c > 1)));
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(" dangerous areas");
        }

        static void VisualizePart2()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            for (int i = 3; i > 0; i--)
            {
                Console.Clear();
                Console.WriteLine($"Visualization for 2021.5.2");
                Console.WriteLine($"Starting in {i}");
                Thread.Sleep(1000);
            }

            InputToGrid();

            DrawLinesWithVisualization(true);

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(grid.Sum(g => g.Count(c => c > 1)));
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(" dangerous areas");
        }

        static void DrawLinesWithVisualization(bool drawDiags)
        {
            foreach (var input in inputs)
            {
                (int x1, int y1) = (int.Parse(input.Split(" ")[0].Split(",")[0]), int.Parse(input.Split(" ")[0].Split(",")[1]));
                (int x2, int y2) = (int.Parse(input.Split(" ")[2].Split(",")[0]), int.Parse(input.Split(" ")[2].Split(",")[1]));

                if (!drawDiags && x1 != x2 && y1 != y2)
                    continue;

                grid[y1][x1]++;

                while (x1 != x2 || y1 != y2)
                {
                    if (x1 > x2)
                        x1--;
                    else if (x1 < x2)
                        x1++;

                    if (y1 > y2)
                        y1--;
                    else if (y1 < y2)
                        y1++;

                    grid[y1][x1]++;
                }

                Console.Clear();
                PrintGrid();
                Thread.Sleep(1000);
            }
        }

        static void PrintGrid()
        {
            foreach (var y in grid)
            {
                foreach (var x in y)
                {
                    if (x > 1)
                        Console.ForegroundColor = ConsoleColor.Red;
                    else if (x > 0)
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    else
                        Console.ForegroundColor = ConsoleColor.White;

                    Console.Write($"{(x == 0 ? "." : x)} ");
                }
                Console.WriteLine();
            }
        }
    }
}