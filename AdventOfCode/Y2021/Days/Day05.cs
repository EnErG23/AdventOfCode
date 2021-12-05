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

            var maxX = 0;
            var maxY = 0;

            foreach (var input in inputs)
            {
                (int x1, int y1) = (int.Parse(input.Split(" ")[0].Split(",")[0]), int.Parse(input.Split(" ")[0].Split(",")[1]));
                (int x2, int y2) = (int.Parse(input.Split(" ")[2].Split(",")[0]), int.Parse(input.Split(" ")[2].Split(",")[1]));

                maxX = Math.Max(maxX, Math.Max(x1, x2));
                maxY = Math.Max(maxY, Math.Max(y1, y2));
            }

            var grid = new List<List<int>>();
            for (int y = 0; y < maxY + 1; y++)
            {
                var row = new List<int>();

                for (int x = 0; x < maxX + 1; x++)
                {
                    row.Add(0);
                }

                grid.Add(row);
            }

            foreach (var input in inputs)
            {
                (int x1, int y1) = (int.Parse(input.Split(" ")[0].Split(",")[0]), int.Parse(input.Split(" ")[0].Split(",")[1]));
                (int x2, int y2) = (int.Parse(input.Split(" ")[2].Split(",")[0]), int.Parse(input.Split(" ")[2].Split(",")[1]));

                if (x1 != x2 && y1 != y2)
                    continue;

                if (x1 > x2)
                    while (x1 >= x2)
                    {
                        grid[y1][x1]++;

                        if (y1 > y2)
                            y1--;
                        else if (y1 < y2)
                            y1++;

                        x1--;
                    }
                else if (x1 < x2)
                    while (x1 <= x2)
                    {
                        grid[y1][x1]++;

                        if (y1 > y2)
                            y1--;
                        else if (y1 < y2)
                            y1++;

                        x1++;
                    }
                else
                {

                    if (y1 > y2)
                        while (y1 >= y2)
                        {
                            grid[y1][x1]++;
                            y1--;
                        }
                    else if (y1 < y2)
                        while (y1 <= y2)
                        {
                            grid[y1][x1]++;
                            y1++;
                        }
                    else
                        grid[y1][x1]++;
                }
            }

            result = grid.Sum(g => g.Count(c => c > 1));

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

            var maxX = 0;
            var maxY = 0;

            foreach (var input in inputs)
            {
                (int x1, int y1) = (int.Parse(input.Split(" ")[0].Split(",")[0]), int.Parse(input.Split(" ")[0].Split(",")[1]));
                (int x2, int y2) = (int.Parse(input.Split(" ")[2].Split(",")[0]), int.Parse(input.Split(" ")[2].Split(",")[1]));

                maxX = Math.Max(maxX, Math.Max(x1, x2));
                maxY = Math.Max(maxY, Math.Max(y1, y2));
            }

            var grid = new List<List<int>>();
            for (int y = 0; y < maxY + 1; y++)
            {
                var row = new List<int>();

                for (int x = 0; x < maxX + 1; x++)
                {
                    row.Add(0);
                }

                grid.Add(row);
            }

            foreach (var input in inputs)
            {
                (int x1, int y1) = (int.Parse(input.Split(" ")[0].Split(",")[0]), int.Parse(input.Split(" ")[0].Split(",")[1]));
                (int x2, int y2) = (int.Parse(input.Split(" ")[2].Split(",")[0]), int.Parse(input.Split(" ")[2].Split(",")[1]));

                if (x1 > x2)
                    while (x1 >= x2)
                    {
                        grid[y1][x1]++;

                        if (y1 > y2)
                            y1--;
                        else if (y1 < y2)
                            y1++;

                        x1--;
                    }
                else if (x1 < x2)
                    while (x1 <= x2)
                    {
                        grid[y1][x1]++;

                        if (y1 > y2)
                            y1--;
                        else if (y1 < y2)
                            y1++;

                        x1++;
                    }
                else
                {

                    if (y1 > y2)
                        while (y1 >= y2)
                        {
                            grid[y1][x1]++;
                            y1--;
                        }
                    else if (y1 < y2)
                        while (y1 <= y2)
                        {
                            grid[y1][x1]++;
                            y1++;
                        }
                    else
                        grid[y1][x1]++;
                }
            }

            result = grid.Sum(g => g.Count(c => c > 1));

            #endregion

            var ms = Math.Round((DateTime.Now - start).TotalMilliseconds);

            if (result > 0) Answer2 = result.ToString();
            return $"Part 2 ({ms}ms): {result} ";
        }        
    }
}