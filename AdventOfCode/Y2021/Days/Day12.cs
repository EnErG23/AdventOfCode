using AdventOfCode.Models;

namespace AdventOfCode.Y2021.Days
{
    public class Day12 : Day
    {
        private List<Cave>? caves;
        private List<List<Cave>>? paths;
        private List<Path>? paths2;

        public Day12(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
        {
            long result = 0;

            caves = new();
            paths = new();

            foreach (var input in Inputs)
            {
                var newCaves = input.Split("-");

                if (caves.Count(c => c.Name == newCaves[0]) == 0)
                    caves.Add(new Cave(newCaves[0]));

                if (caves.Count(c => c.Name == newCaves[1]) == 0)
                    caves.Add(new Cave(newCaves[1]));

                Cave cave1 = caves.First(c => c.Name == newCaves[0]);
                Cave cave2 = caves.First(c => c.Name == newCaves[1]);

                cave1.ConnectedCaves.Add(cave2);
                cave2.ConnectedCaves.Add(cave1);
            }

            AddNextNode(new List<Cave> { caves.First(c => c.Name == "start") });

            result = paths.Count();

            return result.ToString();
        }

        public override string RunPart2()
        {
            long result = 0;

            if (caves == null)
            {
                caves = new();

                foreach (var input in Inputs)
                {
                    var newCaves = input.Split("-");

                    if (caves.Count(c => c.Name == newCaves[0]) == 0)
                        caves.Add(new Cave(newCaves[0]));

                    if (caves.Count(c => c.Name == newCaves[1]) == 0)
                        caves.Add(new Cave(newCaves[1]));

                    Cave cave1 = caves.First(c => c.Name == newCaves[0]);
                    Cave cave2 = caves.First(c => c.Name == newCaves[1]);

                    cave1.ConnectedCaves.Add(cave2);
                    cave2.ConnectedCaves.Add(cave1);
                }
            }

            paths2 = new();

            AddNextNode2(new Path(new List<Cave> { caves.First(c => c.Name == "start") }));

            result = paths2.Count();

            return result.ToString();
        }

        private void AddNextNode(List<Cave> currentPath)
        {
            var lastCave = currentPath.Last();

            if (lastCave.Name == "end")
                paths.Add(currentPath);
            else
                foreach (var connectedCave in lastCave.ConnectedCaves)
                    if (connectedCave.IsBig || !currentPath.Contains(connectedCave))
                    {
                        var newCurrentPath = currentPath.ToList();

                        newCurrentPath.Add(connectedCave);
                        AddNextNode(newCurrentPath);
                    }
        }

        private void AddNextNode2(Path currentPath)
        {
            var lastCave = currentPath.Caves.Last();

            if (lastCave.Name == "end")
                paths2.Add(currentPath);
            else
                foreach (var connectedCave in lastCave.ConnectedCaves.Where(c => c.Name != "start"))
                    if (connectedCave.IsBig || !currentPath.Caves.Contains(connectedCave) || (currentPath.Caves.Contains(connectedCave) && !currentPath.SmallCavePassedTwice))
                    {
                        var newCurrentPath = new Path(currentPath.Caves.ToList(), currentPath.SmallCavePassedTwice);

                        if (!connectedCave.IsBig && newCurrentPath.Caves.Contains(connectedCave))
                            newCurrentPath.SmallCavePassedTwice = true;

                        newCurrentPath.Caves.Add(connectedCave);

                        AddNextNode2(newCurrentPath);
                    }
        }

        public class Cave
        {
            public string Name { get; set; }
            public bool IsBig { get; set; }
            public List<Cave> ConnectedCaves { get; set; }

            public Cave(string name)
            {
                Name = name;
                IsBig = Char.IsUpper(name[0]);
                ConnectedCaves = new();
            }
        }

        public class Path
        {
            public List<Cave> Caves { get; set; }
            public bool SmallCavePassedTwice { get; set; }

            public Path(List<Cave> caves)
            {
                Caves = caves;
                SmallCavePassedTwice = false;
            }

            public Path(List<Cave> caves, bool smallCavePassedTwice)
            {
                Caves = caves;
                SmallCavePassedTwice = smallCavePassedTwice;
            }
        }
    }
}