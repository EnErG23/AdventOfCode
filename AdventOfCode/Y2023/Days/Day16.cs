using AdventOfCode.Models;

namespace AdventOfCode.Y2023.Days
{
    public class Day16 : Day
    {
        private List<Space> _spaces;

        public Day16(int year, int day, bool test) : base(year, day, test)
        {
            _spaces = new();

            for (int r = 0; r < Inputs.Count; r++)
                for (int c = 0; c < Inputs[0].Length; c++)
                    _spaces.Add(new Space((r, c), Inputs[r][c]));
        }

        public override string RunPart1() => Beam((9, 2), 0, new()).Count().ToString(); //Beam((0, 0), 1, new()).Count().ToString();

        public override string RunPart2()
        {
            //Console.WriteLine(_spaces.Count(s => s.Coords.Item1 == 0));
            //Console.WriteLine(_spaces.Count(s => s.Coords.Item2 == 0));

            var maxEnergized = 0;

            foreach (var space in _spaces.Where(s => s.Coords.Item1 == 0)) // Down from top row
            {
                Reset();

                Console.WriteLine($"{space.Coords.Item2} / {_spaces.Count(s => s.Coords.Item1 == 0)}");

                var coords = Beam(space.Coords, 2, new());

                Console.WriteLine($"{coords.Count}: {string.Join(", ", coords.OrderBy(c => c.Item1).ThenBy(c => c.Item2).Select(c => "(" + c.Item1 + ", " + c.Item2 + ")"))}");

                maxEnergized = Math.Max(maxEnergized, coords.Count());
            }
            Console.WriteLine("1 / 4");

            foreach (var space in _spaces.Where(s => s.Coords.Item1 == _spaces.Max(s => s.Coords.Item1)))  // Up from bottom row
            {
                Reset();

                Console.WriteLine($"{space.Coords.Item2} / {_spaces.Count(s => s.Coords.Item1 == _spaces.Max(s => s.Coords.Item1))}");

                var coords = Beam(space.Coords, 0, new());

                Console.WriteLine($"{coords.Count}: {string.Join(", ", coords.OrderBy(c => c.Item1).ThenBy(c => c.Item2).Select(c => "(" + c.Item1 + ", " + c.Item2 + ")"))}");

                maxEnergized = Math.Max(maxEnergized, coords.Count());
            }
            Console.WriteLine("2 / 4");

            foreach (var space in _spaces.Where(s => s.Coords.Item2 == 0)) // Right from left column
            {
                Reset();

                Console.WriteLine($"{space.Coords.Item1} / {_spaces.Count(s => s.Coords.Item2 == 0)}");

                var coords = Beam(space.Coords, 1, new());

                Console.WriteLine($"{coords.Count}: {string.Join(", ", coords.OrderBy(c => c.Item1).ThenBy(c => c.Item2).Select(c => "(" + c.Item1 + ", " + c.Item2 + ")"))}");

                maxEnergized = Math.Max(maxEnergized, coords.Count());
            }
            Console.WriteLine("3 / 4");

            foreach (var space in _spaces.Where(s => s.Coords.Item2 == _spaces.Max(s => s.Coords.Item2))) // Left from right column
            {
                Reset();

                Console.WriteLine($"{space.Coords.Item1} / {_spaces.Count(s => s.Coords.Item2 == _spaces.Max(s => s.Coords.Item2))}");

                var coords = Beam(space.Coords, 3, new());

                Console.WriteLine($"{coords.Count}: {string.Join(", ", coords.OrderBy(c => c.Item1).ThenBy(c => c.Item2).Select(c => "(" + c.Item1 + ", " + c.Item2 + ")"))}");

                maxEnergized = Math.Max(maxEnergized, coords.Count());
            }
            Console.WriteLine("4 / 4");

            return maxEnergized.ToString();
        }

