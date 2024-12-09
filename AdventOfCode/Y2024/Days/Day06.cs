using AdventOfCode.Models;
using AdventOfCode.Y2023.Days;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2024.Days
{
    public class Day06 : Day
    {
        private List<Location> _obstacles = new();
        private Location? _startPosition;

        public Day06(int year, int day, bool test) : base(year, day, test)
        {
            List<char> guardChars = new List<char> { '^', '>', 'v', '<' };

            for (int r = 0; r < Inputs.Count(); r++)
                for (int c = 0; c < Inputs[r].Count(); c++)
                    if (Inputs[r][c] == '#')
                        _obstacles.Add(new Location(r, c, '#'));
                    else if (guardChars.Contains(Inputs[r][c]))
                        _startPosition = new Location(r, c, Inputs[r][c]);
        }

        public override string RunPart1() => FollowGuardPath(_obstacles, _startPosition, false).Item1.Count().ToString();

        public override string RunPart2() => FollowGuardPath(_obstacles, _startPosition, true).Item2.ToString();

        private (List<Location>, int) FollowGuardPath(List<Location> obstacles, Location startingPosition, bool checkOptionalObstacles)
        {
            List<Location> guardPositions = new() { startingPosition };
            List<Location> optionalObstacles = new();

            while (true)
            {
                Location currentPos = guardPositions.Last();
                Location nextPos = new Location(currentPos.Row, currentPos.Column, currentPos.Value);

                switch (currentPos.Value)
                {
                    case '^': //top
                        if (obstacles.Exists(o => o.Row == currentPos.Row - 1 && o.Column == currentPos.Column))
                        {
                            currentPos.Value = '>';
                            continue;
                        }
                        else
                        {
                            if (checkOptionalObstacles && !guardPositions.GetRange(0, guardPositions.Count() - 1).Exists(g => g.Row == currentPos.Row && g.Column == currentPos.Column && g.Value == currentPos.Value))
                            {
                                Location optionalObstacle = new Location(currentPos.Row - 1, currentPos.Column, 'O');

                                if (ExtraObstacleCreatesLoop(obstacles, currentPos, optionalObstacle))
                                    optionalObstacles.Add(optionalObstacle);
                            }
                            nextPos.Row--;
                        }
                        break;
                    case '>': //right
                        if (obstacles.Exists(o => o.Row == currentPos.Row && o.Column == currentPos.Column + 1))
                        {
                            currentPos.Value = 'v';
                            continue;
                        }
                        else
                        {
                            if (checkOptionalObstacles && !guardPositions.GetRange(0, guardPositions.Count() - 1).Exists(g => g.Row == currentPos.Row && g.Column == currentPos.Column && g.Value == currentPos.Value))
                            {
                                Location optionalObstacle = new Location(currentPos.Row, currentPos.Column + 1, 'O');

                                if (ExtraObstacleCreatesLoop(obstacles, currentPos, optionalObstacle))
                                    optionalObstacles.Add(optionalObstacle);
                            }

                            nextPos.Column++;
                        }
                        break;
                    case 'v': //down
                        if (obstacles.Exists(o => o.Row == currentPos.Row + 1 && o.Column == currentPos.Column))
                        {
                            currentPos.Value = '<';
                            continue;
                        }
                        else
                        {
                            if (checkOptionalObstacles && !guardPositions.GetRange(0, guardPositions.Count() - 1).Exists(g => g.Row == currentPos.Row && g.Column == currentPos.Column && g.Value == currentPos.Value))
                            {
                                Location optionalObstacle = new Location(currentPos.Row + 1, currentPos.Column, 'O');

                                if (ExtraObstacleCreatesLoop(obstacles, currentPos, optionalObstacle))
                                    optionalObstacles.Add(optionalObstacle);
                            }

                            nextPos.Row++;
                        }
                        break;
                    case '<': //left
                        if (obstacles.Exists(o => o.Row == currentPos.Row && o.Column == currentPos.Column - 1))
                        {
                            currentPos.Value = '^';
                            continue;
                        }
                        else
                        {
                            if (checkOptionalObstacles && !guardPositions.GetRange(0, guardPositions.Count() - 1).Exists(g => g.Row == currentPos.Row && g.Column == currentPos.Column && g.Value == currentPos.Value))
                            {
                                Location optionalObstacle = new Location(currentPos.Row, currentPos.Column - 1, 'O');

                                if (ExtraObstacleCreatesLoop(obstacles, currentPos, optionalObstacle))
                                    optionalObstacles.Add(optionalObstacle);
                            }

                            nextPos.Column--;
                        }
                        break;
                    default:
                        break;
                }

                if (nextPos.Row < 0 || nextPos.Column > Inputs[0].Count() - 1 || nextPos.Row > Inputs.Count() - 1 || nextPos.Column < 0 || guardPositions.Exists(g => g.Row == nextPos.Row && g.Column == nextPos.Column && g.Value == nextPos.Value))
                    break;
                else
                    guardPositions.Add(nextPos);
            }

            return (guardPositions.Select(g => (g.Row, g.Column)).Distinct().Select(g => new Location(g.Row, g.Column, 'X')).ToList(), optionalObstacles.Count());
        }

        private bool ExtraObstacleCreatesLoop(List<Location> obstaclesOG, Location startingPositionOG, Location extraObstacle)
        {
            List<Location> guardPositions = new() { new Location(startingPositionOG.Row, startingPositionOG.Column, startingPositionOG.Value) };

            List<Location> obstacles = obstaclesOG.ToList();
            obstacles.Add(extraObstacle);

            while (true)
            {
                Location currentPos = guardPositions.Last();
                Location nextPos = new Location(currentPos.Row, currentPos.Column, currentPos.Value);

                switch (currentPos.Value)
                {
                    case '^': //top
                        if (obstacles.Exists(o => o.Row == currentPos.Row - 1 && o.Column == currentPos.Column))
                        {
                            currentPos.Value = '>';
                            continue;
                        }
                        else
                        {
                            nextPos.Row--;
                        }
                        break;
                    case '>': //right
                        if (obstacles.Exists(o => o.Row == currentPos.Row && o.Column == currentPos.Column + 1))
                        {
                            currentPos.Value = 'v';
                            continue;
                        }
                        else
                        {
                            nextPos.Column++;
                        }
                        break;
                    case 'v': //down
                        if (obstacles.Exists(o => o.Row == currentPos.Row + 1 && o.Column == currentPos.Column))
                        {
                            currentPos.Value = '<';
                            continue;
                        }
                        else
                        {
                            nextPos.Row++;
                        }
                        break;
                    case '<': //left
                        if (obstacles.Exists(o => o.Row == currentPos.Row && o.Column == currentPos.Column - 1))
                        {
                            currentPos.Value = '^';
                            continue;
                        }
                        else
                        {
                            nextPos.Column--;
                        }
                        break;
                    default:
                        break;
                }

                if (guardPositions.Exists(g => g.Row == nextPos.Row && g.Column == nextPos.Column && g.Value == nextPos.Value))
                {
                    Console.WriteLine($"({extraObstacle.Row}, {extraObstacle.Column})");
                    return true;
                }
                else if (nextPos.Row < 0 || nextPos.Column > Inputs[0].Count() - 1 || nextPos.Row > Inputs.Count() - 1 || nextPos.Column < 0)
                    return false;
                else
                    guardPositions.Add(nextPos);
            }
        }

        public override void VisualizePart1() => Visualize(FollowGuardPath(_obstacles, _startPosition, false).Item1);

        private void Visualize(List<Location> guardPositions)
        {
            for (int r = 0; r < Inputs.Count(); r++)
            {
                for (int c = 0; c < Inputs[r].Count(); c++)
                    if (guardPositions.Exists(g => g.Row == r && g.Column == c))
                        Console.Write('X');
                    else
                        Console.Write(Inputs[r][c].ToString());

                Console.WriteLine();
            }
        }

        private void Visualize(List<Location> guardPositions, Location newObstacle)
        {
            //Console.Clear();
            Console.WriteLine();

            for (int r = 0; r < Inputs.Count(); r++)
            {
                for (int c = 0; c < Inputs[r].Count(); c++)
                {
                    if (guardPositions.Last().Row == r && guardPositions.Last().Column == c)
                    {
                        Console.Write(guardPositions.Last().Value);
                    }
                    else if (guardPositions.Exists(g => g.Row == r && g.Column == c))
                    {
                        Console.Write('X');
                    }
                    else if (newObstacle.Row == r && newObstacle.Column == c)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write('O');
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                        Console.Write(Inputs[r][c].ToString());
                }
                Console.WriteLine();
            }

            Console.ReadLine();
        }
    }
}