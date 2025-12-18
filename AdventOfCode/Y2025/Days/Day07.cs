using AdventOfCode.Models;

namespace AdventOfCode.Y2025.Days
{
    public class Day07 : Day
    {
        private readonly Location _start;
        private readonly List<Location> _splitters;
        private HashSet<Location> splittersHit;
        private long timelines;

        public Day07(int year, int day, bool test) : base(year, day, test)
        {
            _start = new(0, Inputs[0].IndexOf('S'), 'S');

            _splitters = new();

            for (var r = 1; r < Inputs.Count; r++)
                for (var c = 0; c < Inputs[r].Length; c++)
                    if (Inputs[r][c] == '^')
                        _splitters.Add(new(r, c, '^'));

            splittersHit = new();
            timelines = 0;
        }

        public override string RunPart1()
        {
            Beam(_start.Row, _start.Column);

            return splittersHit.Count.ToString();
        }

        public override string RunPart2()
        {
            Beam2(_start.Row, _start.Column);

            return timelines.ToString();
        }

        public override void VisualizePart1()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Black;

            for (int i = 0; i < Inputs.Count; i++)
            {
                for (int j = 0; j < Inputs[i].Length; j++)
                {
                    if (splittersHit.Any(s => s.Row == i && s.Column == j))
                        Console.ForegroundColor = ConsoleColor.Red;
                    else if (Inputs[i][j] == '^')
                        Console.ForegroundColor = ConsoleColor.White;
                    else
                        Console.ForegroundColor = ConsoleColor.Black;

                    Console.Write(Inputs[i][j].ToString());
                }
                Console.WriteLine();
            }
        }

        private void Beam(int row, int col)
        {
            if (_splitters.Any(s => s.Row == row + 1 && s.Column == col))
            {
                if (!splittersHit.Any(s => s.Row == row + 1 && s.Column == col))
                {
                    splittersHit.Add(_splitters.First(s => s.Row == row + 1 && s.Column == col));

                    if (col > 0)
                        Beam(row + 1, col - 1);
                    if (col < Inputs[0].Length - 1)
                        Beam(row + 1, col + 1);
                }
            }
            else if (row + 1 < Inputs.Count)
                Beam(row + 1, col);
        }
        private void Beam2(int row, int col)
        {
            if (_splitters.Any(s => s.Row == row + 1 && s.Column == col))
            {
                if (col > 0)
                    Beam2(row + 1, col - 1);
                if (col < Inputs[0].Length - 1)
                    Beam2(row + 1, col + 1);
            }
            else if (row + 1 < Inputs.Count)
                Beam2(row + 1, col);
            else
                timelines++;
        }
    }
}