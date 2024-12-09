using AdventOfCode.Helpers;
using AdventOfCode.Models;

namespace AdventOfCode.Y2023.Days
{
    public class Day25 : Day
    {
        private HashSet<string> _wires;
        private HashSet<(string, string)> _connections;

        public Day25(int year, int day, bool test) : base(year, day, test)
        {
            _connections = new();

            foreach (var input in Inputs)
            {
                var wire = input.Substring(0, 3);

                foreach (var connection in input.Split(':')[1].Trim().Split(' ').ToList())
                    _connections.Add((wire, connection));
            }

            _wires = new();
            _wires.UnionWith(_connections.Select(c => c.Item1).Distinct());
            _wires.UnionWith(_connections.Select(c => c.Item2).Distinct());
        }

        public override string RunPart1()
        {
            //Print();

            for (int i = 0; i < _connections.Count - 2; i++)
                for (int j = 0; j < _connections.Count - 2; j++)
                    for (int k = 0; k < _connections.Count - 2; k++)
                        if (i != k && k != j && i != j)
                        {
                            HashSet<(string, string)> connections = new(_connections);
                            connections.Remove(_connections.ToList()[i]);
                            connections.Remove(_connections.ToList()[j]);
                            connections.Remove(_connections.ToList()[k]);

                            var wireGroups = GetGroupCounts(connections);

                            if (wireGroups.Item1 == true)
                                return $"{wireGroups.Item2[0] * wireGroups.Item2[1]}";
                        }

            return "undefined";
        }

        public override string RunPart2()
        {
            HashSet<(int, int)> connections = new();
            connections.UnionWith(_connections.Select(c => (_wires.ToList().IndexOf(c.Item1), _wires.ToList().IndexOf(c.Item2))).OrderBy(c => c.Item1).ThenBy(c => c.Item2));
            connections.ToList().ForEach(c => Console.WriteLine($"{c.Item1}-{c.Item2}"));
            Console.WriteLine();

            Graph<int> graph = new Graph<int>(Enumerable.Range(0, _wires.Count).ToList(), connections.Select(c => Tuple.Create(c.Item1, c.Item2)), true);

            var karg = new Kargers();
            Console.WriteLine(karg.MinCut(graph));

            return "undefined";
        }

        new private void Print() => _connections.ToList().ForEach(c => Console.WriteLine($"{c.Item1} - {c.Item2}"));

        public (bool, List<int>) GetGroupCounts(HashSet<(string, string)> conns)
        {
            List<HashSet<string>> groups = new();

            HashSet<(string, string)> connsLeft = new(conns);

            while (connsLeft.Any())
            {
                HashSet<string> group = new();
                HashSet<string> nextConns = new() { connsLeft.First().Item1 };

                while (nextConns.Any())
                {
                    var nextConn = nextConns.First();
                    group.Add(nextConn);
                    nextConns.Remove(nextConn);

                    foreach (var conn in connsLeft.Where(c => c.Item1 == nextConn))
                    {
                        group.Add(conn.Item2);
                        nextConns.Add(conn.Item2);
                        connsLeft.Remove(conn);
                    }

                    foreach (var conn in connsLeft.Where(c => c.Item2 == nextConn))
                    {
                        group.Add(conn.Item1);
                        nextConns.Add(conn.Item1);
                        connsLeft.Remove(conn);
                    }
                }

                groups.Add(group);

                if (groups.Count == 2 && connsLeft.Any())
                    return (false, groups.Select(g => g.Count()).ToList());
            }

            return (groups.Count == 2, groups.Select(g => g.Count()).ToList());
        }
    }

    public class Wire
    {
        public string Name { get; set; }
        public List<Wire> Connections { get; set; }

        public Wire(string name)
        {
            Name = name;
            Connections = new();
        }
    }
}