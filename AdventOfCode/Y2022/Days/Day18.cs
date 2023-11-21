using AdventOfCode.Models;

namespace AdventOfCode.Y2022.Days
{
    public class Day18 : Day
    {
        private readonly List<Cube> _cubes;
        public Day18(int year, int day, bool test) : base(year, day, test)
            => _cubes = Inputs.Select(i => new Cube(int.Parse(i.Split(",")[0]), int.Parse(i.Split(",")[1]), int.Parse(i.Split(",")[2]))).ToList();

        public override string RunPart1()
        {
            long freeSides = 0;

            foreach (Cube cube in _cubes)
            {
                freeSides += _cubes.Any(c => c.X == cube.X - 1 && c.Y == cube.Y && c.Z == cube.Z) ? 0 : 1;
                freeSides += _cubes.Any(c => c.X == cube.X + 1 && c.Y == cube.Y && c.Z == cube.Z) ? 0 : 1;
                freeSides += _cubes.Any(c => c.X == cube.X && c.Y == cube.Y - 1 && c.Z == cube.Z) ? 0 : 1;
                freeSides += _cubes.Any(c => c.X == cube.X && c.Y == cube.Y + 1 && c.Z == cube.Z) ? 0 : 1;
                freeSides += _cubes.Any(c => c.X == cube.X && c.Y == cube.Y && c.Z == cube.Z - 1) ? 0 : 1;
                freeSides += _cubes.Any(c => c.X == cube.X && c.Y == cube.Y && c.Z == cube.Z + 1) ? 0 : 1;
            }

            return freeSides.ToString();
        }

        public override string RunPart2()
        {
            long freeSides = 0;

            foreach (Cube cube in _cubes)
            {
                List<Cube> checkCubes = new()
                {
                    new(cube.X - 1, cube.Y, cube.Z),
                    new(cube.X + 1, cube.Y, cube.Z),
                    new(cube.X, cube.Y - 1, cube.Z),
                    new(cube.X, cube.Y + 1, cube.Z),
                    new(cube.X, cube.Y, cube.Z - 1),
                    new(cube.X, cube.Y, cube.Z + 1)
                };

                foreach (Cube checkCube in checkCubes)
                {
                    if (_cubes.Any(c => c.X == checkCube.X && c.Y == checkCube.Y && c.Z == checkCube.Z))
                        continue;

                    int checkFreesides = 0;

                    checkFreesides += _cubes.Any(c => c.X > checkCube.X && c.Y == checkCube.Y && c.Z == checkCube.Z) ? 0 : 1;
                    checkFreesides += _cubes.Any(c => c.X < checkCube.X && c.Y == checkCube.Y && c.Z == checkCube.Z) ? 0 : 1;
                    checkFreesides += _cubes.Any(c => c.X == checkCube.X && c.Y > checkCube.Y && c.Z == checkCube.Z) ? 0 : 1;
                    checkFreesides += _cubes.Any(c => c.X == checkCube.X && c.Y < checkCube.Y && c.Z == checkCube.Z) ? 0 : 1;
                    checkFreesides += _cubes.Any(c => c.X == checkCube.X && c.Y == checkCube.Y && c.Z > checkCube.Z) ? 0 : 1;
                    checkFreesides += _cubes.Any(c => c.X == checkCube.X && c.Y == checkCube.Y && c.Z < checkCube.Z) ? 0 : 1;

                    freeSides += checkFreesides > 0 ? 1 : 0;
                }
            }

            return freeSides.ToString();
        }
    }

    public class Cube
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public Cube(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}