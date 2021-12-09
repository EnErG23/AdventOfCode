using AdventOfCode.Models;

namespace AdventOfCode.Y2021.Days
{
    public class Day09 : Day
    {
        private List<List<int>>? locations;
        private List<(int, int)>? basinLocations;

        public Day09(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
        {
            locations = Inputs.Select(i => i.ToList().Select(c => Convert.ToInt32(c.ToString())).ToList()).ToList();

            var lowPoints = GetLowPoints(locations);

            return (lowPoints.Sum(l => locations[l.Item1][l.Item2]) + lowPoints.Count).ToString();
        }

        public override string RunPart2()
        {
            if (locations is null)
                locations = Inputs.Select(i => i.ToList().Select(c => Convert.ToInt32(c.ToString())).ToList()).ToList();

            List<int> basins = new();

            foreach (var lowPoint in GetLowPoints(locations))
            {
                basinLocations = new();

                CheckNeighbours(lowPoint, 'o');

                basins.Add(basinLocations.Count());
            }

            return basins.OrderByDescending(b => b).Take(3).Aggregate(1, (x, y) => x * y).ToString();
        }

        private List<(int, int)> GetLowPoints(List<List<int>> locations)
        {
            List<(int, int)> lowPoints = new();

            for (int r = 0; r < locations.Count; r++)
            {
                for (int c = 0; c < locations[r].Count; c++)
                {
                    var height = locations[r][c];

                    var lHeight = c == 0 ? 9 : locations[r][c - 1];
                    var rHeight = c == locations[r].Count - 1 ? 9 : locations[r][c + 1];
                    var aHeight = r == 0 ? 9 : locations[r - 1][c];
                    var bHeight = r == locations.Count - 1 ? 9 : locations[r + 1][c];

                    if (height < lHeight && height < rHeight && height < aHeight && height < bHeight)
                        lowPoints.Add((r, c));
                }
            }

            return lowPoints;
        }

        private void CheckNeighbours((int, int) location, char origin)
        {
            var r = location.Item1;
            var c = location.Item2;

            var height = locations[r][c];

            if (height < 9)
            {
                basinLocations.Add((r, c));

                if (c - 1 >= 0 && origin != 'w' && !basinLocations.Contains((r, c - 1)))
                    CheckNeighbours((r, c - 1), 'e');
                if (c + 1 < locations[0].Count && origin != 'e' && !basinLocations.Contains((r, c + 1)))
                    CheckNeighbours((r, c + 1), 'w');
                if (r - 1 >= 0 && origin != 's' && !basinLocations.Contains((r - 1, c)))
                    CheckNeighbours((r - 1, c), 'n');
                if (r + 1 < locations.Count && origin != 'n' && !basinLocations.Contains((r + 1, c)))
                    CheckNeighbours((r + 1, c), 's');
            }
        }
    }
}