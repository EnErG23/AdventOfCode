using AdventOfCode.Models;

namespace AdventOfCode.Y2025.Days
{
    public class Day08 : Day
    {
        private List<List<Location3D>> circuits;
        private List<Connection> openConnections;

        public Day08(int year, int day, bool test) : base(year, day, test)
        {
            circuits = new();
            openConnections = new();

            foreach (var input in Inputs)
            {
                var coords = input.Split(",").Select(long.Parse).ToArray();
                circuits.Add([new(coords[0], coords[1], coords[2])]);
            }

            List<List<Location3D>> circuitsChecked = new();

            foreach (var circuit in circuits)
            {
                circuitsChecked.Add(circuit);

                foreach (var box1 in circuit)
                    foreach (var circuit2 in circuits.Where(c => !circuitsChecked.Contains(c)))
                        foreach (var box2 in circuit2)
                            if (!openConnections.Any(c => (c.Location1 == box1 && c.Location2 == box2) || (c.Location1 == box2 && c.Location2 == box1)))
                                openConnections.Add(new(box1, box2));                
            }

            openConnections = openConnections.OrderBy(c => c.Distance).ToList();
        }

        public override string RunPart1()
        {
            for (int i = 0; i < (Test ? 10 : 1000); i++)
            {
                var connection = openConnections.FirstOrDefault();
                var (box1, box2) = (connection.Location1, connection.Location2);

                var circuit1 = circuits.First(c => c.Contains(box1));
                var circuit2 = circuits.First(c => c.Contains(box2));

                openConnections.Remove(connection);

                if (circuit1 == circuit2)
                    continue;

                circuit1.AddRange(circuit2);
                circuits.Remove(circuit2);
            }

            long result = 1;
            foreach (var circuit in circuits.OrderByDescending(c => c.Count).Take(3))
                result *= circuit.Count;

            return result.ToString();
        }

        public override string RunPart2()
        {
            var lastConnection = openConnections.FirstOrDefault();

            while (openConnections.Count > 0)
            {
                var connection = openConnections.FirstOrDefault();

                var (box1, box2) = (connection.Location1, connection.Location2);

                var circuit1 = circuits.First(c => c.Contains(box1));
                var circuit2 = circuits.First(c => c.Contains(box2));

                openConnections.Remove(connection);

                if (circuit1 == circuit2)
                    continue;

                lastConnection = connection;

                circuit1.AddRange(circuit2);
                circuits.Remove(circuit2);
            }

            return (lastConnection.Location1.X * lastConnection.Location2.X).ToString();
        }
    }

    public class Connection
    {
        public Location3D Location1 { get; set; }
        public Location3D Location2 { get; set; }
        public double Distance
            => Math.Sqrt(Math.Pow(Location2.X - Location1.X, 2) + Math.Pow(Location2.Y - Location1.Y, 2) + Math.Pow(Location2.Z - Location1.Z, 2));

        public Connection(Location3D loc1, Location3D loc2)
        {
            Location1 = loc1;
            Location2 = loc2;
        }
    }
}