using AdventOfCode.Models;
using AdventOfCode.Y2019.Models;

namespace AdventOfCode.Y2019.Days
{
    public class Day06 : Day
    {
        private List<SpaceObject>? spaceObjects;
        private bool extraTestSpaceObjects;

        public Day06(int year, int day, bool test) : base(year, day, test)
        {
            extraTestSpaceObjects = test;
        }

        public override string RunPart1()
        {
            spaceObjects = new();

            foreach (var input in Inputs)
            {
                string spaceObjectName1 = input.Split(")")[0];
                string spaceObjectName2 = input.Split(")")[1];

                if (!spaceObjects.Exists(s => s.Name == spaceObjectName1))
                {
                    SpaceObject spaceObject1 = new SpaceObject
                    {
                        Name = spaceObjectName1
                    };

                    spaceObjects.Add(spaceObject1);
                }

                if (!spaceObjects.Exists(s => s.Name == spaceObjectName2))
                {
                    SpaceObject spaceObject2 = new SpaceObject
                    {
                        Name = spaceObjectName2
                    };

                    spaceObjects.Add(spaceObject2);
                }

                spaceObjects.First(s => s.Name == spaceObjectName2).OrbitsAround = spaceObjects.First(s => s.Name == spaceObjectName1);
            }

            return spaceObjects.Sum(s => CalculateOrbits(s)).ToString();
        }

        public override string RunPart2()
        {
            if (spaceObjects is null)
            {
                spaceObjects = new();

                foreach (var input in Inputs)
                {
                    string spaceObjectName1 = input.Split(")")[0];
                    string spaceObjectName2 = input.Split(")")[1];

                    if (!spaceObjects.Exists(s => s.Name == spaceObjectName1))
                    {
                        SpaceObject spaceObject1 = new SpaceObject
                        {
                            Name = spaceObjectName1
                        };

                        spaceObjects.Add(spaceObject1);
                    }

                    if (!spaceObjects.Exists(s => s.Name == spaceObjectName2))
                    {
                        SpaceObject spaceObject2 = new SpaceObject
                        {
                            Name = spaceObjectName2
                        };

                        spaceObjects.Add(spaceObject2);
                    }

                    spaceObjects.First(s => s.Name == spaceObjectName2).OrbitsAround = spaceObjects.First(s => s.Name == spaceObjectName1);
                }
            }

            if (extraTestSpaceObjects)
            {
                SpaceObject youSpaceObject = new SpaceObject
                {
                    Name = "YOU",
                    OrbitsAround = spaceObjects.First(s => s.Name == "K")
                };
                spaceObjects.Add(youSpaceObject);

                SpaceObject sanSpaceObject = new SpaceObject
                {
                    Name = "SAN",
                    OrbitsAround = spaceObjects.First(s => s.Name == "I")
                };
                spaceObjects.Add(sanSpaceObject);
            }

            List<SpaceObject> youPassedObjects = new();
            List<SpaceObject> sanPassedObjects = new();

            var name = "YOU";

            while (true)
            {
                var spaceObject = spaceObjects.First(s => s.Name == name);

                if (spaceObject.OrbitsAround is null)
                    break;

                youPassedObjects.Add(spaceObject.OrbitsAround);

                name = spaceObject.OrbitsAround.Name;
            }

            name = "SAN";

            while (true)
            {
                var spaceObject = spaceObjects.First(s => s.Name == name);

                if (spaceObject.OrbitsAround is null)
                    break;

                sanPassedObjects.Add(spaceObject.OrbitsAround);

                name = spaceObject.OrbitsAround.Name;
            }

            long youTillIntersect = -1;

            foreach (var spaceObject in youPassedObjects)
            {
                youTillIntersect++;

                if (sanPassedObjects.Exists(s => s.Name == spaceObject.Name))
                    break;
            }

            long sanTillIntersect = -1;

            foreach (var spaceObject in sanPassedObjects)
            {
                sanTillIntersect++;

                if (youPassedObjects.Exists(s => s.Name == spaceObject.Name))
                    break;
            }

            return (youTillIntersect + sanTillIntersect).ToString();
        }

        private long CalculateOrbits(SpaceObject spaceObject)
        {
            long result = 0;

            if (spaceObject.OrbitsAround is not null)
                result = 1 + CalculateOrbits(spaceObject.OrbitsAround);

            return result;
        }
    }

}