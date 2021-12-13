using AdventOfCode.Helpers;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventOfCode.Models;

namespace AdventOfCode.Y2019.Days
{
    public class Day06 : Day
    {
        private List<SpaceObject>? spaceObjects;

        public Day06(int year, int day, bool test) : base(year, day, test) { }

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

                SpaceObject spaceObject2 = new SpaceObject
                {
                    Name = spaceObjectName2,
                    OrbitsAround = spaceObjects.Find(s => s.Name == spaceObjectName1)
                };

                spaceObjects.Add(spaceObject2);
            }

            return spaceObjects.Sum(s => CalculateOrbits(s)).ToString();
        }

        public override string RunPart2()
        {
            return "Undefined";
        }

        private long CalculateOrbits(SpaceObject spaceObject)
        {
            long result = 0;

            if (spaceObject.OrbitsAround is not null)
                result = 1 + CalculateOrbits(spaceObject.OrbitsAround);

            return result;
        }
    }

    public class SpaceObject
    {
        public string? Name { get; set; }
        public SpaceObject? OrbitsAround { get; set; }
    }
}