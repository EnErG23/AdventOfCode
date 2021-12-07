using AdventOfCode.Models;
using AdventOfCode.Helpers;

namespace AdventOfCode.Y2021.Days
{
    public class Day05 : Day
    {
        private const int day = 5;
        private List<int>? inputs;
        private List<List<int>>? grid;

        public Day05(bool test) : base(day, test) { }

        public override string RunPart1()
        {
            long result = 0;

            InputToGrid();

            DrawLines(false);

            result = grid.Sum(g => g.Count(c => c > 1));

            return result.ToString();
        }

        public override string RunPart2()
        {
            long result = 0;

            InputToGrid();

            DrawLines(true);

            result = grid.Sum(g => g.Count(c => c > 1));

            return result.ToString();
        }

        private void InputToGrid()
        {
            grid = new();

            (int maxX, int maxY) = (0, 0);

            foreach (var i in Inputs)
                (maxX, maxY) = (Math.Max(maxX, Math.Max(int.Parse(i.Split(" ")[0].Split(",")[0]), int.Parse(i.Split(" ")[2].Split(",")[0]))), Math.Max(maxY, Math.Max(int.Parse(i.Split(" ")[0].Split(",")[1]), int.Parse(i.Split(" ")[2].Split(",")[1]))));

            for (int y = 0; y < maxY + 1; y++)
                grid.Add(new List<int>(new int[maxX + 1]));
        }

        private void DrawLines(bool drawDiags)
        {
            foreach (var input in Inputs)
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

        public override void VisualizePart1()
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

        public override void VisualizePart2()
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

        private void DrawLinesWithVisualization(bool drawDiags)
        {
            foreach (var input in Inputs)
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

        private void PrintGrid()
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