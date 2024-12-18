using AdventOfCode.Models;
using AdventOfCode.Y2022.Days;

namespace AdventOfCode.Y2024.Days
{
    public class Day14 : Day
    {
        private List<Robot> _robots;
        private decimal _width = 101;
        private decimal _height = 103;

        public Day14(int year, int day, bool test) : base(year, day, test)
        {
            _robots = new();

            foreach (var input in Inputs)
            {
                var line = input.Replace("p=", "").Replace("v=", "");
                var position = line.Split(" ")[0].Split(",").Select(i => decimal.Parse(i)).ToList();
                var velocity = line.Split(" ")[1].Split(",").Select(i => decimal.Parse(i)).ToList();

                _robots.Add(new Robot((position[0], position[1]), (velocity[0], velocity[1])));
            }
        }

        public override string RunPart1()
        {
            decimal seconds = 100;

            decimal Q1 = 0;
            decimal Q2 = 0;
            decimal Q3 = 0;
            decimal Q4 = 0;

            if (Test)
            {
                _width = 11;
                _height = 7;
            }

            foreach (var robot in _robots)
            {
                var position = robot.GetPositionAfterSeconds(_width, _height, seconds);

                if (position.Item1 < ((_width - 1) / 2) && position.Item2 < ((_height - 1) / 2))
                    Q1++;
                else if (position.Item1 > ((_width - 1) / 2) && position.Item2 < ((_height - 1) / 2))
                    Q2++;
                else if (position.Item1 < ((_width - 1) / 2) && position.Item2 > ((_height - 1) / 2))
                    Q3++;
                else if (position.Item1 > ((_width - 1) / 2) && position.Item2 > ((_height - 1) / 2))
                    Q4++;
            }

            return (Q1 * Q2 * Q3 * Q4).ToString();
        }

        public override string RunPart2()
        {
            decimal seconds = 1;

            if (Test)
            {
                _width = 11;
                _height = 7;
            }

            while (true)
            {
                List<(decimal, decimal)> positions = new();

                foreach (var robot in _robots)
                    positions.Add(robot.GetPositionAfterSeconds(_width, _height, seconds));

                if (positions.Count() == positions.Distinct().Count())
                {
                    //Vis(positions);
                    return seconds.ToString();
                }
                seconds++;
            }
        }

        public void Vis(List<(decimal, decimal)> positions)
        {
            Console.Clear();

            for (decimal r = 0; r < _height; r++)
            {
                for (decimal c = 0; c < _width; c++)
                {
                    if (positions.Exists(p => p.Item1 == c && p.Item2 == r))
                        Console.Write("#");
                    else
                        Console.Write(".");
                }
                Console.WriteLine();
            }
        }

        class Robot
        {
            public (decimal, decimal) StartPosition;
            public (decimal, decimal) Velocity;

            public Robot((decimal, decimal) startPosition, (decimal, decimal) velocity)
            {
                StartPosition = startPosition;
                Velocity = velocity;
            }

            public (decimal, decimal) GetPositionAfterSeconds(decimal width, decimal height, decimal seconds)
            {
                var x = StartPosition.Item1 + (seconds * Velocity.Item1);
                var y = StartPosition.Item2 + (seconds * Velocity.Item2);

                if (x < 0)
                    x = width - (Math.Abs(x) % width);
                else if (x > width - 1)
                    x = x % width;

                if (x == width)
                    x = 0;

                if (y < 0)
                    y = height - (Math.Abs(y) % height);
                else if (y > height - 1)
                    y = y % height;

                if (y == height)
                    y = 0;

                return (x, y);
            }
        }
    }
}