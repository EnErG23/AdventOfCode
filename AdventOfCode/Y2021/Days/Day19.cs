using AdventOfCode.Models;

namespace AdventOfCode.Y2021.Days
{
    public class Day19 : Day
    {
        private List<Scanner>? scanners;

        public Day19(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
        {
            scanners = new();

            int name = 0;

            Scanner scanner = new(name++);

            foreach (var input in Inputs)
            {
                if (input == "")
                {
                    scanners.Add(scanner);
                    scanner = new(name++);
                    continue;
                }
                else if (input.Substring(0, 3) == "---")
                    continue;

                List<int> coords = input.Split(",").Select(i => int.Parse(i)).ToList();

                scanner.Beacons.Add(new Coord(coords[0], coords[1], coords[2]));
            }

            scanners.Add(scanner);

            List<Scanner> positionedScanners = new();

            while (positionedScanners.Count != scanners.Count)
            {
                //Console.WriteLine("Loop");

                positionedScanners = new();
                positionedScanners.Add(scanners[0]);
                positionedScanners.AddRange(scanners.Skip(1).Where(s => s.Position.X + s.Position.Y + s.Position.Z != 0).ToList());

                //Console.WriteLine();

                //foreach (var positionedScanner in positionedScanners)
                //    Console.WriteLine($"({positionedScanner.Position.X}, {positionedScanner.Position.Y}, {positionedScanner.Position.Z})");

                //Console.WriteLine();

                List<Scanner> unPositionedScanners = scanners.Skip(1).Where(s => s.Position.X + s.Position.Y + s.Position.Z == 0).ToList();

                foreach (var positionedScanner in positionedScanners)
                {
                    foreach (var unPositionedScanner in unPositionedScanners)
                    {
                        bool matchFound = false;

                        for (int x = 0; x < 4; x++)
                        {
                            for (int y = 0; y < 4; y++)
                            {
                                for (int z = 0; z < 4; z++)
                                {
                                    var beacons = unPositionedScanner.GetRotatedBeacons(x, y, z);

                                    //Console.WriteLine($"Scanner {positionedScanner.Name} vs Scanner {unPositionedScanner.Name}");
                                    //Console.WriteLine($"Rotation: ({x}, {y}, {z})");
                                    //Console.WriteLine($"-------------------------");

                                    //beacons.ForEach(b => Console.WriteLine($"({b.X}, {b.Y}, {b.Z})"));
                                    //Console.WriteLine($"-------------------------");

                                    (bool, Coord) checkResult = CheckMatchingBeacons(positionedScanner, beacons);

                                    //Console.WriteLine($"({checkResult.Item1}, ({checkResult.Item2.X}, {checkResult.Item2.Y}, {checkResult.Item2.Z}))");
                                    //Console.WriteLine($"-------------------------");

                                    //CHECK MATCHING BEACONS
                                    if (checkResult.Item1)
                                    {
                                        scanners.First(s => s.Name == unPositionedScanner.Name).Position = checkResult.Item2;
                                        scanners.First(s => s.Name == unPositionedScanner.Name).Beacons = beacons;
                                        //scanners.First(s => s.Name == unPositionedScanner.Name).Beacons = GetBeaconsRelativeToOrigin(checkResult.Item2, beacons);

                                        //Console.WriteLine($"Match found");
                                        //Console.WriteLine($"-------------------------");
                                        //Console.WriteLine();
                                        //Console.Read();

                                        matchFound = true;
                                        break;
                                    }

                                    //Console.WriteLine();
                                }
                                if (matchFound)
                                    break;
                            }
                            if (matchFound)
                                break;
                        }
                    }
                }

                //Console.Read();
            }

            List<Coord> distinctBeacons = new();

            foreach (var s in scanners)
                foreach (var beacon in s.Beacons)
                {
                    Coord coord = new(s.Position.X + beacon.X, s.Position.Y + beacon.Y, s.Position.Z + beacon.Z);

                    if (!distinctBeacons.Exists(b => b.X == coord.X && b.Y == coord.Y && b.Z == coord.Z))
                        distinctBeacons.Add(coord);
                }

            return distinctBeacons.Count.ToString();
        }

        public override string RunPart2()
        {
            if (scanners is null)
                RunPart1();

            long maxDistance = 0;

            foreach (var s1 in scanners)
                Console.WriteLine($"{s1.Name} ({s1.Position.X}, {s1.Position.Y}, {s1.Position.Z}");

            foreach (var s1 in scanners)
                foreach (var s2 in scanners)
                {
                    Console.WriteLine($"{s1.Name} => {s2.Name}: {Math.Abs(s1.Position.X - s2.Position.X) + Math.Abs(s1.Position.Y - s2.Position.Y) + Math.Abs(s1.Position.Z - s2.Position.Z)}");
                    maxDistance = Math.Max(maxDistance, Math.Abs(s1.Position.X - s2.Position.X) + Math.Abs(s1.Position.Y - s2.Position.Y) + Math.Abs(s1.Position.Z - s2.Position.Z));
                }

            return maxDistance.ToString();
        }

        private (bool, Coord) CheckMatchingBeacons(Scanner scanner1, List<Coord> beacons2)
        {
            //Console.WriteLine();
            //Console.WriteLine($"Comparing beacons");
            //Console.WriteLine($"-----------------");

            List<Coord> scanner2Positions = new();

            var origin = scanner1.Position;

            foreach (var beacon1 in scanner1.Beacons)
                foreach (var beacon2 in beacons2)
                {
                    //Console.Write($"Beacon 1 ({beacon1.X}, {beacon1.Y}, {beacon1.Z}) vs Beacon 2 ({beacon2.X}, {beacon2.Y}, {beacon2.Z}) => ");
                    //Console.WriteLine($"Scanner 2 position: ({beacon1.X - beacon2.X}, {beacon1.Y - beacon2.Y}, {beacon1.Z - beacon2.Z})");

                    scanner2Positions.Add(new Coord(origin.X + beacon1.X - beacon2.X, origin.Y + beacon1.Y - beacon2.Y, origin.Z + beacon1.Z - beacon2.Z));
                }

            //Console.WriteLine($"-----------------");

            var scanner2PositionOccurences = scanner2Positions.GroupBy(c => new { c.X, c.Y, c.Z })
                    .Select(s => new
                    {
                        Coord = s.FirstOrDefault(),
                        Occurence = s.Count()
                    })
                    .OrderByDescending(p => p.Occurence);

            //Console.WriteLine($"Most occuring position: ({scanner2PositionOccurences.First().Coord.X}, {scanner2PositionOccurences.First().Coord.Y}, {scanner2PositionOccurences.First().Coord.Z}) => {scanner2PositionOccurences.First().Occurence}");
            //Console.WriteLine($"-----------------");

            if (scanner2PositionOccurences.First().Occurence > 11)
                return (true, scanner2PositionOccurences.First().Coord);

            return (false, new Coord(0, 0, 0));
        }
    }

