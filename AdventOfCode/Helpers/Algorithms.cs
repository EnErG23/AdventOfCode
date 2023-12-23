using AdventOfCode.Models;

namespace AdventOfCode.Helpers
{
    public static class Algorithms
    {
        //  PATHFINDING

        //  Breadth-First Search
        public static HashSet<T> BFS<T>(Graph<T> graph, T start)
        {
            var visited = new HashSet<T>();

            if (!graph.AdjacencyList.ContainsKey(start))
                return visited;

            var queue = new Queue<T>();
            queue.Enqueue(start);

            while (queue.Any())
            {
                var vertex = queue.Dequeue();

                if (visited.Contains(vertex))
                    continue;

                visited.Add(vertex);

                foreach (var neighbor in graph.AdjacencyList[vertex])
                    if (!visited.Contains(neighbor))
                        queue.Enqueue(neighbor);
            }

            return visited;
        }

        public static HashSet<T> BFS<T>(Graph<T> graph, T start, long distance)
        {
            var visited = new HashSet<T>();
            var visitedEven = new HashSet<T>() { start };

            if (!graph.AdjacencyList.ContainsKey(start))
                return visited;

            var queue = new Queue<(T, long)>();
            queue.Enqueue((start, 0));

            while (queue.Any())
            {
                var item = queue.Dequeue();
                var vertex = item.Item1;
                var d = item.Item2;

                if (d > distance)
                    break;

                if (visited.Contains(vertex))
                    continue;

                visited.Add(vertex);

                if (d % 2 == 0)
                    visitedEven.Add(vertex);

                Console.WriteLine($"{vertex} - {d}");

                foreach (var neighbor in graph.AdjacencyList[vertex])
                    if (!visited.Contains(neighbor))
                        queue.Enqueue((neighbor, d + 1));
            }

            return visitedEven;
        }

        public static Func<T, IEnumerable<T>> ShortestPathFunction<T>(Graph<T> graph, T start)
        {
            var previous = new Dictionary<T, T>();

            var queue = new Queue<T>();
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                var vertex = queue.Dequeue();
                foreach (var neighbor in graph.AdjacencyList[vertex])
                {
                    if (previous.ContainsKey(neighbor))
                        continue;

                    previous[neighbor] = vertex;
                    queue.Enqueue(neighbor);
                }
            }

            Func<T, IEnumerable<T>> shortestPath = v =>
            {
                var path = new List<T> { };

                var current = v;
                while (!current.Equals(start))
                {
                    path.Add(current);
                    current = previous[current];
                };

                path.Add(start);
                path.Reverse();

                return path;
            };

            return shortestPath;
        }

        // Greatest Common Divider
        public static long GCD(long n1, long n2) => n2 == 0 ? n1 : GCD(n2, n1 % n2);

        // Least Common Multiple
        public static long LCM(List<long> numbers) => numbers.Aggregate((S, val) => S * val / GCD(S, val));

        // Chinese Remainder Theorem
        public static long CRT(List<long> n, List<long> a)
        {
            long prod = n.Aggregate(1, (long i, long j) => i * j);
            long p;
            long sm = 0;

            for (int i = 0; i < n.Count; i++)
            {
                p = prod / n[i];
                sm += a[i] * MMI(p, n[i]) * p;
            }
            return sm % prod;
        }

        // Modular Multiplicative Inverse
        public static long MMI(long a, long mod)
        {
            long b = a % mod;

            for (int x = 1; x < mod; x++)
                if ((b * x) % mod == 1)
                    return x;

            return 1;
        }

        // Pick's Theorem (Count nodes inside polygon)
        public static long PicksTheorem(List<Node> nodes) => IrregularPolygonArea(nodes) - (IrregularPolygonCircumference(nodes) / 2) + 1;

        // Polygon Area
        public static long IrregularPolygonArea(List<Node> nodes)
        {
            long sum1 = 0;
            long sum2 = 0;

            for (int i = 0; i < nodes.Count(); i++)
            {
                var node1 = nodes[i];
                var node2 = i == nodes.Count() - 1 ? nodes[0] : nodes[i + 1];

                sum1 += node1.Coords.Item2 * node2.Coords.Item1;
                sum2 += node1.Coords.Item1 * node2.Coords.Item2;
            }

            return Math.Abs((sum1 - sum2) / 2);
        }

        // Polygon Circumference
        public static long IrregularPolygonCircumference(List<Node> nodes)
        {
            long circumference = 0;

            for (int i = 0; i < nodes.Count(); i++)
            {
                var node1 = nodes[i];
                var node2 = i == nodes.Count() - 1 ? nodes[0] : nodes[i + 1];

                if (node1.Coords.Item1 != node2.Coords.Item1)
                    circumference += Math.Abs(node2.Coords.Item1 - node1.Coords.Item1);
                else
                    circumference += Math.Abs(node2.Coords.Item2 - node1.Coords.Item2);
            }

            return circumference;
        }
    }

    public class Node
    {
        public (long, long) Coords { get; set; }

        public Node((long, long) coords) => Coords = coords;
    }
}
