﻿using AdventOfCode.Helpers;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Y2020.Days
{
    public class Day24
    {
        static readonly int day = 24;
        static List<string>? inputs;
        public static List<List<bool>>? tiles;

        public static string? Answer1 { get; set; }
        public static string? Answer2 { get; set; }

        public static void Run(int part, bool test)
        {
            Stopwatch sw = Stopwatch.StartNew();

            inputs = InputManager.GetInputAsStrings(day, test);
            tiles = new List<List<bool>>();

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

        private static string Part1()
        {
            Stopwatch sw = Stopwatch.StartNew();

            long result = 0;

            #region Solution

            List<bool> startRow = new List<bool>();
            tiles.Add(startRow);

            bool startTile = false;
            startRow.Add(startTile);

            foreach (var input in inputs)
            {
                var y = 0;
                var x = 0;

                var prevDir = 'a';
                var shifted = false;

                foreach (var dir in input)
                {
                    switch (dir)
                    {
                        case 'n':
                            y--;
                            shifted = !shifted;
                            break;
                        case 'e':
                            if (shifted || prevDir == 'e' || prevDir == 'w' || prevDir == 'a')
                                x++;
                            break;
                        case 's':
                            y++;
                            shifted = !shifted;
                            break;
                        default:
                            if (!shifted || prevDir == 'e' || prevDir == 'w' || prevDir == 'a')
                                x--;
                            break;
                    }

                    prevDir = dir;
                }

                var startTileY = ((tiles.Count - 1) / 2);
                var startTileX = ((tiles.ElementAt(startTileY).Count - 1) / 2);

                // Enlarge grid height if necessary
                if (Math.Abs(y) > startTileY)
                {
                    var newTiles = new List<List<bool>>();
                    var newRow = new List<bool>();

                    for (int i = 0; i < tiles.ElementAt(0).Count; i++)
                    {
                        newRow.Add(false);
                    }

                    for (int i = 0; i < (Math.Abs(y) - startTileY); i++)
                    {
                        newTiles.Add(newRow.ToList());
                    }
                    newTiles.AddRange(tiles);
                    for (int i = 0; i < (Math.Abs(y) - startTileY); i++)
                    {
                        newTiles.Add(newRow.ToList());
                    }

                    tiles = newTiles;
                }

                // Enlarge grid width if necessary
                if (Math.Abs(x) > startTileX)
                {
                    var newTiles = new List<List<bool>>();

                    foreach (var row in tiles)
                    {
                        var newRow = new List<bool>();

                        for (int i = 0; i < (Math.Abs(x) - startTileX); i++)
                        {
                            newRow.Add(false);
                        }

                        newRow.AddRange(row);

                        for (int i = 0; i < (Math.Abs(x) - startTileX); i++)
                        {
                            newRow.Add(false);
                        }

                        newTiles.Add(newRow);
                    }

                    tiles = newTiles;
                }

                // Calculate tile coords
                startTileY = ((tiles.Count - 1) / 2);
                startTileX = ((tiles.ElementAt(startTileY).Count - 1) / 2);
                var destY = startTileY + y;
                var destX = startTileX + x;

                // Flip tile
                tiles[destY][destX] = !tiles[destY][destX];
            }

            //WriteTiles(tiles);
            //Console.WriteLine();

            foreach (var row in tiles)
                foreach (var tile in row)
                    if (tile)
                        result++;

            #endregion

            sw.Stop();
			var ms = sw.Elapsed.TotalMilliseconds;

            if (result > 0) Answer1 = result.ToString();
            return $"Part 1 ({ms}ms): {result} ";
        }

        private static string Part2()
        {
            Stopwatch sw = Stopwatch.StartNew();

            long result = 0;

            #region Solution

            for (int i = 0; i < 100; i++)
            {
                // Any black tile with zero or more than 2 black tiles immediately adjacent to it is flipped to white.
                // Any white tile with exactly 2 black tiles immediately adjacent to it is flipped to black.
            }

            #endregion

            sw.Stop();
			var ms = sw.Elapsed.TotalMilliseconds;

            if (result > 0) Answer2 = result.ToString();
            return $"Part 2 ({ms}ms): {result} ";
        }

        static void WriteTiles(List<List<bool>> tiles)
        {
            foreach (var row in tiles)
            {
                var line = "";

                foreach (var tile in row)
                {
                    if (tile)
                        line += "B";
                    else
                        line += "W";
                }

                Console.WriteLine(line);
            }
        }
    }
}