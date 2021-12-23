using AdventOfCode.Models;

namespace AdventOfCode.Y2021.Days
{
    public class Day15 : Day
    {
        private List<Location>? locations;

        public Day15(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
        {
            locations = new();

            for (var r = 0; r < Inputs.Count; r++)
                for (var c = 0; c < Inputs[r].Length; c++)
                {
                    Location location = new()
                    {
                        Row = r,
                        Column = c,
                        Value = int.Parse(Inputs[r][c].ToString()),
                        DistanceFromSource = r + c > 0 ? int.MaxValue : 0
                    };

                    locations.Add(location);
                }

            return FindPath();
        }

        public override string RunPart2()
        {
            locations = new();

            for (var rOffset = 0; rOffset < 5; rOffset++)
                for (var cOffset = 0; cOffset < 5; cOffset++)
                    for (var r = 0; r < Inputs.Count; r++)
                        for (var c = 0; c < Inputs[r].Length; c++)
                        {
                            Location location = new()
                            {
                                Row = r + (rOffset * Inputs.Count),
                                Column = c + (cOffset * Inputs[r].Length),
                                Value = (int.Parse(Inputs[r][c].ToString()) + rOffset + cOffset) > 9 ? (int.Parse(Inputs[r][c].ToString()) + rOffset + cOffset) - 9 : (int.Parse(Inputs[r][c].ToString()) + rOffset + cOffset),
                                DistanceFromSource = r + rOffset + c + cOffset > 0 ? int.MaxValue : 0
                            };

                            locations.Add(location);
                        }

            return FindPath();
        }

        private string FindPath()
        {
            var lastLocation = locations.OrderBy(l => l.Row + l.Column).Last();

            while (!lastLocation.IsVisited)
            {
                Location visitingLocation = locations.Where(l => !l.IsVisited).OrderBy(l => l.DistanceFromSource).First();

                // Top
                if (visitingLocation.Row > 0)
                {
                    var topLocation = locations.First(l => l.Row == visitingLocation.Row - 1 && l.Column == visitingLocation.Column);

                    topLocation.DistanceFromSource = Math.Min(topLocation.DistanceFromSource, visitingLocation.DistanceFromSource + topLocation.Value);
                }

                // Right
                if (visitingLocation.Column < locations.Max(l => l.Column))
                {
                    var rightLocation = locations.First(l => l.Row == visitingLocation.Row && l.Column == visitingLocation.Column + 1);

                    rightLocation.DistanceFromSource = Math.Min(rightLocation.DistanceFromSource, visitingLocation.DistanceFromSource + rightLocation.Value);
                }

                // Bottom
                if (visitingLocation.Row < locations.Max(l => l.Row))
                {
                    var bottomLocation = locations.First(l => l.Row == visitingLocation.Row + 1 && l.Column == visitingLocation.Column);

                    bottomLocation.DistanceFromSource = Math.Min(bottomLocation.DistanceFromSource, visitingLocation.DistanceFromSource + bottomLocation.Value);
                }

                // Left
                if (visitingLocation.Column > 0)
                {
                    var leftLocation = locations.First(l => l.Row == visitingLocation.Row && l.Column == visitingLocation.Column - 1);

                    leftLocation.DistanceFromSource = Math.Min(leftLocation.DistanceFromSource, visitingLocation.DistanceFromSource + leftLocation.Value);
                }

                visitingLocation.IsVisited = true;
            }

            return lastLocation.DistanceFromSource.ToString();
        }
    }

    public class Location
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public int Value { get; set; }
        public long DistanceFromSource { get; set; }
        public bool IsVisited { get; set; }
    }
}