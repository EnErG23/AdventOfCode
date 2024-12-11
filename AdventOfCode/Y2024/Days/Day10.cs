using AdventOfCode.Models;

namespace AdventOfCode.Y2024.Days
{
    public class Day10 : Day
    {
        private List<Location> _map;

        public Day10(int year, int day, bool test) : base(year, day, test)
        {
            _map = new();

            for (int r = 0; r < Inputs.Count(); r++)
                for (int c = 0; c < Inputs[r].Count(); c++)
                    _map.Add(new Location(r, c, Inputs[r][c]));
        }

        public override string RunPart1() => _map.Where(m => m.Value == '0').Sum(m => GetTrailHeadScore(m, new())).ToString();

        public override string RunPart2() => _map.Where(m => m.Value == '0').Sum(m => GetUniqueTrails(m)).ToString();

        public int GetTrailHeadScore(Location position, List<Location> tops)
        {
            int score = 0;

            List<(int, int)> directions = new() { (-1, 0), (0, 1), (1, 0), (0, -1) };

            if (position.Value == '9' && !tops.Exists(v => v.Row == position.Row && v.Column == position.Column))
            {
                tops.Add(position);
                return 1;
            }

            foreach (var direction in directions)
            {
                if (_map.Exists(m => m.Row == position.Row + direction.Item1 && m.Column == position.Column + direction.Item2))
                {
                    var nextPosition = _map.First(m => m.Row == position.Row + direction.Item1 && m.Column == position.Column + direction.Item2);

                    if (int.Parse(nextPosition.Value.ToString()) == int.Parse(position.Value.ToString()) + 1)
                        score += GetTrailHeadScore(nextPosition, tops);
                }
            }

            return score;
        }

        public int GetUniqueTrails(Location position)
        {
            int trails = 0;

            List<(int, int)> directions = new() { (-1, 0), (0, 1), (1, 0), (0, -1) };

            if (position.Value == '9')
                return 1;

            foreach (var direction in directions)
            {
                if (_map.Exists(m => m.Row == position.Row + direction.Item1 && m.Column == position.Column + direction.Item2))
                {
                    var nextPosition = _map.First(m => m.Row == position.Row + direction.Item1 && m.Column == position.Column + direction.Item2);

                    if (int.Parse(nextPosition.Value.ToString()) == int.Parse(position.Value.ToString()) + 1)
                        trails += GetUniqueTrails(nextPosition);
                }
            }

            return trails;
        }
    }
}