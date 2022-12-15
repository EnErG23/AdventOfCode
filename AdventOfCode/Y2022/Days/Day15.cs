using AdventOfCode.Models;

namespace AdventOfCode.Y2022.Days
{
    public class Day15 : Day
    {
        private readonly List<Sensor> _sensors;
        private bool _test;

        public Day15(int year, int day, bool test) : base(year, day, test)
        {
            _test = test;

            _sensors = Inputs
                .Select(i => i.Replace("Sensor at x=", "").Replace(" closest beacon is at x=", "").Replace(" y=", "").Replace(":", ",").Split(","))
                .Select(i => new Sensor(long.Parse(i[0]), long.Parse(i[1]), long.Parse(i[2]), long.Parse(i[3])))
                .ToList();
        }

        public override string RunPart1()
            => CheckRow(_test ? 10 : 2000000, 0).ToString();

        //TODO: Speed up (20s)
        public override string RunPart2()
        {
            int max = _test ? 20 : 4000000;

            for (long y = 0; y <= max; y++)
                if (CheckRow(y, max) == max)
                    for (long x = 0; x <= max; x++)
                        if (_sensors.Count(s => (Math.Abs(s.X - x) + Math.Abs(s.Y - y)) <= s.MD) == 0)
                            return ((x * 4000000) + y).ToString();

            return "undefined";
        }

        public long CheckRow(long rowToCheck, long max)
        {
            List<(long, long)> _ranges = new();

            foreach (Sensor sensor in _sensors)
            {
                var topReach = sensor.TopReach;
                var bottomReach = sensor.BottomReach;

                if (sensor.Y < rowToCheck && bottomReach >= rowToCheck)
                {
                    var rowsBelow = bottomReach - rowToCheck;
                    _ranges.Add((sensor.X - rowsBelow, sensor.X + rowsBelow));
                }
                else if (sensor.Y > rowToCheck && topReach <= rowToCheck)
                {
                    var rowsAbove = rowToCheck - topReach;
                    _ranges.Add((sensor.X - rowsAbove, sensor.X + rowsAbove));
                }
                else if (sensor.Y == rowToCheck)
                    _ranges.Add((sensor.X - sensor.MD, sensor.X + sensor.MD));
            }

            List<int> rangesToRemove = new();

            for (int i = 0; i < _ranges.Count; i++)
            {
                var range1 = _ranges[i];

                for (int j = 0; j < _ranges.Count; j++)
                {
                    if (i == j || rangesToRemove.Contains(i))
                        continue;

                    var range2 = _ranges[j];

                    if (range2.Item1 > range1.Item2 || range2.Item2 < range1.Item1)
                        continue;
                    else if (range2.Item1 > range1.Item1)
                    {
                        if (range2.Item2 > range1.Item2)
                            _ranges[j] = (range1.Item2 + 1, range2.Item2);
                        else
                            rangesToRemove.Add(j);
                    }
                    else if (range2.Item1 < range1.Item1)
                    {
                        if (range2.Item2 < range1.Item2)
                            _ranges[j] = (range2.Item1, range1.Item1 - 1);
                    }
                    else
                    {
                        if (range2.Item2 <= range1.Item2)
                            rangesToRemove.Add(j);
                    }
                }
            }

            List<(long, long)> uniqueRanges = new();

            for (int i = 0; i < _ranges.Count; i++)
            {
                if (!rangesToRemove.Contains(i) && (max == 0 ? true : (_ranges[i].Item1 <= max && _ranges[i].Item2 >= 0)))
                {
                    long x = max == 0 ? _ranges[i].Item1 : (_ranges[i].Item1 < 0 ? 0 : _ranges[i].Item1);
                    long y = max == 0 ? _ranges[i].Item2 : (_ranges[i].Item2 > max ? max : _ranges[i].Item2);
                    uniqueRanges.Add((x, y));
                }
            }

            return uniqueRanges.Sum(u => Math.Abs(u.Item2 - u.Item1) + 1) - (max == 0 ? _sensors.Where(s => s.BeaconY == rowToCheck).Select(s => s.BeaconX).Distinct().Count() : 0);
        }
    }

    public class Sensor
    {
        public Sensor(long x, long y, long beaconX, long beaconY)
        {
            X = x;
            Y = y;
            BeaconX = beaconX;
            BeaconY = beaconY;
        }

        public long X { get; set; }
        public long Y { get; set; }
        public long BeaconX { get; set; }
        public long BeaconY { get; set; }

        public long MD => Math.Abs(X - BeaconX) + Math.Abs(Y - BeaconY);
        public long TopReach => Y - MD;
        public long BottomReach => Y + MD;
        public long LeftReach => X - MD;
        public long RightReach => X + MD;
    }
}