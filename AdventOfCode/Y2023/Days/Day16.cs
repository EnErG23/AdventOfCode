using AdventOfCode.Helpers;
using AdventOfCode.Models;
using AdventOfCode.Y2021.Days;
using System;

namespace AdventOfCode.Y2023.Days
{
    public class Day16 : Day
    {
        private (int, int) _dimensions;
        private int[] _rowDiffs = new[] { -1, 0, 1, 0 };
        private int[] _colDiffs = new[] { 0, 1, 0, -1 };

        public Day16(int year, int day, bool test) : base(year, day, test) => _dimensions = (Inputs.Count, Inputs[0].Length);

        public override string RunPart1() => Beam((0, 0, 1)).ToString();

        public override string RunPart2()
        {
            var maxEnergized = 0;

            for (int r = 0; r < _dimensions.Item1; r++)
                maxEnergized = Math.Max(maxEnergized, Math.Max(Beam((r, 0, 1)), Beam((r, _dimensions.Item2 - 1, 3))));

            for (int c = 0; c < _dimensions.Item2; c++)
                maxEnergized = Math.Max(maxEnergized, Math.Max(Beam((0, c, 2)), Beam((_dimensions.Item1 - 1, c, 0))));

            return maxEnergized.ToString();
        }

        private int Beam((int, int, int) rcd)
        {
            HashSet<(int, int, int)> toCheckRcds = new() { rcd };
            HashSet<(int, int)> energizedSpaces = new();
            HashSet<(int, int, int)> checkedRcds = new();

            while (true)
            {
                HashSet<(int, int, int)> newToCheckRcds = new();

                if (toCheckRcds.Count == 0)
                    break;

                foreach (var toCheckRcd in toCheckRcds)
                {
                    int r = toCheckRcd.Item1;
                    int c = toCheckRcd.Item2;
                    int d = toCheckRcd.Item3;

                    if (0 <= r && r < _dimensions.Item1 && 0 <= c && c < _dimensions.Item2)
                        energizedSpaces.Add((r, c));
                    else
                        continue;

                    if (checkedRcds.Contains((r, c, d)))
                        continue;

                    checkedRcds.Add((r, c, d));

                    switch (Inputs[r][c])
                    {
                        case '/':
                            switch (d)
                            {
                                case 0:
                                    newToCheckRcds.Add(Next(r, c, 1));
                                    break;
                                case 1:
                                    newToCheckRcds.Add(Next(r, c, 0));
                                    break;
                                case 2:
                                    newToCheckRcds.Add(Next(r, c, 3));
                                    break;
                                case 3:
                                    newToCheckRcds.Add(Next(r, c, 2));
                                    break;
                            }
                            break;
                        case '\\':
                            switch (d)
                            {
                                case 0:
                                    newToCheckRcds.Add(Next(r, c, 3));
                                    break;
                                case 1:
                                    newToCheckRcds.Add(Next(r, c, 2));
                                    break;
                                case 2:
                                    newToCheckRcds.Add(Next(r, c, 1));
                                    break;
                                case 3:
                                    newToCheckRcds.Add(Next(r, c, 0));
                                    break;
                            }
                            break;
                        case '|':
                            switch (d)
                            {
                                case 0:
                                    newToCheckRcds.Add(Next(r, c, 0));
                                    break;
                                case 2:
                                    newToCheckRcds.Add(Next(r, c, 2));
                                    break;
                                case 1:
                                case 3:
                                    newToCheckRcds.Add(Next(r, c, 0));
                                    newToCheckRcds.Add(Next(r, c, 2));
                                    break;
                            }
                            break;
                        case '-':
                            switch (d)
                            {
                                case 1:
                                    newToCheckRcds.Add(Next(r, c, 1));
                                    break;
                                case 3:
                                    newToCheckRcds.Add(Next(r, c, 3));
                                    break;
                                case 0:
                                case 2:
                                    newToCheckRcds.Add(Next(r, c, 1));
                                    newToCheckRcds.Add(Next(r, c, 3));
                                    break;
                            }
                            break;
                        default:
                            newToCheckRcds.Add(Next(r, c, d));
                            break;
                    }
                }

                toCheckRcds = newToCheckRcds;
            }

            return energizedSpaces.Count;
        }

        private (int, int, int) Next(int r, int c, int d) => (r + _rowDiffs[d], c + _colDiffs[d], d);
    }
}