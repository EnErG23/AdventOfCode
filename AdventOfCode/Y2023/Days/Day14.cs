using AdventOfCode.Helpers;
using AdventOfCode.Models;

namespace AdventOfCode.Y2023.Days
{
    public class Day14 : Day
    {
        private List<(int, int)> _cubedRocks;
        private List<(int, int)> _roundedRocks;
        private int _rows;
        private int _cols;

        public Day14(int year, int day, bool test) : base(year, day, test)
        {
            _cubedRocks = new();
            _roundedRocks = new();
            _rows = Inputs.Count;
            _cols = Inputs[0].Length;

            for (int r = 0; r < _rows; r++)
                for (int c = 0; c < _cols; c++)
                    if (Inputs[r][c] == '#')
                        _cubedRocks.Add((r, c));
                    else if (Inputs[r][c] == 'O')
                        _roundedRocks.Add((r, c));
        }

        public override string RunPart1() => TiltNorth(_roundedRocks).Sum(r => _rows - r.Item1).ToString();

        public override string RunPart2() => Cycle(1000000000).Sum(r => _rows - r.Item1).ToString();

        public override void VisualizePart2()
        {
            List<List<(int, int)>> platformHistory = new() { _roundedRocks };

            var newRoundedRocks = platformHistory.Last();

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Starting State:");
            Print(newRoundedRocks);

            Thread.Sleep(1000);

            for (int i = 0; i < 1000000000; i++)
            {
                newRoundedRocks = TiltNorth(newRoundedRocks).ToList();
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Cycle {i} (Tilt North):");
                Print(newRoundedRocks);

                Thread.Sleep(500);

                newRoundedRocks = TiltWest(newRoundedRocks).ToList();
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Cycle {i} (Tilt West):");
                Print(newRoundedRocks);

                Thread.Sleep(500);

                newRoundedRocks = TiltSouth(newRoundedRocks).ToList();
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Cycle {i} (Tilt South):");
                Print(newRoundedRocks);

                Thread.Sleep(500);

                newRoundedRocks = TiltEast(newRoundedRocks).ToList();
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Cycle {i} (Tilt East):");
                Print(newRoundedRocks);

                Thread.Sleep(500);

                // Check if state has occured before (Probably a more efficient way, but I've spend enough time on fixing this shit.)
                if (platformHistory.Select(p => string.Join("", p.Select(m => m.Item1 + " " + m.Item1))).ToList().Contains(string.Join("", newRoundedRocks.Select(m => m.Item1 + " " + m.Item1))))
                {
                    // Determine loop properties
                    var loopFirstIndex = platformHistory.Select(p => string.Join("", p.Select(m => m.Item1 + " " + m.Item1))).ToList().LastIndexOf(string.Join("", newRoundedRocks.Select(m => m.Item1 + " " + m.Item1))) - 1;
                    var loopLastIndex = i - 1;
                    var loopItems = loopLastIndex - loopFirstIndex + 1;

                    // Check where index % loop length is 0
                    int offset = Enumerable.Range(loopFirstIndex, loopLastIndex).First(i => i % loopItems == 0);

                    // Use mod to determine which state to take after offset
                    int mod = 1000000000 % loopItems;

                    // Get index of state after 1.000.000.000 cycles (if index would be out of range, take value before offset)
                    int index = offset + mod > platformHistory.Count ? offset - (offset - mod) : offset + mod;
                    break;
                }
                else
                    platformHistory.Add(newRoundedRocks);
            }
        }