    public class Scanner
    {
        public int Name { get; set; }
        public Coord Position { get; set; }
        public List<Coord> Beacons { get; set; }

        public Scanner(int name)
        {
            Name = name;
            Position = new Coord(0, 0, 0);
            Beacons = new();
        }

        public List<Coord> GetRotatedBeacons(int x, int y, int z)
        {
            List<Coord> rotatedBeacons = Beacons.Select(b => new Coord(b.X, b.Y, b.Z)).ToList();

            //ROTATE BEACON VALUES

            //ROTATE X
            switch (x)
            {
                case 1: // y = z && z = -y
                    foreach (var beacon in rotatedBeacons)
                    {
                        int bY = beacon.Y;

                        beacon.Y = beacon.Z;
                        beacon.Z = -bY;
                    }
                    break;
                case 2: // y = -y && z = -z
                    foreach (var beacon in rotatedBeacons)
                    {
                        beacon.Y = -beacon.Y;
                        beacon.Z = -beacon.Z;
                    }
                    break;
                case 3: // y = -z && z = y
                    foreach (var beacon in rotatedBeacons)
                    {
                        int bY = beacon.Y;

                        beacon.Y = -beacon.Z;
                        beacon.Z = bY;
                    }
                    break;
                default:
                    break;
            }

            //ROTATE Y
            switch (y)
            {
                case 1: // x = -z && z = x
                    foreach (var beacon in rotatedBeacons)
                    {
                        int bX = beacon.X;

                        beacon.X = -beacon.Z;
                        beacon.Z = bX;
                    }
                    break;
                case 2: // x = -x && z = -z
                    foreach (var beacon in rotatedBeacons)
                    {
                        beacon.X = -beacon.X;
                        beacon.Z = -beacon.Z;
                    }
                    break;
                case 3: // x = z && z = -x
                    foreach (var beacon in rotatedBeacons)
                    {
                        int bX = beacon.X;

                        beacon.X = beacon.Z;
                        beacon.Z = -bX;
                    }
                    break;
                default:
                    break;
            }

            //ROTATE Z
            switch (z)
            {
                case 1: // x = y && y = -x
                    foreach (var beacon in rotatedBeacons)
                    {
                        int bX = beacon.X;

                        beacon.X = beacon.Y;
                        beacon.Y = -bX;
                    }
                    break;
                case 2: // x = -x && y = -y
                    foreach (var beacon in rotatedBeacons)
                    {
                        beacon.X = -beacon.X;
                        beacon.Y = -beacon.Y;
                    }
                    break;
                case 3: // x = -y && y = x
                    foreach (var beacon in rotatedBeacons)
                    {
                        int bX = beacon.X;

                        beacon.X = -beacon.Y;
                        beacon.Y = bX;
                    }
                    break;
                default:
                    break;
            }

            return rotatedBeacons;
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