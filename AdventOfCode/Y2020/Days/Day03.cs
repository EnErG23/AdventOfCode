using AdventOfCode.Models;
using System.Diagnostics;

namespace AdventOfCode.Y2020.Days
{
    public class Day03 : Day
    {
        public Day03(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
        {
            long result = 0;

            result = CheckSlope(Inputs, 3, 1);

			return result.ToString();
        }

        public override string RunPart2()
        {
            long result = 1;

            result = 1 * CheckSlope(Inputs, 1, 1) * CheckSlope(Inputs, 3, 1) * CheckSlope(Inputs, 5, 1) * CheckSlope(Inputs, 7, 1) * CheckSlope(Inputs, 1, 2);

			return result.ToString();
        }

        static long CheckSlope(List<string> Inputs, int right, int down)
        {
            long result = 0;

            var maxX = Inputs[0].Length - 1;
            var x = right;

            var skip = down;

            foreach (var input in Inputs)
            {
                if (skip > 0)
                {
                    skip--;
                    continue;
                }

                result += input[x] == '#' ? 1 : 0;
                x = x + right > maxX ? (x + right) % maxX - 1 : x + right;   

                skip = down - 1;
            }

            return result;
        }

        public override void VisualizePart1()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            for (int i = 3; i > 0; i--)
            {
                Console.Clear();
                Console.WriteLine($"Visualization for 2020.3.1");
                Console.WriteLine($"Starting in {i}");
                Thread.Sleep(1000);
            }

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.DarkBlue;

            VisualizeSlope(Inputs, 3, 1);
        }

        private void VisualizeSlope(List<string> Inputs, int right, int down)
        {
            var maxX = Inputs[0].Length - 1;
            var x = right;
            var y = 0;

            var skip = down;

            foreach (var input in Inputs)
            {
                var row = input.Replace('.', ' ');

                y++;

                if (skip > 0)
                {
                    Console.WriteLine(row);
                    skip--;
                    continue;
                }

                VisualizeRow(row, x);

                Thread.Sleep(100);

                x = x + right > maxX ? x + right - maxX - 1 : x + right;
                skip = down - 1;
            }
        }

        private void VisualizeRow(string row, int x)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write(row[..x]);

            if (row[x] == '#')
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write('X');
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write('O');
            }

            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write(row[(x + 1)..]);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine();
        }
    }
}