using AdventOfCode.Models;

namespace AdventOfCode.Y2021.Days
{
    public class Day19 : Day
    {
        public Day19(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
        {
            List<Scanner> scanners = new();

            Scanner scanner = new();

            foreach (var input in Inputs)
            {
                if (input.Substring(0, 3) == "---")
                    continue;

                if (input == "")
                {
                    scanners.Add(scanner);
                    scanner = new();
                    continue;
                }

                List<int> coords = input.Split(",").Select(i => int.Parse(i)).ToList();

                scanner.Beacons.Add(new Coord(coords[0], coords[1], coords[2]));
            }

            while (scanners.Skip(1).ToList().Exists(s => s.Position.X + s.Position.Y + s.Position.Z == 0))
            {
                List<Scanner> positionedScanners = new();
                positionedScanners.Add(scanners[0]);
                positionedScanners.AddRange(scanners.Skip(1).Where(s => s.Position.X + s.Position.Y + s.Position.Z != 0).ToList());

                List<Scanner> unPositionedScanners = scanners.Skip(1).Where(s => s.Position.X + s.Position.Y + s.Position.Z == 0).ToList();

                foreach (var positionedScanner in positionedScanners)
                {
                    foreach (var unPositionedScanner in unPositionedScanners)
                    {

                    }
                }
            }

            return "Undefined";
        }

        public override string RunPart2()
        {
            return "Undefined";
        }
    }

    public class Scanner
    {
        public Coord Position { get; set; }
        public List<Coord> Beacons { get; set; }

        public Scanner()
        {
            Position = new Coord(0, 0, 0);
            Beacons = new();
        }
    }

    public class Coord
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public Coord(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}