        private List<(int, int)> Beam((int, int) coords, int direction, List<(int, int)> energizedSpaces)
        {
            energizedSpaces = energizedSpaces.Distinct().ToList();

            //if (energizedSpaces.Count > 12100)
            //{
            //Console.Write($"({coords.Item1}, {coords.Item2}) => {direction}: {energizedSpaces.Count()}");
            //Console.ReadLi();
            //}

            if (_spaces.Exists(s => s.Coords == (coords.Item1, coords.Item2)))
            {
                var space = _spaces.First(s => s.Coords == (coords.Item1, coords.Item2));
                energizedSpaces.Add(space.Coords);

                if (space.EnergizedSpacesFromDirections[direction].Count() > 0)
                {
                    space.EnteredFromDirection[direction] = true;
                    energizedSpaces.AddRange(space.EnergizedSpacesFromDirections[direction]);
                }
                else if (!space.EnteredFromDirection[direction])
                {
                    space.EnteredFromDirection[direction] = true;

                    switch (space.Char)
                    {
                        case '/':
                            switch (direction)
                            {
                                case 0:
                                    energizedSpaces.AddRange(Beam((coords.Item1, coords.Item2 + 1), 1, energizedSpaces));
                                    break;
                                case 1:
                                    energizedSpaces.AddRange(Beam((coords.Item1 - 1, coords.Item2), 0, energizedSpaces));
                                    break;
                                case 2:
                                    energizedSpaces.AddRange(Beam((coords.Item1, coords.Item2 - 1), 3, energizedSpaces));
                                    break;
                                case 3:
                                    energizedSpaces.AddRange(Beam((coords.Item1 + 1, coords.Item2), 2, energizedSpaces));
                                    break;
                            }
                            break;
                        case '\\':
                            switch (direction)
                            {
                                case 0:
                                    energizedSpaces.AddRange(Beam((coords.Item1, coords.Item2 - 1), 3, energizedSpaces));
                                    break;
                                case 1:
                                    energizedSpaces.AddRange(Beam((coords.Item1 + 1, coords.Item2), 2, energizedSpaces));
                                    break;
                                case 2:
                                    energizedSpaces.AddRange(Beam((coords.Item1, coords.Item2 + 1), 1, energizedSpaces));
                                    break;
                                case 3:
                                    energizedSpaces.AddRange(Beam((coords.Item1 - 1, coords.Item2), 0, energizedSpaces));
                                    break;
                            }
                            break;
                        case '|':
                            switch (direction)
                            {
                                case 0:
                                    energizedSpaces.AddRange(Beam((coords.Item1 - 1, coords.Item2), 0, energizedSpaces));
                                    break;
                                case 2:
                                    energizedSpaces.AddRange(Beam((coords.Item1 + 1, coords.Item2), 2, energizedSpaces));
                                    break;
                                case 1:
                                case 3:
                                    energizedSpaces.AddRange(Beam((coords.Item1 - 1, coords.Item2), 0, energizedSpaces));
                                    energizedSpaces.AddRange(Beam((coords.Item1 + 1, coords.Item2), 2, energizedSpaces));
                                    break;
                            }
                            break;
                        case '-':
                            switch (direction)
                            {
                                case 1:
                                    energizedSpaces.AddRange(Beam((coords.Item1, coords.Item2 + 1), 1, energizedSpaces));
                                    break;
                                case 3:
                                    energizedSpaces.AddRange(Beam((coords.Item1, coords.Item2 - 1), 3, energizedSpaces));
                                    break;
                                case 0:
                                case 2:
                                    energizedSpaces.AddRange(Beam((coords.Item1, coords.Item2 - 1), 3, energizedSpaces));
                                    energizedSpaces.AddRange(Beam((coords.Item1, coords.Item2 + 1), 1, energizedSpaces));
                                    break;
                            }
                            break;
                        default:
                            switch (direction)
                            {
                                case 0:
                                    energizedSpaces.AddRange(Beam((coords.Item1 - 1, coords.Item2), 0, energizedSpaces));
                                    break;
                                case 1:
                                    energizedSpaces.AddRange(Beam((coords.Item1, coords.Item2 + 1), 1, energizedSpaces));
                                    break;
                                case 2:
                                    energizedSpaces.AddRange(Beam((coords.Item1 + 1, coords.Item2), 2, energizedSpaces));
                                    break;
                                case 3:
                                    energizedSpaces.AddRange(Beam((coords.Item1, coords.Item2 - 1), 3, energizedSpaces));
                                    break;
                            }
                            break;
                    }

                    space.EnergizedSpacesFromDirections[direction] = energizedSpaces.Distinct().ToList();
                }
            }

            return energizedSpaces.Distinct().ToList();
        }

        private void Reset()
        {
            _spaces = new();

            for (int r = 0; r < Inputs.Count; r++)
                for (int c = 0; c < Inputs[0].Length; c++)
                    _spaces.Add(new Space((r, c), Inputs[r][c]));
        }
        //=> _spaces.ForEach(s => s.EnteredFromDirection = new() { false, false, false, false });
    }

    public class Space
    {
        public (int, int) Coords { get; set; }
        public char Char { get; set; }
        public List<bool> EnteredFromDirection { get; set; }
        public List<List<(int, int)>> EnergizedSpacesFromDirections { get; set; }

        public Space((int, int) coords, char c)
        {
            Coords = coords;
            Char = c;
            EnteredFromDirection = new() { false, false, false, false };
            EnergizedSpacesFromDirections = new() { new(), new(), new(), new() };
        }
    }
}