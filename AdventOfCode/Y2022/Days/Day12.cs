using AdventOfCode.Models;

namespace AdventOfCode.Y2022.Days
{
    public class Day12 : Day
    {
        private readonly List<Location> _locations;
        private List<List<Location>> _paths;

        public Day12(int year, int day, bool test) : base(year, day, test)
        {
            _locations = new();
            _paths = new();

            for (var r = 0; r < Inputs.Count; r++)
                for (var c = 0; c < Inputs[r].Length; c++)
                    _locations.Add(new(r, c, Inputs[r][c], 0, false));

            foreach (var location in _locations)
            {
                var locVal = location.Value == 'E' ? 'z' : (location.Value == 'S' ? 'a' : location.Value);

                // RIGHT
                if (location.Column < _locations.Max(l => l.Column))
                {
                    var connLoc = _locations.First(l => l.Row == location.Row && l.Column == location.Column + 1);
                    var connVal = connLoc.Value == 'E' ? 'z' : (connLoc.Value == 'S' ? 'a' : connLoc.Value);

                    if (connVal - locVal < 2)
                        location.ConnectedLocations.Add(connLoc);

                }

                // BOTTOM
                if (location.Row < _locations.Max(l => l.Row))
                {
                    var connLoc = _locations.First(l => l.Row == location.Row + 1 && l.Column == location.Column);
                    var connVal = connLoc.Value == 'E' ? 'z' : (connLoc.Value == 'S' ? 'a' : connLoc.Value);

                    if (connVal - locVal < 2)
                        location.ConnectedLocations.Add(connLoc);
                }

                //TOP
                if (location.Row > 0)
                {
                    var connLoc = _locations.First(l => l.Row == location.Row - 1 && l.Column == location.Column);
                    var connVal = connLoc.Value == 'E' ? 'z' : (connLoc.Value == 'S' ? 'a' : connLoc.Value);

                    if (connVal - locVal < 2)
                        location.ConnectedLocations.Add(connLoc);
                }

                //LEFT
                if (location.Column > 0)
                {
                    var connLoc = _locations.First(l => l.Row == location.Row && l.Column == location.Column - 1);
                    var connVal = connLoc.Value == 'E' ? 'z' : (connLoc.Value == 'S' ? 'a' : connLoc.Value);

                    if (connVal - locVal < 2)
                        location.ConnectedLocations.Add(connLoc);
                }
            }
        }

        public override string RunPart1()
        {
            Location startLocation = _locations.First(l => l.Value == 'S');
            startLocation.Value = 'a';

            AddNextNode(new() { startLocation });

            return (_paths.Select(p => p.Count()).Min() - 1).ToString();
        }

        public override string RunPart2()
        {
            foreach (Location location in _locations.Where(s => s.Value == 'a'))
                AddNextNode(new() { location });

            return (_paths.Select(p => p.Count()).Min() - 1).ToString();
        }

        private void AddNextNode(List<Location> path)
        {
            var lastLocation = path.Last();

            if (lastLocation.Value == 'E')
            {
                Console.WriteLine(String.Join(" ", path.Select(p => p.Value)));
                _paths.Add(path);
            }
            else
                foreach (var connectedLocation in lastLocation.ConnectedLocations)
                    if (!path.Contains(connectedLocation) && (_paths.Count() > 0 ? (path.Count() < _paths.Select(p => p.Count()).Min()) : true))
                    {
                        var newPath = path.ToList();
                        newPath.Add(connectedLocation);

                        AddNextNode(newPath);
                    }
        }

        //private void FindPath(int maxHeightDiff, Location location, List<(int, int)> visitedLocations, Location endlocation, int steps)
        //{
        //    if (_cache.Contains($"FindPath({maxHeightDiff}, ({location.Row}, {location.Column}) - {location.Value}, {visitedLocations.Distinct().Count()}, {endlocation.Value}, {steps})"))
        //        return;

        //    _cache.Add($"FindPath({maxHeightDiff}, ({location.Row}, {location.Column}) - {location.Value}, {visitedLocations.Distinct().Count()}, {endlocation.Value}, {steps})");

        //    //if (location.Value > 105)
        //    Console.WriteLine($"FindPath({maxHeightDiff}, ({location.Row}, {location.Column}) - {location.Value}, {visitedLocations.Distinct().Count()}, {endlocation.Value}, {steps})");

        //    var newVisitedLocations = new List<(int, int)>(visitedLocations);
        //    newVisitedLocations.Add((location.Row, location.Column));

        //    if (location.DistanceFromSource > 0 && location.DistanceFromSource < steps)
        //        return;

        //    location.DistanceFromSource = steps;

        //    if (_paths.Count() > 0)
        //        if (steps >= _paths.Min())
        //            return;

        //    if (location == endlocation)
        //    {
        //        _paths.Add(steps);
        //        Console.WriteLine(steps);
        //        return;
        //    }

        //    List<Location> nextLocations = new();

        //    if (location.Column < _locations.Max(l => l.Column))
        //        nextLocations.Add(_locations.First(l => l.Row == location.Row && l.Column == location.Column + 1));
        //    if (location.Row < _locations.Max(l => l.Row))
        //        nextLocations.Add(_locations.First(l => l.Row == location.Row + 1 && l.Column == location.Column));
        //    if (location.Row > 0)
        //        nextLocations.Add(_locations.First(l => l.Row == location.Row - 1 && l.Column == location.Column));
        //    if (location.Column > 0)
        //        nextLocations.Add(_locations.First(l => l.Row == location.Row && l.Column == location.Column - 1));

        //    foreach (Location l in nextLocations.Where(n => newVisitedLocations.Where(v => v.Item1 == n.Row && v.Item2 == n.Column).Count() == 0 && n.Value - location.Value <= maxHeightDiff))
        //        FindPath(maxHeightDiff, l, newVisitedLocations, endlocation, steps + 1);
        //}
    }

    public class Location
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public char Value { get; set; }
        public long DistanceFromSource { get; set; }
        public bool IsVisited { get; set; }
        public List<Location> ConnectedLocations { get; set; }

        public Location(int row, int col, char val, long dist, bool isVisited)
        {
            Row = row;
            Column = col;
            Value = val;
            DistanceFromSource = dist;
            IsVisited = isVisited;
            ConnectedLocations = new();
        }
    }
}