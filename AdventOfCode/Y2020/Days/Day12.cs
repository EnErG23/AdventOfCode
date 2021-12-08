using AdventOfCode.Models;
using System.Diagnostics;

namespace AdventOfCode.Y2020.Days
{
    public class Day12 : Day
    {
        public Day12(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
        {
            long result = 0;

            var x = 0;
            var y = 0;
            var d = 1;
            var ds = new List<string>() { "N", "E", "S", "W" };

            foreach (var input in Inputs)
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

			return result.ToString();
        }

        public override string RunPart2()
        {
            long result = 0;

            var x = 0;
            var y = 0;

            var wX = 10;
            var wY = 1;

            foreach (var input in Inputs)
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

			return result.ToString();
        }
    }
}