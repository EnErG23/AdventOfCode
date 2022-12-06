using AdventOfCode.Models;

namespace AdventOfCode.Y2022.Days
{
    public class Day06 : Day
    {
        private readonly string _datastream;
        private readonly int _distChars1;
        private readonly int _distChars2;

        public Day06(int year, int day, bool test) : base(year, day, test)
        {
            _datastream = Inputs.First();
            _distChars1 = 4;
            _distChars2 = 14;
        }

        public override string RunPart1()
        {
            return Enumerable
                .Range(_distChars1, _datastream.Length)
                .ToList()
                .FirstOrDefault(m => _datastream.Substring(m - _distChars1, _distChars1).Distinct().Count() == _distChars1)
                .ToString();
        }

        public override string RunPart2()
        {
            return Enumerable
                .Range(_distChars2, _datastream.Length)
                .ToList()
                .FirstOrDefault(m => _datastream.Substring(m - _distChars2, _distChars2).Distinct().Count() == _distChars2)
                .ToString();
        }
    }
}