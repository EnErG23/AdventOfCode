using AdventOfCode.Helpers;
using AdventOfCode.Y2020.Models;

namespace AdventOfCode.Y2020.Days
{
    public static class Day17
    {
        static int day = 17;
        static List<string>? inputs;

        public static string? Answer1 { get; set; }
        public static string? Answer2 { get; set; }

        public static void Run(int part, bool test)
        {
            inputs = InputManager.GetInputAsStrings(day, test);

            var start = DateTime.Now;

            string part1 = "";
            string part2 = "";

            switch (part)
            {
                case 1:
                    part1 = Part1();
                    break;
                case 2:
                    part2 = Part2();
                    break;
                default:
                    part1 = Part1();
                    part2 = Part2();
                    break;
            }

            var ms = Math.Round((DateTime.Now - start).TotalMilliseconds);

            Console.WriteLine($"Day {day} ({ms}ms):");
            if (part1 != "") Console.WriteLine($"    {part1}");
            if (part2 != "") Console.WriteLine($"    {part2}");
        }

        static string Part1()
        {
            long result = 0;

            var start = DateTime.Now;

            #region Solution

            var activeCubes = new List<Cube>();

            var z = 0;
            var y = 0;

            foreach (var input in inputs)
            {
                var x = 0;

                foreach (var inputChar in input)
                {
                    if (inputChar == '#') activeCubes.Add(new Cube { X = x, Y = y, Z = z });
                    x++;
                }
                y++;
            }

            int minX = activeCubes.OrderBy(c => c.X).First().X - 1;
            int maxX = activeCubes.OrderByDescending(c => c.X).First().X + 1;

            int minZ = -1;
            int maxZ = 1;

            int cycles = 6;

            for (int c = 0; c < cycles; c++)
            {
                var newActiveCubes = activeCubes.ToList();

                for (int i = minX; i <= maxX; i++)
                {
                    for (int j = minX; j <= maxX; j++)
                    {
                        for (int k = minZ; k <= maxZ; k++)
                        {
                            var cubeToCheck = activeCubes.Where(a => a.X == i && a.Y == j && a.Z == k);
                            var neighboursActive = 0;

                            for (int m = -1; m <= 1; m++)
                            {
                                for (int n = -1; n <= 1; n++)
                                {
                                    for (int o = -1; o <= 1; o++)
                                    {
                                        if (m == 0 && n == 0 && o == 0) continue;
                                        neighboursActive += activeCubes.Count(a => a.X == (i + m) && a.Y == (j + n) && a.Z == (k + o));
                                    }
                                }
                            }

                            if (cubeToCheck.Count() > 0)
                            {
                                if (neighboursActive < 2 || neighboursActive > 3) newActiveCubes.Remove(cubeToCheck.First());
                            }
                            else
                            {
                                if (neighboursActive == 3) newActiveCubes.Add(new Cube { X = i, Y = j, Z = k });
                            }
                        }
                    }
                }

                activeCubes = newActiveCubes;

                minX--;
                maxX++;
                minZ--;
                maxZ++;
            }

            result = activeCubes.Count();

            #endregion

            var ms = Math.Round((DateTime.Now - start).TotalMilliseconds);

            if (result > 0) Answer1 = result.ToString();
            return $"Part 1 ({ms}ms): {result} ";
        }

        static string Part2()
        {
            long result = 0;

            var start = DateTime.Now;

            #region Solution

            var activeCubes = new List<Cube>();

            var z = 0;
            var y = 0;

            foreach (var input in inputs)
            {
                var x = 0;

                foreach (var inputChar in input)
                {
                    if (inputChar == '#') activeCubes.Add(new Cube { X = x, Y = y, Z = z, W = z });
                    x++;
                }
                y++;
            }

            int minX = activeCubes.OrderBy(c => c.X).First().X - 1;
            int maxX = activeCubes.OrderByDescending(c => c.X).First().X + 1;

            int minZ = -1;
            int maxZ = 1;

            int cycles = 6;

            for (int c = 0; c < cycles; c++)
            {
                var newActiveCubes = activeCubes.ToList();

                for (int i = minX; i <= maxX; i++)
                {
                    for (int j = minX; j <= maxX; j++)
                    {
                        for (int k = minZ; k <= maxZ; k++)
                        {
                            for (int l = minZ; l <= maxZ; l++)
                            {
                                var cubeToCheck = activeCubes.Where(a => a.X == i && a.Y == j && a.Z == k && a.W == l);
                                var neighboursActive = 0;

                                for (int m = -1; m <= 1; m++)
                                {
                                    for (int n = -1; n <= 1; n++)
                                    {
                                        for (int o = -1; o <= 1; o++)
                                        {
                                            for (int p = -1; p <= 1; p++)
                                            {
                                                if (m == 0 && n == 0 && o == 0 && p == 0) continue;
                                                neighboursActive += activeCubes.Count(a => a.X == (i + m) && a.Y == (j + n) && a.Z == (k + o) && a.W == (l + p));
                                            }
                                        }
                                    }
                                }

                                if (cubeToCheck.Count() > 0)
                                {
                                    if (neighboursActive < 2 || neighboursActive > 3) newActiveCubes.Remove(cubeToCheck.First());
                                }
                                else
                                {
                                    if (neighboursActive == 3) newActiveCubes.Add(new Cube { X = i, Y = j, Z = k, W = l });
                                }
                            }
                        }
                    }
                }

                activeCubes = newActiveCubes;

                minX--;
                maxX++;
                minZ--;
                maxZ++;
            }

            result = activeCubes.Count();

            #endregion

            var ms = Math.Round((DateTime.Now - start).TotalMilliseconds);

            if (result > 0) Answer2 = result.ToString();
            return $"Part 2 ({ms}ms): {result} ";
        }
    }
}