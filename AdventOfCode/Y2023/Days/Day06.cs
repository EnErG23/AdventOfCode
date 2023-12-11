using AdventOfCode.Models;
using System.Numerics;

namespace AdventOfCode.Y2023.Days
{
    public class Day06 : Day
    {
        private List<(BigInteger, BigInteger)> _races;

        public Day06(int year, int day, bool test) : base(year, day, test)
        {
            _races = new();

            var timesS = Inputs[0].Replace("Time: ", "");

            while (timesS.Contains("  "))
                timesS = timesS.Replace("  ", " ");

            var times = timesS.Trim().Split(" ").Select(i => BigInteger.Parse(i)).ToList();

            var distanceS = Inputs[1].Replace("Distance: ", "");

            while (distanceS.Contains("  "))
                distanceS = distanceS.Replace("  ", " ");

            var distances = distanceS.Trim().Split(" ").Select(i => BigInteger.Parse(i)).ToList();

            for (int t = 0; t < times.Count; t++)
                _races.Add((times[t], distances[t]));
        }

        public override string RunPart1() => _races.Select(r => GetWins(r)).Aggregate(1, (BigInteger x, BigInteger y) => x * y).ToString();

        public override string RunPart2() => GetWins((BigInteger.Parse(string.Join("", _races.Select(r => r.Item1))), BigInteger.Parse(string.Join("", _races.Select(r => r.Item2))))).ToString();

        public BigInteger GetWins((BigInteger, BigInteger) race)
        {
            BigInteger wins = 0;

            for (BigInteger i = 0; i <= race.Item1 / 2; i++)
                if ((race.Item1 - i) * i > race.Item2)
                {
                    wins = i - 1;
                    break;
                }

            return 2 * (race.Item1 / 2 - wins) - (race.Item1 % 2 == 0 ? 1 : 0);
        }
    }
}