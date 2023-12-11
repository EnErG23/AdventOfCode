using AdventOfCode.Models;

namespace AdventOfCode.Y2023.Days
{
    public class Day11 : Day
    {
        private List<List<char>> _universe;
        private List<Galaxy> _galaxies;
        private List<Path> _paths;
        private List<int> _emptyRows;
        private List<int> _emptyCols;

        public Day11(int year, int day, bool test) : base(year, day, test)
        {
            // CREATE UNIVERSE
            _universe = new();
            Inputs.ForEach(i => _universe.Add(i.ToList()));

            // EMPTY ROWS & COLS
            _emptyRows = Enumerable.Range(0, _universe.Count()).Where(i => _universe[i].TrueForAll(c => c == '.')).ToList();
            _emptyCols = Enumerable.Range(0, _universe[0].Count()).Where(i => _universe.Select(r => r[i]).ToList().TrueForAll(c => c == '.')).ToList();

            // POPULATE GALAXIES
            _galaxies = new();

            for (int r = 0; r < _universe.Count; r++)
                for (int c = 0; c < _universe[0].Count; c++)
                    if (_universe[r][c] == '#')
                        _galaxies.Add(new Galaxy((r, c)));

            // POPULATE PATHS
            _paths = new();
            List<Galaxy> calculatedGalaxies = new();

            foreach (var from in _galaxies)
            {
                calculatedGalaxies.Add(from);

                foreach (var until in _galaxies.Where(g => !calculatedGalaxies.Contains(g)))
                    _paths.Add(new Path((from, until)));
            }
        }

        public override string RunPart1() => _paths.Sum(p => Steps(p, 2)).ToString();

        public override string RunPart2() => _paths.Sum(p => Steps(p, 1000000)).ToString();

        public long Steps(Path p, int f)
        {
            long x1 = p.Galaxies.Item1.Coords.Item2;
            long y1 = p.Galaxies.Item1.Coords.Item1;
            long x2 = p.Galaxies.Item2.Coords.Item2;
            long y2 = p.Galaxies.Item2.Coords.Item1;

            long horizontalSpace = _emptyCols.Count(c => c > Math.Min(x1, x2) && c < Math.Max(x1, x2)) * (f - 1);
            long verticalSpace = _emptyRows.Count(r => r > Math.Min(y1, y2) && r < Math.Max(y1, y2)) * (f - 1);

            return Math.Max(x1, x2) + horizontalSpace - Math.Min(x1, x2) + Math.Max(y1, y2) + verticalSpace - Math.Min(y1, y2);
        }
    }

    public class Galaxy
    {
        public (int, int) Coords { get; set; }

        public Galaxy((int, int) coords)
        {
            Coords = coords;
        }
    }

    public class Path
    {
        public (Galaxy, Galaxy) Galaxies { get; set; }

        public Path((Galaxy, Galaxy) galaxies)
        {
            Galaxies = galaxies;
        }
    }
}