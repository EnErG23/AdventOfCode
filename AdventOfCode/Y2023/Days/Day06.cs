using AdventOfCode.Models;
using System.Numerics;

namespace AdventOfCode.Y2023.Days
{
    public class Day06 : Day
    {
        private List<int> _times;
        private List<int> _distances;

        public Day06(int year, int day, bool test) : base(year, day, test)
        {
            var timesS = Inputs[0].Replace("Time: ", "");

            while (timesS.Contains("  "))
                timesS = timesS.Replace("  ", " ");

            _times = timesS.Trim().Split(" ").Select(i => int.Parse(i)).ToList();


            var distanceS = Inputs[1].Replace("Distance: ", "");

            while (distanceS.Contains("  "))
                distanceS = distanceS.Replace("  ", " ");

            _distances = distanceS.Trim().Split(" ").Select(i => int.Parse(i)).ToList();
        }

        public override string RunPart1()
        {
            int totalWins = 1;

            for (int t = 0; t < _times.Count; t++)
            {
                int time = _times[t];
                int distance = _distances[t];
                int wins = 0;

                for (int i = 0; i <= time / 2; i++)
                    if ((time - i) * i > distance)
                        wins++;

                totalWins *= 2 * wins - (time % 2 == 0 ? 1 : 0);
            }

            return totalWins.ToString();
        }

        public override string RunPart2()
        {
            BigInteger time = BigInteger.Parse(string.Join("", _times));
            BigInteger distance = BigInteger.Parse(string.Join("", _distances));
            BigInteger wins = 0;

            for (BigInteger i = 0; i <= time / 2; i++)
                if ((time - i) * i > distance)
                    wins++;

            return (2 * wins - (time % 2 == 0 ? 1 : 0)).ToString();
        }
    }
}