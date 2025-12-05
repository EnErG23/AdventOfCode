using AdventOfCode.Models;
using static System.Net.Mime.MediaTypeNames;

namespace AdventOfCode.Y2025.Days
{
    public class Day05 : Day
    {
        private readonly List<(long, long)> _freshRanges;
        private readonly List<long> _ingredients;

        public Day05(int year, int day, bool test) : base(year, day, test)
        {
            _freshRanges = new();

            foreach (var input in Inputs)
            {
                if (string.IsNullOrEmpty(input))
                    break;
                _freshRanges.Add((long.Parse(input.Split('-')[0]), long.Parse(input.Split('-')[1])));
            }

            _ingredients = new();

            foreach (var input in Inputs.Skip(_freshRanges.Count + 1))
                _ingredients.Add(long.Parse(input));
        }

        public override string RunPart1()
        {
            long result = 0;

            foreach (var ingredient in _ingredients)
                if (_freshRanges.Any(f => ingredient >= f.Item1 && ingredient <= f.Item2))
                    result++;

            return result.ToString();
        }

        public override string RunPart2()
        {
            long result = 0;

            var freshRanges = _freshRanges.OrderBy(f => f.Item1).ToList();

            var nextRange = freshRanges[0];

            for (int i = 0; i < freshRanges.Count; i++)
            {
                var freshRange = nextRange;

                result += freshRange.Item2 - freshRange.Item1 + 1;

                if (i == freshRanges.Count - 1)
                    break;

                nextRange = freshRanges[i + 1];

                while (true)
                {
                    if (freshRange.Item2 >= nextRange.Item1)
                    {
                        if (freshRange.Item2 >= nextRange.Item2)
                        {
                            nextRange = freshRanges[i++ + 2];
                        }
                        else
                        {
                            nextRange.Item1 = freshRange.Item2 + 1;
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return result.ToString();
        }
    }
}