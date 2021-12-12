using AdventOfCode.Models;
using AdventOfCode.Y2021.Models;

namespace AdventOfCode.Y2021.Days
{
    public class Day12 : Day
    {
        private List<Cave>? caves;
        private List<List<Cave>>? paths;

        public Day12(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
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

            paths = new();

            AddNextNode(new List<Cave> { caves.First(c => c.Name == "start") }, false);

            return paths.Count().ToString();
        }

        public override string RunPart2()
        {
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

            paths = new();

            AddNextNode(new List<Cave> { caves.First(c => c.Name == "start") }, true);

            return paths.Count().ToString();
        }

        private void AddNextNode(List<Cave> path, bool allowSecondPassInFirstSmallCave)
        {
            var lastCave = path.Last();

            if (lastCave.Name == "end")
                paths.Add(path);
            else
                foreach (var connectedCave in lastCave.ConnectedCaves.Where(c => c.Name != "start"))
                    if (connectedCave.IsBig || !path.Contains(connectedCave) || allowSecondPassInFirstSmallCave)
                    {
                        var newPath = path.ToList();
                        newPath.Add(connectedCave);

                        AddNextNode(newPath, (!connectedCave.IsBig && path.Contains(connectedCave)) ? false : allowSecondPassInFirstSmallCave);
                    }
        }
    }
}