        private List<(int, int)> Cycle(int cycles)
        {
            List<List<(int, int)>> platformHistory = new() { _roundedRocks };

            for (int i = 0; i < cycles; i++)
            {
                var newRoundedRocks = TiltEast(TiltSouth(TiltWest(TiltNorth(platformHistory.Last()))));

                // Check if state has occured before (Probably a more efficient way, but I've spend enough time on fixing this shit.)
                if (platformHistory.Select(p => string.Join("", p.Select(m => m.Item1 + " " + m.Item1))).ToList().Contains(string.Join("", newRoundedRocks.Select(m => m.Item1 + " " + m.Item1))))
                {
                    // Determine loop properties
                    var loopFirstIndex = platformHistory.Select(p => string.Join("", p.Select(m => m.Item1 + " " + m.Item1))).ToList().LastIndexOf(string.Join("", newRoundedRocks.Select(m => m.Item1 + " " + m.Item1))) - 1;
                    var loopLastIndex = i - 1;
                    var loopItems = loopLastIndex - loopFirstIndex + 1;

                    // Check where index % loop length is 0
                    int offset = Enumerable.Range(loopFirstIndex, loopLastIndex).First(i => i % loopItems == 0);

                    // Use mod to determine which state to take after offset
                    int mod = cycles % loopItems;

                    // Get index of state after 1.000.000.000 cycles (if index would be out of range, take value before offset)
                    int index = offset + mod > platformHistory.Count ? offset - (offset - mod) : offset + mod;

                    return platformHistory[index];
                }
                else
                    platformHistory.Add(newRoundedRocks);
            }

            return platformHistory.Last();
        }

        private List<(int, int)> TiltNorth(List<(int, int)> roundedRocks)
        {
            List<(int, int)> newRoundedRocks = new();

            foreach (var roundedRock in roundedRocks.OrderBy(r => r.Item1))
                for (int rd = roundedRock.Item1 - 1; rd >= -1; rd--)
                    if (rd == -1 || _cubedRocks.Contains((rd, roundedRock.Item2)) || newRoundedRocks.Contains((rd, roundedRock.Item2)))
                    {
                        newRoundedRocks.Add((rd + 1, roundedRock.Item2));
                        break;
                    }

            return newRoundedRocks;
        }

        private List<(int, int)> TiltEast(List<(int, int)> roundedRocks)
        {
            List<(int, int)> newRoundedRocks = new();

            foreach (var roundedRock in roundedRocks.OrderByDescending(r => r.Item2))
                for (int cd = roundedRock.Item2; cd <= _cols; cd++)
                    if (cd == _cols || _cubedRocks.Contains((roundedRock.Item1, cd)) || newRoundedRocks.Contains((roundedRock.Item1, cd)))
                    {
                        newRoundedRocks.Add((roundedRock.Item1, cd - 1));
                        break;
                    }

            return newRoundedRocks;
        }

        private List<(int, int)> TiltSouth(List<(int, int)> roundedRocks)
        {
            List<(int, int)> newRoundedRocks = new();

            foreach (var roundedRock in roundedRocks.OrderByDescending(r => r.Item1))
                for (int rd = roundedRock.Item1 + 1; rd <= _rows; rd++)
                    if (rd == _rows || _cubedRocks.Contains((rd, roundedRock.Item2)) || newRoundedRocks.Contains((rd, roundedRock.Item2)))
                    {
                        newRoundedRocks.Add((rd - 1, roundedRock.Item2));
                        break;
                    }

            return newRoundedRocks;
        }

        private List<(int, int)> TiltWest(List<(int, int)> roundedRocks)
        {
            List<(int, int)> newRoundedRocks = new();

            foreach (var roundedRock in roundedRocks.OrderBy(r => r.Item2))
                for (int cd = roundedRock.Item2 - 1; cd >= -1; cd--)
                    if (cd == -1 || _cubedRocks.Contains((roundedRock.Item1, cd)) || newRoundedRocks.Contains((roundedRock.Item1, cd)))
                    {
                        newRoundedRocks.Add((roundedRock.Item1, cd + 1));
                        break;
                    }

            return newRoundedRocks;
        }

        private void Print(List<(int, int)> newRoundedRocks)
        {
            for (int r = 0; r < _rows; r++)
            {
                for (int c = 0; c < _cols; c++)
                {
                    if (_cubedRocks.Contains((r, c)))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("#");
                    }
                    else if (newRoundedRocks.Contains((r, c)))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("O");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write(".");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}