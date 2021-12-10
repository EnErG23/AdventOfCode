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

            var lowPoints = GetLowPoints(locations);

            List<long> basins = new();

            foreach (var lowPoint in lowPoints)
            {
                basinLocations = new();

                CheckNeighbours(lowPoint);

                basins.Add(basinLocations.Count());
            }

            basins = basins.OrderByDescending(b => b).ToList();

            return (basins[0] * basins[1] * basins[2]).ToString();
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

        private void CheckNeighbours((int, int) location)
        {
            var r = location.Item1;
            var c = location.Item2;

            var height = locations[r][c];

            if (height < 9 && !basinLocations.Contains(location))
            {
                basinLocations.Add((r, c));

                if (c - 1 >= 0)
                    CheckNeighbours((r, c - 1));
                if (c + 1 < locations[0].Count)
                    CheckNeighbours((r, c + 1));
                if (r - 1 >= 0)
                    CheckNeighbours((r - 1, c));
                if (r + 1 < locations.Count)
                    CheckNeighbours((r + 1, c));
            }
        }

        public override void VisualizePart2()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            for (int i = 0; i < 9; i++)
            {
                Console.Clear();

                foreach (var input in Inputs.Take((int)(Inputs.Count / 2.5)))
                {
                    foreach (var height in input.Take((int)(input.Length / 1.5)))
                    {
                        if (height.ToString() == i.ToString())
                            Console.ForegroundColor = ConsoleColor.Red;
                        else if (int.Parse(height.ToString()) < i)
                            Console.ForegroundColor = ConsoleColor.Yellow;
                        else
                            Console.ForegroundColor = ConsoleColor.Black;

                        Console.Write("■ ");
                    }
                    Console.WriteLine();
                }

                Thread.Sleep(500);
            }

            Console.Clear();

            foreach (var input in Inputs.Take((int)(Inputs.Count / 2.5)))
            {
                foreach (var height in input.Take((int)(input.Length / 1.5)))
                {
                    if (height == '9')
                        Console.ForegroundColor = ConsoleColor.Black;
                    else
                        Console.ForegroundColor = ConsoleColor.Yellow;

                    Console.Write("■ ");
                }
                Console.WriteLine();
            }

            Thread.Sleep(500);
        }
    }
}