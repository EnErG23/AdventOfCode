namespace AdventOfCode.Y2023.Models
{
    public class Interval
    {
        public int Category { get; set; }
        public long DestinationStart { get; set; }
        public long SourceStart { get; set; }
        public long IntervalLength { get; set; }
        public long DestinationEnd => DestinationStart + IntervalLength - 1;
        public long SourceEnd => SourceStart + IntervalLength - 1;

        public Interval(int category, long destinationStart, long sourceStart, long rangeLength)
        {
            Category = category;
            DestinationStart = destinationStart;
            SourceStart = sourceStart;
            IntervalLength = rangeLength;
        }
    }
}