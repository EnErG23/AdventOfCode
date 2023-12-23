using AdventOfCode.Helpers;
using AdventOfCode.Models;

namespace AdventOfCode.Y2023.Days
{
    public class Day21 : Day
    {
        private readonly List<Location> _locations;
        private readonly List<Tuple<Location, Location>> _edges;
        private readonly Graph<Location> _graph;
        private Location _startLocation;

        public Day21(int year, int day, bool test) : base(year, day, test)
        {
            _locations = new();
            _edges = new();

            for (var r = 0; r < Inputs.Count; r++)
                for (var c = 0; c < Inputs[r].Length; c++)
                    _locations.Add(new(r, c, Inputs[r][c]));

            _startLocation = _locations.First(l => l.Value == 'S');

            foreach (var location in _locations.Where(l => l.Value != '#'))
            {
                //TOP
                if (location.Row > 0)
                {
                    var connLoc = _locations.First(l => l.Row == location.Row - 1 && l.Column == location.Column);

                    if (connLoc.Value != '#')
                        _edges.Add(Tuple.Create(location, connLoc));
                }

                // RIGHT
                if (location.Column < _locations.Max(l => l.Column))
                {
                    var connLoc = _locations.First(l => l.Row == location.Row && l.Column == location.Column + 1);

                    if (connLoc.Value != '#')
                        _edges.Add(Tuple.Create(location, connLoc));
                }

                // BOTTOM
                if (location.Row < _locations.Max(l => l.Row))
                {
                    var connLoc = _locations.First(l => l.Row == location.Row + 1 && l.Column == location.Column);

                    if (connLoc.Value != '#')
                        _edges.Add(Tuple.Create(location, connLoc));
                }

                //LEFT
                if (location.Column > 0)
                {
                    var connLoc = _locations.First(l => l.Row == location.Row && l.Column == location.Column - 1);

                    if (connLoc.Value != '#')
                        _edges.Add(Tuple.Create(location, connLoc));
                }
            }

            _graph = new Graph<Location>(_locations, _edges, false);
        }

        public override string RunPart1() => Algorithms.BFS(_graph, _startLocation, Test ? 6 : 64).Count.ToString();

        public override string RunPart2()
        {
            // Modify BFS to use input tile as extra dimension
            return "undefined";
        }
    }
}