using AdventOfCode.Models;

namespace AdventOfCode.Y2023.Days
{
    public class Day03 : Day
    {
        public Day03(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
        {
            int result = 0;

            for (int x = 0; x < Inputs.Count; x++)
                for (int y = 0; y < Inputs[x].Length; y++)
                {
                    if (char.IsDigit(Inputs[x][y]))
                    {
                        int endY = y;
                        string number = "";

                        while (endY <= Inputs[x].Length)
                        {
                            if (endY == Inputs[x].Length || !char.IsDigit(Inputs[x][endY]))
                            {
                                bool hasSymbol = false;

                                for (int sX = Math.Max(x - 1, 0); sX <= Math.Min(x + 1, Inputs.Count - 1); sX++)
                                {
                                    for (int sY = Math.Max(y - 1, 0); sY <= Math.Min(endY, Inputs[0].Length - 1); sY++)
                                    {
                                        if (!(char.IsDigit(Inputs[sX][sY]) || Inputs[sX][sY] == '.'))
                                        {
                                            hasSymbol = true;
                                            break;
                                        }
                                    }

                                    if (hasSymbol)
                                    {
                                        result += int.Parse(number);
                                        break;
                                    }
                                }

                                break;
                            }
                            else
                            {
                                number += Inputs[x][endY++].ToString();
                            }
                        }

                        y = endY;
                    }
                }

            return result.ToString();
        }

        public override string RunPart2()
        {
            var gears = new List<Gear>();

            for (int x = 0; x < Inputs.Count; x++)
                for (int y = 0; y < Inputs[x].Length; y++)
                {
                    if (char.IsDigit(Inputs[x][y]))
                    {
                        int endY = y;
                        string number = "";

                        while (endY <= Inputs[x].Length)
                        {
                            if (endY == Inputs[x].Length || !char.IsDigit(Inputs[x][endY]))
                            {
                                bool hasSymbol = false;

                                for (int sX = Math.Max(x - 1, 0); sX <= Math.Min(x + 1, Inputs.Count - 1); sX++)
                                {
                                    for (int sY = Math.Max(y - 1, 0); sY <= Math.Min(endY, Inputs[0].Length - 1); sY++)
                                    {
                                        if (!(char.IsDigit(Inputs[sX][sY]) || Inputs[sX][sY] == '.'))
                                        {
                                            hasSymbol = true;

                                            if (Inputs[sX][sY] == '*')
                                                if (gears.Exists(g => g.Coords == (sX, sY)))
                                                    gears.First(g => g.Coords == (sX, sY)).Values.Add(int.Parse(number));
                                                else
                                                    gears.Add(new Gear((sX, sY), int.Parse(number)));

                                            break;
                                        }
                                    }

                                    if (hasSymbol)
                                        break;
                                }
                                break;
                            }
                            else
                            {
                                number += Inputs[x][endY++].ToString();
                            }
                        }

                        y = endY;
                    }
                }

            return gears.Where(g => g.Values.Count == 2).Sum(g => g.Ratio).ToString();
        }
    }

    public class Gear
    {
        public (int, int) Coords { get; set; }
        public List<int> Values { get; set; }
        public int Ratio => Values[0] * Values[1];        

        public Gear((int, int) coords, int value)
        {
            Coords = coords;
            Values = new List<int>() { value };
        }
    }
}