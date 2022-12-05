using AdventOfCode.Models;

namespace AdventOfCode.Y2022.Days
{
    public class Day02 : Day
    {
        private readonly IEnumerable<(int, int)> _games;

        public Day02(int year, int day, bool test) : base(year, day, test)
        {
            _games = Inputs.Select(i => (char.Parse(i.Split(' ')[0]) - 64, char.Parse(i.Split(' ')[1]) - 87));
        }

        public override string RunPart1()
        {
            return _games
                .Sum(i => i.Item2 + (i.Item1 == i.Item2 ? 3 :
                                    (i.Item1 == 1 ? (i.Item2 == 2 ? 6 : 0) :
                                    (i.Item1 == 2 ? (i.Item2 == 1 ? 0 : 6) :
                                    (i.Item2 == 1 ? 6 : 0)))))
                .ToString();
        }

        public override string RunPart2()
        {
            return _games
                .Sum(i => (i.Item2 == 2 ? (i.Item1 + 3) :
                                          (i.Item2 == 1 ? (i.Item1 == 1 ? 3 :
                                                          (i.Item1 == 2 ? 1 : 2)) :
                                          (i.Item1 == 1 ? 8 :
                                          (i.Item1 == 2 ? 9 : 7)))))
                .ToString();
        }
    }
}