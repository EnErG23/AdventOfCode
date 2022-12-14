using AdventOfCode.Models;

namespace AdventOfCode.Y2022.Days
{
    public class Day14 : Day
    {
        private List<List<char>> _cave;
        private (int, int) _sandOrigin;
        private int _minR;
        private int _maxR;
        private int _minC;
        private int _maxC;

        public Day14(int year, int day, bool test) : base(year, day, test)
        {
            _sandOrigin = (0, 500);

            List<(int, int)> locations = new() { _sandOrigin };

            foreach (var input in Inputs)
            {
                var pathPoints = input.Split(" -> ").ToList();

                for (int i = 0; i < pathPoints.Count - 1; i++)
                {
                    var from = (int.Parse(pathPoints[i].Split(",")[1]), int.Parse(pathPoints[i].Split(",")[0]));
                    var to = (int.Parse(pathPoints[i + 1].Split(",")[1]), int.Parse(pathPoints[i + 1].Split(",")[0]));

                    for (int r = Math.Min(from.Item1, to.Item1); r <= Math.Max(from.Item1, to.Item1); r++)
                        for (int c = Math.Min(from.Item2, to.Item2); c <= Math.Max(from.Item2, to.Item2); c++)
                            locations.Add((r, c));
                }
            }

            _minR = locations.Min(l => l.Item1);
            _maxR = locations.Max(l => l.Item1);
            _minC = locations.Min(l => l.Item2);
            _maxC = locations.Max(l => l.Item2);

            _cave = new();

            for (int r = _minR; r <= _maxR; r++)
            {
                List<char> row = new();

                for (int c = _minC; c <= _maxC; c++)
                    row.Add(locations.Exists(l => l == (r, c)) ? '#' : '.');

                _cave.Add(row);
            }

            _cave[_sandOrigin.Item1 - _minR][_sandOrigin.Item2 - _minC] = '+';
        }

        public override string RunPart1()
            => DropSand(_sandOrigin).ToString();

        public override string RunPart2()
        {
            string expandRow = "";

            for (int i = 0; i < _cave.Count; i++)
                expandRow += ".";

            for (int i = 0; i < _cave.Count; i++)
            {
                List<char> row = expandRow.ToCharArray().ToList();
                row.AddRange(_cave[i]);
                row.AddRange(expandRow.ToCharArray().ToList());
                _cave[i] = row;
            }

            _cave.Add(_cave.Last().Select(c => '.').ToList());
            _cave.Add(_cave.Last().Select(c => '#').ToList());

            _sandOrigin.Item2 += _cave.Count - 2;

            return DropSand(_sandOrigin).ToString();
        }

        private long DropSand((int, int) sandOrigin)
        {
            while (true)
            {
                (int, int) sand = (sandOrigin.Item1 - _minR, sandOrigin.Item2 - _minC);

                while (true)
                {
                    //If blocked by # or 0
                    if (!IsAbyss(sand.Item1 + 1, sand.Item2) && IsBlocked(sand.Item1 + 1, sand.Item2))
                        //One step down and to the left
                        if (!IsAbyss(sand.Item1 + 1, sand.Item2 - 1) && IsBlocked(sand.Item1 + 1, sand.Item2 - 1))
                            //One step down and to the right
                            if (!IsAbyss(sand.Item1 + 1, sand.Item2 + 1) && IsBlocked(sand.Item1 + 1, sand.Item2 + 1))
                            {
                                //Sand rests
                                _cave[sand.Item1][sand.Item2] = '0';

                                //If full
                                if (sand.Item1 == sandOrigin.Item1 - _minR && sand.Item2 == sandOrigin.Item2 - _minC)
                                    return _cave.Sum(r => r.Count(c => c == '0'));
                                break;
                            }
                            //Sand drops down
                            else
                            {
                                sand.Item1++;
                                sand.Item2++;
                            }
                        else
                        {
                            sand.Item1++;
                            sand.Item2--;
                        }
                    else
                        sand.Item1++;

                    //Return if sand drops into the abyss
                    if (IsAbyss(sand.Item1, sand.Item2))
                        return _cave.Sum(r => r.Count(c => c == '0'));
                }
            }
        }

        public bool IsBlocked(int r, int c)
            => _cave[r][c] == '#' || _cave[r][c] == '0';

        public bool IsAbyss(int r, int c)
            => r > _cave.Count || c < 0 || c >= _cave[0].Count;

        public override void VisualizePart1()
        {
            RunPart1();
            _cave.ForEach(r => Console.WriteLine(String.Join("", r)));
        }

        public override void VisualizePart2()
        {
            RunPart2();
            _cave.ForEach(r => Console.WriteLine(String.Join("", r)));
        }
    }
}