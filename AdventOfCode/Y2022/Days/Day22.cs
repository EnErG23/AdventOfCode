using AdventOfCode.Models;

namespace AdventOfCode.Y2022.Days
{
    public class Day22 : Day
    {
        private readonly List<List<char>> _maze;
        private readonly string _moves;

        public Day22(int year, int day, bool test) : base(year, day, test)
        {
            _maze = new();

            foreach (var input in Inputs)
            {
                if (input == "")
                    break;

                _maze.Add(input.ToCharArray().ToList());
            }

            foreach (var row in _maze.Where(m => m.Count < _maze.Max(m => m.Count)))
            {
                List<char> charsToAdd = new();

                for (int i = row.Count; i < _maze.Max(m => m.Count); i++)
                    charsToAdd.Add(' ');

                row.AddRange(charsToAdd);
            }

            _moves = Inputs.Last();
        }

        public override string RunPart1()
        {
            List<Location> positions = new();

            (int, int) position = (0, _maze.First().IndexOf('.'));
            int facing = 0; //Facing is 0 for right(>), 1 for down(v), 2 for left(<), and 3 for up(^)

            int lastIndex = 0;

            //PrintMaze(position);

            for (int i = 0; i < _moves.Length; i++)
            {
                if (_moves[i] == 'L' || _moves[i] == 'R' || i == _moves.Length - 1)
                {
                    positions = new() { new Location(position.Item1, position.Item2, 'X') };

                    int distance = i == _moves.Length - 1 ? int.Parse(_moves.Substring(lastIndex)) : int.Parse(_moves.Substring(lastIndex, i - lastIndex));

                    Console.Write($"({position.Item1},{position.Item2}) => Facing: {facing} - Distance: {distance} => ");

                    var row = _maze[position.Item1];

                    switch (facing)
                    {
                        case 0:
                            for (int j = 0; j < distance; j++)
                            {
                                if (position.Item2 + 1 >= row.Count || row[position.Item2 + 1] == ' ')
                                    if (row.IndexOf('#') < row.IndexOf('.'))
                                        break;
                                    else
                                        position.Item2 = row.IndexOf('.');
                                else if (row[position.Item2 + 1] == '#')
                                    break;
                                else
                                    position.Item2++;

                                positions.Add(new Location(position.Item1, position.Item2, '>'));
                            }
                            break;
                        case 1:
                            for (int j = 0; j < distance; j++)
                            {
                                if (position.Item1 + 1 >= _maze.Count || _maze[position.Item1 + 1][position.Item2] == ' ')
                                {
                                    int tempY = 0;

                                    while (true)
                                    {
                                        if (_maze[tempY][position.Item2] == '#')
                                            goto OUTER;
                                        else if (_maze[tempY][position.Item2] == '.')
                                        {
                                            position.Item1 = tempY;
                                            break;
                                        }

                                        tempY++;
                                    }
                                }
                                else if (_maze[position.Item1 + 1][position.Item2] == '#')
                                    break;
                                else
                                    position.Item1++;

                                positions.Add(new Location(position.Item1, position.Item2, 'v'));
                            }
                            break;
                        case 2:
                            for (int j = 0; j < distance; j++)
                            {
                                if (position.Item2 - 1 < 0 || row[position.Item2 - 1] == ' ')
                                    if (row.LastIndexOf('#') > row.LastIndexOf('.'))
                                        break;
                                    else
                                        position.Item2 = row.LastIndexOf('.');
                                else if (row[position.Item2 - 1] == '#')
                                    break;
                                else
                                    position.Item2--;

                                positions.Add(new Location(position.Item1, position.Item2, '<'));
                            }
                            break;
                        case 3:
                            for (int j = 0; j < distance; j++)
                            {
                                if (position.Item1 - 1 < 0 || _maze[position.Item1 - 1][position.Item2] == ' ')
                                {
                                    int tempY = _maze.Count - 1;

                                    while (true)
                                    {
                                        if (_maze[tempY][position.Item2] == '#')
                                            goto OUTER;
                                        else if (_maze[tempY][position.Item2] == '.')
                                        {
                                            position.Item1 = tempY;
                                            break;
                                        }

                                        tempY--;
                                    }
                                }
                                else if (_maze[position.Item1 - 1][position.Item2] == '#')
                                    break;
                                else
                                    position.Item1--;

                                positions.Add(new Location(position.Item1, position.Item2, '^'));
                            }
                            break;
                    }

                OUTER:
                    Console.WriteLine($"({position.Item1},{position.Item2}) THEN Turn: {_moves[i]}");

                    if (i > _moves.Length - 5)
                    {
                        PrintMaze(positions);
                        Console.ReadLine();
                    }

                    if (_moves[i] == 'L')
                        facing = facing - 1 < 0 ? 3 : facing - 1;
                    else if (_moves[i] == 'R')
                        facing = facing + 1 > 3 ? 0 : facing + 1;

                    if (i < _moves.Length - 1)
                        lastIndex = i + 1;

                }
            }

            //sum of 1000 times the row, 4 times the column, and the facing.
            Console.WriteLine($"1000 * {position.Item1 + 1} = {1000 * (position.Item1 + 1)}");
            Console.WriteLine($"4 * {position.Item2 + 1} = {4 * (position.Item2 + 1)}");
            Console.WriteLine(facing);

            return ((1000 * (position.Item1 + 1)) + (4 * (position.Item2 + 1)) + facing).ToString();
        }

        public override string RunPart2()
        {
            return "undefined";
        }

        public void PrintMaze((int, int) position)
        {
            for (int i = 0; i < _maze.Count; i++)
            {
                for (int j = 0; j < _maze[i].Count; j++)
                {
                    if (position.Item1 == i && position.Item2 == j)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("X");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(_maze[i][j]);
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("--------------------------------------------------------------------------------------------------------");
            Console.WriteLine();
        }

        public void PrintMaze(List<Location> positions)
        {
            for (int i = 0; i < _maze.Count; i++)
            {
                for (int j = 0; j < _maze[i].Count; j++)
                {
                    if (positions.Exists(p => p.Row == i && p.Column == j))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(positions.Last(p => p.Row == i && p.Column == j).Value);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(_maze[i][j]);
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("--------------------------------------------------------------------------------------------------------");
            Console.WriteLine();
        }
    }
}