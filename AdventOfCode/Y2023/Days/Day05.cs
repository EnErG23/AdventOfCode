using AdventOfCode.Models;
using AdventOfCode.Y2023.Models;

namespace AdventOfCode.Y2023.Days
{
    public class Day05 : Day
    {
        private List<long> _seeds;
        private List<Interval> _intervals;

        public Day05(int year, int day, bool test) : base(year, day, test)
        {
            _seeds = Inputs[0].Replace("seeds: ", "").Split(" ").Select(s => long.Parse(s)).ToList();
            _intervals = new List<Interval>();

            int category = -1;

            foreach (var input in Inputs.Skip(2))
                if (input == "")
                    continue;
                else if (char.IsDigit(input[0]))
                    _intervals.Add(new Interval(category, long.Parse(input.Split(" ")[0]), long.Parse(input.Split(" ")[1]), long.Parse(input.Split(" ")[2])));
                else
                    category++;
        }

        public override string RunPart1()
        {
            long closestLocation = 0;

            foreach (var seed in _seeds)
            {
                long source = seed;

                for (int c = 0; c <= _intervals.Max(r => r.Category); c++)
                    if (_intervals.Exists(r => r.Category == c && source >= r.SourceStart && source <= (r.SourceStart + r.IntervalLength)))
                    {
                        var interval = _intervals.First(r => r.Category == c && source >= r.SourceStart && source < (r.SourceStart + r.IntervalLength));
                        source = source - interval.SourceStart + interval.DestinationStart;
                    }

                closestLocation = closestLocation == 0 ? source : Math.Min(closestLocation, source);
            }

            return closestLocation.ToString();
        }

        public override string RunPart2()
        {
            var seedIntervals = new List<(long, long)>();

            for (int i = 0; i < _seeds.Count; i += 2)
                seedIntervals.Add(new(_seeds[i], _seeds[i] + _seeds[i + 1] - 1));

            for (int c = 0; c <= _intervals.Max(r => r.Category); c++)
            {
                var intervalsToCheck = seedIntervals.ToList();
                seedIntervals = new List<(long, long)>();

                foreach (var interval in _intervals.Where(r => r.Category == c).OrderBy(r => r.SourceStart))
                {
                    var intervalsToAdd = new List<(long, long)>();
                    var intervalsToRemove = new List<(long, long)>();

                    foreach (var seedInterval in intervalsToCheck.OrderBy(s => s.Item1))
                    {
                        var source = (interval.SourceStart, interval.SourceEnd);
                        var destination = (interval.DestinationStart, interval.DestinationEnd);

                        if (seedInterval.Item1 < source.Item1)
                        {
                            intervalsToRemove.Add(seedInterval);

                            if (seedInterval.Item2 >= source.Item1)
                            {
                                if (seedInterval.Item2 <= source.Item2) // CASE 2
                                {
                                    //UNCHANGED, NO NEED TO KEEP CHECKING
                                    seedIntervals.Add((seedInterval.Item1, source.Item1 - 1));

                                    //CHANGED 
                                    seedIntervals.Add((destination.Item1, seedInterval.Item2 - source.Item1 + destination.Item1));
                                }
                                else // CASE 3
                                {
                                    //UNCHANGED, NO NEED TO KEEP CHECKING
                                    seedIntervals.Add((seedInterval.Item1, source.Item1 - 1));

                                    //CHANGED 
                                    seedIntervals.Add((destination.Item1, destination.Item2));

                                    //UNCHANGED, KEEP CHECKING
                                    intervalsToAdd.Add((destination.Item2 + 1, seedInterval.Item2));
                                }
                            }
                            else
                                seedIntervals.Add(seedInterval);
                        }
                        else
                        {
                            if (seedInterval.Item1 <= source.Item2)
                                if (seedInterval.Item2 > source.Item2) // CASE 5
                                {
                                    //CHANGED 
                                    seedIntervals.Add((seedInterval.Item1 - source.Item1 + destination.Item1, destination.Item2));
                                    intervalsToRemove.Add(seedInterval);

                                    //UNCHANGED, KEEP CHECKING
                                    intervalsToAdd.Add((source.Item2 + 1, seedInterval.Item2));
                                }
                                else // CASE 6
                                {
                                    //CHANGED 
                                    seedIntervals.Add((seedInterval.Item1 - source.Item1 + destination.Item1, seedInterval.Item2 - source.Item1 + destination.Item1));
                                    intervalsToRemove.Add(seedInterval);
                                }
                        }
                    }

                    foreach (var intervalToRemove in intervalsToRemove)
                        intervalsToCheck.Remove(intervalToRemove);

                    intervalsToCheck.AddRange(intervalsToAdd);
                }

                seedIntervals.AddRange(intervalsToCheck);
            }

            return seedIntervals.Min(s => s.Item1).ToString();
        }
    }
}