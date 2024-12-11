using AdventOfCode.Models;

namespace AdventOfCode.Y2024.Days
{
    public class Day04 : Day
    {
        public Day04(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
        {
            var result = 0;

            for (int y = 0; y < Inputs.Count; y++)
            {
                for (int x = 0; x < Inputs[y].Length; x++)
                {
                    if (Inputs[y][x] == 'X')
                    {
                        //u
                        try
                        {
                            if (Inputs[y - 1][x] == 'M' && Inputs[y - 2][x] == 'A' && Inputs[y - 3][x] == 'S')
                                result++;
                        }
                        catch { }

                        //ur
                        try
                        {
                            if (Inputs[y - 1][x + 1] == 'M' && Inputs[y - 2][x + 2] == 'A' && Inputs[y - 3][x + 3] == 'S')
                                result++;
                        }
                        catch { }

                        //r
                        try
                        {
                            if (Inputs[y][x + 1] == 'M' && Inputs[y][x + 2] == 'A' && Inputs[y][x + 3] == 'S')
                                result++;
                        }
                        catch { }

                        //dr
                        try
                        {
                            if (Inputs[y + 1][x + 1] == 'M' && Inputs[y + 2][x + 2] == 'A' && Inputs[y + 3][x + 3] == 'S')
                                result++;
                        }
                        catch { }

                        //d
                        try
                        {
                            if (Inputs[y + 1][x] == 'M' && Inputs[y + 2][x] == 'A' && Inputs[y + 3][x] == 'S')
                                result++;
                        }
                        catch { }

                        //dl
                        try
                        {
                            if (Inputs[y + 1][x - 1] == 'M' && Inputs[y + 2][x - 2] == 'A' && Inputs[y + 3][x - 3] == 'S')
                                result++;
                        }
                        catch { }

                        //l
                        try
                        {
                            if (Inputs[y][x - 1] == 'M' && Inputs[y][x - 2] == 'A' && Inputs[y][x - 3] == 'S')
                                result++;
                        }
                        catch { }

                        //ul
                        try
                        {
                            if (Inputs[y - 1][x - 1] == 'M' && Inputs[y - 2][x - 2] == 'A' && Inputs[y - 3][x - 3] == 'S')
                                result++;
                        }
                        catch { }
                    }
                }
            }

            return result.ToString();
        }

        public override string RunPart2()
        {
            var result = 0;

            for (int y = 0; y < Inputs.Count; y++)
            {
                for (int x = 0; x < Inputs[y].Length; x++)
                {
                    if (Inputs[y][x] == 'A')
                    {
                        try
                        {
                            if (((Inputs[y - 1][x - 1] == 'M' && Inputs[y + 1][x + 1] == 'S') || (Inputs[y - 1][x - 1] == 'S' && Inputs[y + 1][x + 1] == 'M')) // diagonal l-r
                                && ((Inputs[y - 1][x + 1] == 'M' && Inputs[y + 1][x - 1] == 'S') || (Inputs[y - 1][x + 1] == 'S' && Inputs[y + 1][x - 1] == 'M'))) // diagonal r-l
                                result++;
                        }
                        catch { }
                    }
                }
            }

            return result.ToString();
        }
    }
}