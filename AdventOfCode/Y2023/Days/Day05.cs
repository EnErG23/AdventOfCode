using AdventOfCode.Models;
using System.Numerics;

namespace AdventOfCode.Y2023.Days
{
    public class Day05 : Day
    {
        private List<BigInteger> _seeds;
        private List<Range> _ranges;

        public Day05(int year, int day, bool test) : base(year, day, test)
        {
            _seeds = Inputs[0].Replace("seeds: ", "").Split(" ").Select(s => BigInteger.Parse(s)).ToList();
            _ranges = new List<Range>();

            int category = -1;

            foreach (var input in Inputs.Skip(2))
                if (input == "")
                    continue;
                else if (char.IsDigit(input[0]))
                    _ranges.Add(new Range(category, BigInteger.Parse(input.Split(" ")[0]), BigInteger.Parse(input.Split(" ")[1]), BigInteger.Parse(input.Split(" ")[2])));
                else
                    category++;
        }

        public override string RunPart1()
        {
            BigInteger closestLocation = 0;

            foreach (var seed in _seeds)
            {
                BigInteger source = seed;

                for (int c = 0; c <= _ranges.Max(r => r.Category); c++)
                    if (_ranges.Exists(r => r.Category == c && source >= r.SourceStart && source <= (r.SourceStart + r.RangeLength)))
                    {
                        var range = _ranges.First(r => r.Category == c && source >= r.SourceStart && source < (r.SourceStart + r.RangeLength));
                        source = source - range.SourceStart + range.DestinationStart;
                    }

                closestLocation = closestLocation == 0 ? source : BigInteger.Min(closestLocation, source);
            }

            return closestLocation.ToString();
        }

        public override string RunPart2()
        {
            var seedRanges = new List<(BigInteger, BigInteger)>();

            for (int i = 0; i < _seeds.Count; i += 2)
                seedRanges.Add(new(_seeds[i], _seeds[i] + _seeds[i + 1] - 1));

            for (int c = 0; c <= _ranges.Max(r => r.Category); c++)
            {
                var rangesToCheck = seedRanges.ToList();
                seedRanges = new List<(BigInteger, BigInteger)>();

                foreach (var range in _ranges.Where(r => r.Category == c).OrderBy(r => r.SourceStart))
                {
                    var rangesToAdd = new List<(BigInteger, BigInteger)>();
                    var rangesToRemove = new List<(BigInteger, BigInteger)>();

                    foreach (var seedRange in rangesToCheck.OrderBy(s => s.Item1))
                    {
                        var source = (range.SourceStart, range.SourceEnd);
                        var destination = (range.DestinationStart, range.DestinationEnd);

                        if (seedRange.Item1 < source.Item1)
                        {
                            rangesToRemove.Add(seedRange);

                            if (seedRange.Item2 >= source.Item1)
                            {
                                if (seedRange.Item2 <= source.Item2) // CASE 2
                                {
                                    //UNCHANGED, NO NEED TO KEEP CHECKING
                                    seedRanges.Add((seedRange.Item1, source.Item1 - 1));

                                    //CHANGED 
                                    seedRanges.Add((destination.Item1, seedRange.Item2 - source.Item1 + destination.Item1));
                                }
                                else // CASE 3
                                {
                                    //UNCHANGED, NO NEED TO KEEP CHECKING
                                    seedRanges.Add((seedRange.Item1, source.Item1 - 1));

                                    //CHANGED 
                                    seedRanges.Add((destination.Item1, destination.Item2));

                                    //UNCHANGED, KEEP CHECKING
                                    rangesToAdd.Add((destination.Item2 + 1, seedRange.Item2));
                                }
                            }
                            else
                                seedRanges.Add(seedRange);
                        }
                        else
                        {
                            if (seedRange.Item1 <= source.Item2)
                                if (seedRange.Item2 > source.Item2) // CASE 5
                                {
                                    //CHANGED 
                                    seedRanges.Add((seedRange.Item1 - source.Item1 + destination.Item1, destination.Item2));
                                    rangesToRemove.Add(seedRange);

                                    //UNCHANGED, KEEP CHECKING
                                    rangesToAdd.Add((source.Item2 + 1, seedRange.Item2));
                                }
                                else // CASE 6
                                {
                                    //CHANGED 
                                    seedRanges.Add((seedRange.Item1 - source.Item1 + destination.Item1, seedRange.Item2 - source.Item1 + destination.Item1));
                                    rangesToRemove.Add(seedRange);
                                }
                        }
                    }

                    foreach (var rangeToRemove in rangesToRemove)
                        rangesToCheck.Remove(rangeToRemove);

                    rangesToCheck.AddRange(rangesToAdd);
                }

                seedRanges.AddRange(rangesToCheck);
            }

            return seedRanges.Min(s => s.Item1).ToString();
        }
    }

    public class Range
    {
        public int Category { get; set; }
        public BigInteger DestinationStart { get; set; }
        public BigInteger SourceStart { get; set; }
        public BigInteger RangeLength { get; set; }
        public BigInteger DestinationEnd => DestinationStart + RangeLength - 1;
        public BigInteger SourceEnd => SourceStart + RangeLength - 1;

        public Range(int category, BigInteger destinationStart, BigInteger sourceStart, BigInteger rangeLength)
        {
            Category = category;
            DestinationStart = destinationStart;
            SourceStart = sourceStart;
            RangeLength = rangeLength;
        }
    }
}