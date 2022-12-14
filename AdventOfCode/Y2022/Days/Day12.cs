using AdventOfCode.Helpers;
using AdventOfCode.Models;

namespace AdventOfCode.Y2022.Days
{
    public class Day12 : Day
    {
        private readonly List<Location> _locations;
        private readonly List<Tuple<Location, Location>> _edges;
        private readonly Graph<Location> _graph;
        private readonly Algorithms _algorithms;
        private Location _startLocation;
        private Location _endLocation;

        public Day12(int year, int day, bool test) : base(year, day, test)
        {
            _locations = new();
            _edges = new();

            for (var r = 0; r < Inputs.Count; r++)
                for (var c = 0; c < Inputs[r].Length; c++)
                    _locations.Add(new(r, c, Inputs[r][c]));

            _startLocation = _locations.First(l => l.Value == 'S');
            _endLocation = _locations.First(l => l.Value == 'E');

            foreach (var location in _locations.Where(l => l.Value != 'E'))
            {
                var locVal = location.Value.Equals('E') ? 'z' : (location.Value.Equals('S') ? 'a' : location.Value);

                // RIGHT
                if (location.Column < _locations.Max(l => l.Column))
                {
                    var connLoc = _locations.First(l => l.Row == location.Row && l.Column == location.Column + 1);
                    var connVal = connLoc.Value.Equals('E') ? 'z' : (connLoc.Value.Equals('S') ? 'a' : connLoc.Value);

                    if (connVal - locVal < 2)
                        _edges.Add(Tuple.Create(location, connLoc));
                }

                // BOTTOM
                if (location.Row < _locations.Max(l => l.Row))
                {
                    var connLoc = _locations.First(l => l.Row == location.Row + 1 && l.Column == location.Column);
                    var connVal = connLoc.Value.Equals('E') ? 'z' : (connLoc.Value.Equals('S') ? 'a' : connLoc.Value);

                    if (connVal - locVal < 2)
                        _edges.Add(Tuple.Create(location, connLoc));
                }

                //TOP
                if (location.Row > 0)
                {
                    var connLoc = _locations.First(l => l.Row == location.Row - 1 && l.Column == location.Column);
                    var connVal = connLoc.Value.Equals('E') ? 'z' : (connLoc.Value.Equals('S') ? 'a' : connLoc.Value);

                    if (connVal - locVal < 2)
                        _edges.Add(Tuple.Create(location, connLoc));
                }

                //LEFT
                if (location.Column > 0)
                {
                    var connLoc = _locations.First(l => l.Row == location.Row && l.Column == location.Column - 1);
                    var connVal = connLoc.Value.Equals('E') ? 'z' : (connLoc.Value.Equals('S') ? 'a' : connLoc.Value);

                    if (connVal - locVal < 2)
                        _edges.Add(Tuple.Create(location, connLoc));
                }
            }

            _graph = new Graph<Location>(_locations, _edges, false);
            _algorithms = new Algorithms();
        }

        public override string RunPart1()
            => (_algorithms.ShortestPathFunction(_graph, _startLocation)(_endLocation).Count() - 1).ToString();

        //TODO: Speed up
        public override string RunPart2()
        {
            int shortestPath = _algorithms.ShortestPathFunction(_graph, _startLocation)(_endLocation).Count();

            foreach (var location in _locations.Where(l => l.Value == 'a'))
            {
                try
                {
                    int pathLength = _algorithms.ShortestPathFunction(_graph, location)(_endLocation).Count();
                    shortestPath = pathLength < shortestPath ? pathLength : shortestPath;
                }
                catch { }
            }

            return (shortestPath - 1).ToString();
        }
    }
}