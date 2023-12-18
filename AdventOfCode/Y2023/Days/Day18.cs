using AdventOfCode.Helpers;
using AdventOfCode.Models;

namespace AdventOfCode.Y2023.Days
{
    public class Day18 : Day
    {
        public Day18(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
        {
            List<Helpers.Node> nodes = new();

            int r = 0;
            int c = 0;

            foreach (var input in Inputs)
            {
                int length = int.Parse(input.Split(' ')[1]);

                switch (input[0])
                {
                    case 'U':
                        r -= length;
                        break;
                    case 'R':
                        c += length;
                        break;
                    case 'D':
                        r += length;
                        break;
                    case 'L':
                        c -= length;
                        break;
                }

                nodes.Add(new Helpers.Node((r, c)));
            }

            return (Algorithms.IrregularPolygonCircumference(nodes) + Algorithms.PicksTheorem(nodes)).ToString();
        }

        public override string RunPart2()
        {
            List<Helpers.Node> nodes = new();

            long r = 0;
            long c = 0;

            foreach (var input in Inputs)
            {
                long length = Convert.ToInt32(input.Split('#')[1].Substring(0, 5), 16);
                switch (input[input.Length - 2])
                {
                    case '3':
                        r -= length;
                        break;
                    case '0':
                        c += length;
                        break;
                    case '1':
                        r += length;
                        break;
                    case '2':
                        c -= length;
                        break;
                }

                nodes.Add(new Helpers.Node((r, c)));
            }

            return (Algorithms.IrregularPolygonCircumference(nodes) + Algorithms.PicksTheorem(nodes)).ToString();
        }
    }
}