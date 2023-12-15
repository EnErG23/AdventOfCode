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

        public override string RunPart2() => Cycle(_roundedRocks, 1000000000).Sum(r => _rows - r.Item1).ToString(); //Test ? "64" : "104671"; 

        public override void VisualizePart2()
        {
            var newRoundedRocks = _roundedRocks.ToList();

            for (int i = 0; i < 1000000000; i++)
            {
                //newRoundedRocks = TiltNorth(newRoundedRocks).ToList();

                //Console.WriteLine();
                //Console.WriteLine($"Tilt North:");

                //for (int r = 0; r < _rows; r++)
                //{
                //    for (int c = 0; c < _cols; c++)
                //    {
                //        if (_cubedRocks.Contains((r, c)))
                //            Console.Write("#");
                //        else if (newRoundedRocks.Contains((r, c)))
                //            Console.Write("O");
                //        else
                //            Console.Write(".");
                //    }
                //    Console.WriteLine();
                //}

                //newRoundedRocks = TiltWest(newRoundedRocks).ToList();

                //Console.WriteLine();
                //Console.WriteLine($"Tilt West:");

                //for (int r = 0; r < _rows; r++)
                //{
                //    for (int c = 0; c < _cols; c++)
                //    {
                //        if (_cubedRocks.Contains((r, c)))
                //            Console.Write("#");
                //        else if (newRoundedRocks.Contains((r, c)))
                //            Console.Write("O");
                //        else
                //            Console.Write(".");
                //    }
                //    Console.WriteLine();
                //}

                //newRoundedRocks = TiltSouth(newRoundedRocks).ToList();

                //Console.WriteLine();
                //Console.WriteLine($"Tilt South:");

                //for (int r = 0; r < _rows; r++)
                //{
                //    for (int c = 0; c < _cols; c++)
                //    {
                //        if (_cubedRocks.Contains((r, c)))
                //            Console.Write("#");
                //        else if (newRoundedRocks.Contains((r, c)))
                //            Console.Write("O");
                //        else
                //            Console.Write(".");
                //    }
                //    Console.WriteLine();
                //}

                //newRoundedRocks = TiltEast(newRoundedRocks).ToList();

                //Console.WriteLine();
                //Console.WriteLine($"Tilt East:");

                //for (int r = 0; r < _rows; r++)
                //{
                //    for (int c = 0; c < _cols; c++)
                //    {
                //        if (_cubedRocks.Contains((r, c)))
                //            Console.Write("#");
                //        else if (newRoundedRocks.Contains((r, c)))
                //            Console.Write("O");
                //        else
                //            Console.Write(".");
                //    }
                //    Console.WriteLine();
                //}

                //Console.WriteLine($"{string.Join(", ", newRoundedRocks.OrderBy(n => n.Item1).ThenBy(n => n.Item2))}");

                newRoundedRocks = TiltEast(TiltSouth(TiltWest(TiltNorth(newRoundedRocks))));
                Console.WriteLine($"After cycle {i}: {newRoundedRocks.Sum(r => _rows - r.Item1)}");

                if (i % 100 == 0)
                    Console.Read();
                //Console.WriteLine($"After cycle {i}: {newRoundedRocks.Sum(r => _rows - r.Item1)}");
            }
        }

        public List<(int, int)> Cycle(List<(int, int)> roundedRocks, int cycles)
        {
            List<List<(int, int)>> platformHistory = new() { roundedRocks };

            for (int i = 0; i < cycles; i++)
            {
                var newRoundedRocks = TiltEast(TiltSouth(TiltWest(TiltNorth(platformHistory.Last()))));
                if (i > 135)
                    Console.WriteLine(i + ": " + newRoundedRocks.Sum(r => _rows - r.Item1));

                if (platformHistory.Select(p => string.Join("", p.Select(m => m.Item1 + " " + m.Item1))).ToList().Contains(string.Join("", newRoundedRocks.Select(m => m.Item1 + " " + m.Item1))))
                {
                    var firstLoopIndex = platformHistory.Select(p => string.Join("", p.Select(m => m.Item1 + " " + m.Item1))).ToList().LastIndexOf(string.Join("", newRoundedRocks.Select(m => m.Item1 + " " + m.Item1))) - 1;
                    var lastLoopIndex = i - 1;
                    Console.WriteLine($"First repeat at {i}");
                    Console.WriteLine($"Previous occurence: {firstLoopIndex}");
                    Console.WriteLine($"Loop length: {lastLoopIndex - firstLoopIndex + 1}");
                    Console.WriteLine($"Mod needed: {cycles % (lastLoopIndex - firstLoopIndex)}");
                    Console.WriteLine($"1.000.000.000 score: {platformHistory[(cycles % (lastLoopIndex - firstLoopIndex)) + firstLoopIndex].Sum(r => _rows - r.Item1)}");

                    //TODO: FIX THIS SHIT
                    return platformHistory[(cycles % (lastLoopIndex - firstLoopIndex)) + firstLoopIndex];
                }
                else
                    platformHistory.Add(newRoundedRocks);
            }

            return platformHistory.Last();
        }

        public List<(int, int)> TiltNorth(List<(int, int)> roundedRocks)
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

        public List<(int, int)> TiltEast(List<(int, int)> roundedRocks)
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

        public List<(int, int)> TiltSouth(List<(int, int)> roundedRocks)
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

        public List<(int, int)> TiltWest(List<(int, int)> roundedRocks)
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
    }
}