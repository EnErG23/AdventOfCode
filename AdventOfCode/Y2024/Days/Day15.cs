using AdventOfCode.Models;
using AdventOfCode.Y2022.Days;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.Security;

namespace AdventOfCode.Y2024.Days
{
    public class Day15 : Day
    {
        private List<Location> _locations = new();
        private int _instructionsStart = 0;

        public Day15(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
        {
            InitMap(false);

            for (int r = _instructionsStart; r < Inputs.Count(); r++)
            {
                foreach (var move in Inputs[r])
                {
                    Location robot = _locations.First(l => l.Value == '@');

                    switch (move)
                    {
                        case '^':
                            if (!_locations.Exists(w => w.Value == '#' && w.Row == robot.Row - 1 && w.Column == robot.Column))
                            {
                                if (_locations.Exists(w => w.Value == 'O' && w.Row == robot.Row - 1 && w.Column == robot.Column))
                                {
                                    List<int> boxesToMove = new();
                                    int counter = 1;

                                    while (true)
                                    {
                                        if (_locations.Exists(w => w.Value == 'O' && w.Row == robot.Row - counter && w.Column == robot.Column))
                                            boxesToMove.Add(robot.Row - counter);
                                        else if (!_locations.Exists(w => w.Value == '#' && w.Row == robot.Row - counter && w.Column == robot.Column))
                                        {
                                            foreach (var box in _locations.Where(b => b.Value == 'O' && boxesToMove.Contains(b.Row) && b.Column == robot.Column))
                                            {
                                                box.Row--;
                                            }

                                            robot.Row--;

                                            break;
                                        }
                                        else
                                        {
                                            break;
                                        }

                                        counter++;
                                    }
                                }
                                else
                                {
                                    robot.Row--;
                                }
                            }
                            break;
                        case '>':
                            if (!_locations.Exists(w => w.Value == '#' && w.Row == robot.Row && w.Column == robot.Column + 1))
                            {
                                if (_locations.Exists(w => w.Value == 'O' && w.Row == robot.Row && w.Column == robot.Column + 1))
                                {
                                    List<int> boxesToMove = new();
                                    int counter = 1;

                                    while (true)
                                    {
                                        if (_locations.Exists(w => w.Value == 'O' && w.Row == robot.Row && w.Column == robot.Column + counter))
                                            boxesToMove.Add(robot.Column + counter);
                                        else if (!_locations.Exists(w => w.Value == '#' && w.Row == robot.Row && w.Column == robot.Column + counter))
                                        {
                                            foreach (var box in _locations.Where(b => b.Value == 'O' && b.Row == robot.Row && boxesToMove.Contains(b.Column)))
                                            {
                                                box.Column++;
                                            }

                                            robot.Column++;

                                            break;
                                        }
                                        else
                                        {
                                            break;
                                        }

                                        counter++;
                                    }
                                }
                                else
                                {
                                    robot.Column++;
                                }
                            }
                            break;
                        case 'v':
                            if (!_locations.Exists(w => w.Value == '#' && w.Row == robot.Row + 1 && w.Column == robot.Column))
                            {
                                if (_locations.Exists(w => w.Value == 'O' && w.Row == robot.Row + 1 && w.Column == robot.Column))
                                {
                                    List<int> boxesToMove = new();
                                    int counter = 1;

                                    while (true)
                                    {
                                        if (_locations.Exists(w => w.Value == 'O' && w.Row == robot.Row + counter && w.Column == robot.Column))
                                            boxesToMove.Add(robot.Row + counter);
                                        else if (!_locations.Exists(w => w.Value == '#' && w.Row == robot.Row + counter && w.Column == robot.Column))
                                        {
                                            foreach (var box in _locations.Where(b => b.Value == 'O' && boxesToMove.Contains(b.Row) && b.Column == robot.Column))
                                            {
                                                box.Row++;
                                            }

                                            robot.Row++;

                                            break;
                                        }
                                        else
                                        {
                                            break;
                                        }

                                        counter++;
                                    }
                                }
                                else
                                {
                                    robot.Row++;
                                }
                            }
                            break;
                        case '<':
                            if (!_locations.Exists(w => w.Value == '#' && w.Row == robot.Row && w.Column == robot.Column - 1))
                            {
                                if (_locations.Exists(w => w.Value == 'O' && w.Row == robot.Row && w.Column == robot.Column - 1))
                                {
                                    List<int> boxesToMove = new();
                                    int counter = 1;

                                    while (true)
                                    {
                                        if (_locations.Exists(w => w.Value == 'O' && w.Row == robot.Row && w.Column == robot.Column - counter))
                                            boxesToMove.Add(robot.Column - counter);
                                        else if (!_locations.Exists(w => w.Value == '#' && w.Row == robot.Row && w.Column == robot.Column - counter))
                                        {
                                            foreach (var box in _locations.Where(b => b.Value == 'O' && b.Row == robot.Row && boxesToMove.Contains(b.Column)))
                                            {
                                                box.Column--;
                                            }

                                            robot.Column--;

                                            break;
                                        }
                                        else
                                        {
                                            break;
                                        }

                                        counter++;
                                    }
                                }
                                else
                                {
                                    robot.Column--;
                                }
                            }
                            break;
                    }
                }
            }

            return _locations.Where(w => w.Value == 'O').Sum(b => (b.Row * 100) + b.Column).ToString();
        }

        public override string RunPart2()
        {
            InitMap(true);

            int instruction = 1;

            for (int r = _instructionsStart; r < Inputs.Count(); r++)
            {
                foreach (var move in Inputs[r])
                {
                    Console.WriteLine($"{instruction++}: {move}");

                    Location robot = _locations.First(l => l.Value == '@');

                    switch (move)
                    {
                        case '^':
                            if (!_locations.Exists(w => w.Value == '#' && w.Row == robot.Row - 1 && w.Column == robot.Column))
                            {
                                if (_locations.Exists(w => (w.Value == '[' || w.Value == ']') && w.Row == robot.Row - 1 && w.Column == robot.Column))
                                {
                                    List<(int, int)> locationsToCheck = new() { (robot.Row, robot.Column) };
                                    List<(int, int)> boxesToMove = new();

                                    while (locationsToCheck.Any())
                                    {
                                        var locationToCheck = locationsToCheck.First();
                                        locationsToCheck.Remove(locationToCheck);

                                        if (_locations.Exists(w => w.Value == '[' && w.Row == locationToCheck.Item1 - 1 && w.Column == locationToCheck.Item2))
                                        {
                                            boxesToMove.Add((locationToCheck.Item1 - 1, locationToCheck.Item2));
                                            locationsToCheck.Add((locationToCheck.Item1 - 1, locationToCheck.Item2));
                                            locationsToCheck.Add((locationToCheck.Item1 - 1, locationToCheck.Item2 + 1));
                                        }
                                        else if (_locations.Exists(w => w.Value == ']' && w.Row == locationToCheck.Item1 - 1 && w.Column == locationToCheck.Item2))
                                        {
                                            boxesToMove.Add((locationToCheck.Item1 - 1, locationToCheck.Item2 - 1));
                                            locationsToCheck.Add((locationToCheck.Item1 - 1, locationToCheck.Item2));
                                            locationsToCheck.Add((locationToCheck.Item1 - 1, locationToCheck.Item2 - 1));
                                        }
                                        else if (_locations.Exists(w => w.Value == '#' && w.Row == locationToCheck.Item1 - 1 && w.Column == locationToCheck.Item2))
                                        {
                                            locationsToCheck.Clear();
                                            boxesToMove.Clear();
                                            break;
                                        }
                                    }

                                    if (boxesToMove.Any())
                                    {
                                        List<Location> locationsToMove = new();

                                        foreach (var boxToMove in boxesToMove.Distinct())
                                        {
                                            locationsToMove.Add(_locations.First(l => l.Row == boxToMove.Item1 && l.Column == boxToMove.Item2));
                                            locationsToMove.Add(_locations.First(l => l.Row == boxToMove.Item1 && l.Column == boxToMove.Item2 + 1));
                                        }

                                        foreach (var locationToMove in locationsToMove)
                                        {
                                            locationToMove.Row--;
                                        }

                                        robot.Row--;
                                    }
                                }
                                else
                                {
                                    robot.Row--;
                                }
                            }
                            break;
                        case '>':
                            if (!_locations.Exists(w => w.Value == '#' && w.Row == robot.Row && w.Column == robot.Column + 1))
                            {
                                if (_locations.Exists(w => w.Value == '[' && w.Row == robot.Row && w.Column == robot.Column + 1))
                                {
                                    List<int> boxesToMove = new();
                                    int counter = 1;

                                    while (true)
                                    {
                                        if (_locations.Exists(w => w.Value == '[' && w.Row == robot.Row && w.Column == robot.Column + counter))
                                            boxesToMove.Add(robot.Column + counter);
                                        else if (!_locations.Exists(w => w.Value == '#' && w.Row == robot.Row && w.Column == robot.Column + counter))
                                        {
                                            foreach (var box in _locations.Where(b => (b.Value == '[' && b.Row == robot.Row && boxesToMove.Contains(b.Column)) || (b.Value == ']' && b.Row == robot.Row && boxesToMove.Contains(b.Column - 1))))
                                            {
                                                //Console.WriteLine("Moved box right");
                                                box.Column++;
                                            }

                                            robot.Column++;

                                            break;
                                        }
                                        else
                                        {
                                            break;
                                        }

                                        counter += 2;
                                    }
                                }
                                else
                                {
                                    robot.Column++;
                                }
                            }
                            break;
                        case 'v':
                            if (!_locations.Exists(w => w.Value == '#' && w.Row == robot.Row + 1 && w.Column == robot.Column))
                            {
                                if (_locations.Exists(w => (w.Value == '[' || w.Value == ']') && w.Row == robot.Row + 1 && w.Column == robot.Column))
                                {
                                    List<(int, int)> locationsToCheck = new() { (robot.Row, robot.Column) };
                                    List<(int, int)> boxesToMove = new();

                                    while (locationsToCheck.Any())
                                    {
                                        var locationToCheck = locationsToCheck.First();
                                        locationsToCheck.Remove(locationToCheck);

                                        if (_locations.Exists(w => w.Value == '[' && w.Row == locationToCheck.Item1 + 1 && w.Column == locationToCheck.Item2))
                                        {
                                            boxesToMove.Add((locationToCheck.Item1 + 1, locationToCheck.Item2));
                                            locationsToCheck.Add((locationToCheck.Item1 + 1, locationToCheck.Item2));
                                            locationsToCheck.Add((locationToCheck.Item1 + 1, locationToCheck.Item2 + 1));
                                        }
                                        else if (_locations.Exists(w => w.Value == ']' && w.Row == locationToCheck.Item1 + 1 && w.Column == locationToCheck.Item2))
                                        {
                                            boxesToMove.Add((locationToCheck.Item1 + 1, locationToCheck.Item2 - 1));
                                            locationsToCheck.Add((locationToCheck.Item1 + 1, locationToCheck.Item2));
                                            locationsToCheck.Add((locationToCheck.Item1 + 1, locationToCheck.Item2 - 1));
                                        }
                                        else if (_locations.Exists(w => w.Value == '#' && w.Row == locationToCheck.Item1 + 1 && w.Column == locationToCheck.Item2))
                                        {
                                            locationsToCheck.Clear();
                                            boxesToMove.Clear();
                                            break;
                                        }
                                    }

                                    if (boxesToMove.Any())
                                    {
                                        List<Location> locationsToMove = new();

                                        foreach (var boxToMove in boxesToMove.Distinct())
                                        {
                                            locationsToMove.Add(_locations.First(l => l.Row == boxToMove.Item1 && l.Column == boxToMove.Item2));
                                            locationsToMove.Add(_locations.First(l => l.Row == boxToMove.Item1 && l.Column == boxToMove.Item2 + 1));
                                        }

                                        foreach (var locationToMove in locationsToMove)
                                        {
                                            locationToMove.Row++;
                                        }

                                        robot.Row++;
                                    }
                                }
                                else
                                {
                                    robot.Row++;
                                }
                            }
                            break;
                        case '<':
                            if (!_locations.Exists(w => w.Value == '#' && w.Row == robot.Row && w.Column == robot.Column - 1))
                            {
                                if (_locations.Exists(w => w.Value == ']' && w.Row == robot.Row && w.Column == robot.Column - 1))
                                {
                                    List<int> boxesToMove = new();
                                    int counter = 1;

                                    while (true)
                                    {
                                        if (_locations.Exists(w => w.Value == ']' && w.Row == robot.Row && w.Column == robot.Column - counter))
                                            boxesToMove.Add(robot.Column - counter);
                                        else if (!_locations.Exists(w => w.Value == '#' && w.Row == robot.Row && w.Column == robot.Column - counter))
                                        {
                                            foreach (var box in _locations.Where(b => (b.Value == ']' && b.Row == robot.Row && boxesToMove.Contains(b.Column)) || (b.Value == '[' && b.Row == robot.Row && boxesToMove.Contains(b.Column + 1))))
                                            {
                                                //Console.WriteLine("Moved box left");
                                                box.Column--;
                                            }

                                            robot.Column--;

                                            break;
                                        }
                                        else
                                        {
                                            break;
                                        }

                                        counter += 2;
                                    }
                                }
                                else
                                {
                                    robot.Column--;
                                }
                            }
                            break;
                    }
                }
            }

            return _locations.Where(w => w.Value == '[').Sum(b => (b.Row * 100) + b.Column).ToString();
        }

        private void InitMap(bool isWide)
        {
            _locations = new();

            for (int r = 0; r < Inputs.Count(); r++)
            {
                if (Inputs[r] == "")
                {
                    _instructionsStart = r + 1;
                    break;
                }

                if (isWide)
                {
                    for (int c = 0; c < Inputs[r].Count() * 2; c += 2)
                    {
                        switch (Inputs[r][c / 2])
                        {
                            case '#':
                                _locations.Add(new Location(r, c, '#'));
                                _locations.Add(new Location(r, c + 1, '#'));
                                break;
                            case 'O':
                                _locations.Add(new Location(r, c, '['));
                                _locations.Add(new Location(r, c + 1, ']'));
                                break;
                            case '@':
                                _locations.Add(new Location(r, c, '@'));
                                break;
                        }
                    }
                }
                else
                {
                    for (int c = 0; c < Inputs[r].Count(); c++)
                    {
                        switch (Inputs[r][c])
                        {
                            case '#':
                                _locations.Add(new Location(r, c, '#'));
                                break;
                            case 'O':
                                _locations.Add(new Location(r, c, 'O'));
                                break;
                            case '@':
                                _locations.Add(new Location(r, c, '@'));
                                break;
                        }
                    }
                }
            }
        }

        public override void VisualizePart1()
        {
            for (int r = 0; r <= _locations.Max(w => w.Row); r++)
            {
                for (int c = 0; c <= _locations.Max(w => w.Column); c++)
                    try
                    {
                        Console.Write(_locations.First(l => l.Row == r && l.Column == c).Value);
                    }
                    catch
                    {
                        Console.Write(".");
                    }

                Console.WriteLine();
            }
        }

        public override void VisualizePart2()
        {
            VisualizePart1();
        }
    }
}