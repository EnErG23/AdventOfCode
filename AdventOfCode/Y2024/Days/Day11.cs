using AdventOfCode.Models;

namespace AdventOfCode.Y2024.Days
{
    public class Day11 : Day
    {
        private List<decimal> _stones;
        private Dictionary<(decimal, decimal), decimal> _stoneStones = new();

        public Day11(int year, int day, bool test) : base(year, day, test) => _stones = Inputs[0].Trim().Split(" ").Select(i => decimal.Parse(i)).ToList();

        public override string RunPart1() => _stones.Sum(s => GetStoneCountAfterBlinks(s, 25)).ToString();

        public override string RunPart2() => _stones.Sum(s => GetStoneCountAfterBlinks(s, 75)).ToString();

        private decimal GetStoneCountAfterBlinks(decimal stone, decimal blinks)
        {
            decimal stoneCountAfterBlinks = 0;

            if (_stoneStones.ContainsKey((stone, blinks)))
                return _stoneStones[(stone, blinks)];

            if (blinks == 0)
            {
                stoneCountAfterBlinks = 1;
            }
            else if (stone == 0)
            {
                stoneCountAfterBlinks = GetStoneCountAfterBlinks(1, blinks - 1);
            }
            else if (stone.ToString().Length % 2 == 0)
            {
                stoneCountAfterBlinks += GetStoneCountAfterBlinks(decimal.Parse(stone.ToString().Substring(0, stone.ToString().Length / 2)), blinks - 1);
                stoneCountAfterBlinks += GetStoneCountAfterBlinks(decimal.Parse(stone.ToString().Substring(stone.ToString().Length / 2, stone.ToString().Length / 2)), blinks - 1);
            }
            else
            {
                stoneCountAfterBlinks = GetStoneCountAfterBlinks(stone * 2024, blinks - 1);
            }

            _stoneStones.Add((stone, blinks), stoneCountAfterBlinks);

            return stoneCountAfterBlinks;
        }
    }
}