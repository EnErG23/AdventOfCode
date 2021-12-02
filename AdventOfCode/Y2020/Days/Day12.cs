using AdventOfCode.Helpers;

namespace AdventOfCode.Y2020.Days
{
    public static class Day12
    {
        static int day = 12;
        static List<string>? inputs;

        public static string? Answer1 { get; set; }
        public static string? Answer2 { get; set; }

        public static void Run(int part, bool test)
        {
            inputs = InputManager.GetInputAsStrings(day, test);

            var start = DateTime.Now;

            string part1 = "";
            string part2 = "";

            if (part == 1)
                part1 = Part1();
            else if (part == 2)
                part2 = Part2();
            else
            {
                part1 = Part1();
                part2 = Part2();
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

            var x = 0;
            var y = 0;
            var d = 1;
            var ds = new List<string>() { "N", "E", "S", "W" };

            foreach (var input in inputs)
            {
                //Console.WriteLine($"{input} => {input.Replace("F", ds[d])}");

                switch (input.Replace("F", ds[d])[0])
                {
                    case 'N':
                        y += Convert.ToInt32(input.Substring(1));
                        break;
                    case 'E':
                        x += Convert.ToInt32(input.Substring(1));
                        break;
                    case 'S':
                        y -= Convert.ToInt32(input.Substring(1));
                        break;
                    case 'W':
                        x -= Convert.ToInt32(input.Substring(1));
                        break;
                    case 'L':
                        d -= Convert.ToInt32(input.Substring(1)) / 90;
                        if (d < 0) d += 4;
                        break;
                    case 'R':
                        d += Convert.ToInt32(input.Substring(1)) / 90;
                        if (d > 3) d -= 4;
                        break;
                    default:
                        break;
                }

                //Console.WriteLine($"({x}, {y})");
            }

            result = Math.Abs(x) + Math.Abs(y);

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

            var x = 0;
            var y = 0;

            var wX = 10;
            var wY = 1;

            foreach (var input in inputs)
            {
                var tX = wX;

                switch (input[0])
                {
                    case 'N':
                        wY += Convert.ToInt32(input.Substring(1));
                        break;
                    case 'E':
                        wX += Convert.ToInt32(input.Substring(1));
                        break;
                    case 'S':
                        wY -= Convert.ToInt32(input.Substring(1));
                        break;
                    case 'W':
                        wX -= Convert.ToInt32(input.Substring(1));
                        break;
                    case 'L':
                        switch (Convert.ToInt32(input.Substring(1)))
                        {
                            case 90:
                                wX = -wY;
                                wY = tX;
                                break;
                            case 180:
                                wX = -wX;
                                wY = -wY;
                                break;
                            case 270:
                                wX = wY;
                                wY = -tX;
                                break;
                            default:
                                break;
                        }
                        break;
                    case 'R':
                        switch (Convert.ToInt32(input.Substring(1)))
                        {
                            case 90:
                                wX = wY;
                                wY = -tX;
                                break;
                            case 180:
                                wX = -wX;
                                wY = -wY;
                                break;
                            case 270:
                                wX = -wY;
                                wY = tX;
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        x += Convert.ToInt32(input.Substring(1)) * wX;
                        y += Convert.ToInt32(input.Substring(1)) * wY;
                        break;
                }
            }

            result = Math.Abs(x) + Math.Abs(y);

            #endregion

            var ms = Math.Round((DateTime.Now - start).TotalMilliseconds);

            if (result > 0) Answer2 = result.ToString();
            return $"Part 2 ({ms}ms): {result} ";
        }
    }
}