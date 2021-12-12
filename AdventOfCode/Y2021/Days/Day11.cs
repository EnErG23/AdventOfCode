using AdventOfCode.Models;

namespace AdventOfCode.Y2021.Days
{
    public class Day11 : Day
    {
        private List<List<int>>? octopuses;
        private List<(int, int)>? flashedOctopuses;

        public Day11(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
        {
            octopuses = new();
            Inputs.ForEach(input => octopuses.Add(input.ToList().Select(i => int.Parse(i.ToString())).ToList()));

            long result = 0;

            for (int i = 0; i < 100; i++)
            {
                flashedOctopuses = new();

                Console.WriteLine("Step 1:");
                Console.WriteLine();
                PrintOctopuses();
                Console.WriteLine();

                // First, the energy level of each octopus increases by 1.
                for (int r = 0; r < octopuses.Count; r++)
                {
                    for (int c = 0; c < octopuses[r].Count; c++)
                    {
                        octopuses[r][c]++;
                    }
                }

                Console.WriteLine("a)");
                Console.WriteLine();
                PrintOctopuses();
                Console.WriteLine();

                long flashResult = 0;

                // Then, any octopus with an energy level greater than 9 flashes.This increases the energy level of all adjacent octopuses by 1, including octopuses that are diagonally adjacent. If this causes an octopus to have an energy level greater than 9, it also flashes.This process continues as long as new octopuses keep having their energy level increased beyond 9. (An octopus can only flash at most once per step.)
                while (octopuses.Count(o => o.Contains(9)) > 1 && flashResult > 0)
                {
                    flashResult = Flash();
                    result += flashResult;
                }

                Console.WriteLine("b)");
                Console.WriteLine();
                PrintOctopuses();
                Console.WriteLine();

                // Finally, any octopus that flashed during this step has its energy level set to 0, as it used all of its energy to flash.
                octopuses.ForEach(r => r.ForEach(o => o = o > 9 ? 0 : o));

                Console.WriteLine("c)");
                Console.WriteLine();
                PrintOctopuses();
                Console.WriteLine();

                Console.Read();
            }

            return result.ToString();
        }

        public override string RunPart2()
        {
            return "Undefined";
        }

        private void PrintOctopuses()
        {
            octopuses.ForEach(o => Console.WriteLine(String.Join("", o)));
        }

        private long Flash()
        {
            long result = 0;

            for (int r = 0; r < octopuses.Count; r++)
            {
                for (int c = 0; c < octopuses[r].Count; c++)
                {
                    if (octopuses[r][c] > 9 && !flashedOctopuses.Contains((r, c)))
                    {
                        result++;

                        for (int v = -1; v <= 1; v++)
                            for (int h = -1; h <= 1; h++)
                                octopuses[r + v][c + h]++;
                    }
                }
            }

            return result;
        }
    }
}