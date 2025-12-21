namespace AdventOfCode.Models
{
    public class Location3D
    {
        public long X { get; set; }
        public long Y { get; set; }
        public long Z { get; set; }        

        public Location3D(long x, long y, long z)
        {
            X = x;
            Y = y;
            Z = z;            
        }

        public override string ToString() => $"({X},{Y},{Z})";
    }
}
