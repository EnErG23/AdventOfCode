using AdventOfCode.Models;

namespace AdventOfCode.Y2023.Days
{
    public class Day10 : Day
    {
        private List<Pipe> _pipes;
        private List<Pipe> _nodes;

        public Day10(int year, int day, bool test) : base(year, day, test)
        {
            _pipes = new List<Pipe>();
            _nodes = new List<Pipe>();

            for (int y = 0; y < Inputs.Count; y++)
                for (int x = 0; x < Inputs[0].Length; x++)
                    _pipes.Add(new Pipe((y, x), Inputs[y][x]));

            (int, int) coords = _pipes.First(p => p.Start).Coords;

            while (true)
            {
                var currentPipe = _pipes.First(p => p.Coords == coords);
                currentPipe.Visited = true;

                if ((currentPipe.North || currentPipe.Start) && _pipes.Exists(p => p.Coords == SumCoords(coords, (-1, 0)) && p.South && !p.Visited)) // CHECK NORTH                
                    coords = SumCoords(coords, (-1, 0));
                else if ((currentPipe.East || currentPipe.Start) && _pipes.Exists(p => p.Coords == SumCoords(coords, (0, 1)) && p.West && !p.Visited)) // CHECK EAST
                    coords = SumCoords(coords, (0, 1));
                else if ((currentPipe.South || currentPipe.Start) && _pipes.Exists(p => p.Coords == SumCoords(coords, (1, 0)) && p.North && !p.Visited)) // CHECK SOUTH
                    coords = SumCoords(coords, (1, 0));
                else if ((currentPipe.West || currentPipe.Start) && _pipes.Exists(p => p.Coords == SumCoords(coords, (0, -1)) && p.East && !p.Visited)) // CHECK WEST
                    coords = SumCoords(coords, (0, -1));
                else
                    break;

                if (currentPipe.Char != '-' && currentPipe.Char != '|')
                    _nodes.Add(currentPipe);
            }

            _nodes = _pipes.Where(p => p.Visited).ToList();
        }

        public override string RunPart1() => (_pipes.Count(p => p.Visited) / 2).ToString();

        public override string RunPart2()
        {
            var sum1 = 0;
            var sum2 = 0;

            Console.WriteLine($"({_nodes[0].Coords.Item2},{_nodes[0].Coords.Item1})");

            for (int i = 0; i < _nodes.Count(); i++)
            {
                var node1 = _nodes[i];
                var node2 = i == _nodes.Count() - 1 ? _nodes[0] : _nodes[i + 1];

                sum1 += node1.Coords.Item2 * node2.Coords.Item1;
                sum2 += node1.Coords.Item1 * node2.Coords.Item2;
                Console.WriteLine($"({node2.Coords.Item2},{node2.Coords.Item1}) => {node1.Coords.Item2} * {node2.Coords.Item1} = {node1.Coords.Item2 * node2.Coords.Item1} | {node1.Coords.Item1} * {node2.Coords.Item2} = {node1.Coords.Item1 * node2.Coords.Item2}");
            }
            Console.WriteLine((sum1 - sum2) / 2);

            return (((sum1 - sum2) / 2) - _pipes.Count(p => p.Visited)).ToString();
        }

        public override void VisualizePart2()
        {
            foreach(var pipe in _pipes)
            {
                if (pipe.Coords.Item2 == 0)
                    Console.WriteLine();

                    if (pipe.Visited)
                    Console.ForegroundColor = ConsoleColor.White;
                else
                    Console.ForegroundColor = ConsoleColor.Red;

                Console.Write(pipe.Char);                
            }
        }

        public (int, int) SumCoords((int, int) coord1, (int, int) coord2) => (coord1.Item1 + coord2.Item1, coord1.Item2 + coord2.Item2);
    }

    public class Pipe
    {
        public (int, int) Coords { get; set; }
        public char Char { get; set; }
        public bool Start => Char == 'S';
        public bool North => (Char == '|' || Char == 'J' || Char == 'L');
        public bool East => (Char == '-' || Char == 'F' || Char == 'L');
        public bool South => (Char == '|' || Char == 'F' || Char == '7');
        public bool West => (Char == '-' || Char == '7' || Char == 'J');
        public bool Visited;

        public Pipe((int, int) coords, char c)
        {
            Coords = coords;
            Char = c;
            Visited = false;
        }
    }
}