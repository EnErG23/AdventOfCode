using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Y2021.Days
{
    public static class Day03
    {
        static int day = 3;
        static List<string>? inputs;

        public static string? Answer1 { get; set; }
        public static string? Answer2 { get; set; }

        public static void Run(int part, bool test)
        {
            var start = DateTime.Now;

            inputs = InputManager.GetInputAsStrings(day, test);

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
            var start = DateTime.Now;

            long result = 0;

            #region Solution

            var gamma = "";
            var epsilon = "";

            for (int i = 0; i < inputs[0].Length; i++)
            {
                var ones = 0;

                foreach (var input in inputs)
                    if (input[i] == '1')
                        ones++;

                gamma += ones > inputs.Count / 2 ? '1' : '0';
                epsilon += ones > inputs.Count / 2 ? '0' : '1';
            }

            result = Convert.ToInt32(gamma, 2) * Convert.ToInt32(epsilon, 2);

            #endregion

            var ms = Math.Round((DateTime.Now - start).TotalMilliseconds);

            if (result > 0) Answer1 = result.ToString();
            return $"Part 1 ({ms}ms): {result} ";
        }
        
        static string Part2()
        {
            var start = DateTime.Now;

            long result = 0;

            #region Solution

            var oxygenInputs = inputs.ToList();
            var scrubberInputs = inputs.ToList();

            for (int i = 0; i < oxygenInputs[0].Length; i++)
            {
                if (oxygenInputs.Count > 1)
                {
                    var oxygenOnes = 0;

                    foreach (var input in oxygenInputs)
                        if (input[i] == '1')
                            oxygenOnes++;

                    oxygenInputs = oxygenInputs.Where(p => p[i] == (oxygenOnes >= oxygenInputs.Count / 2m ? '1' : '0')).ToList();
                }

                if (scrubberInputs.Count > 1)
                {
                    var scrubberOnes = 0;

                    foreach (var input in scrubberInputs)
                        if (input[i] == '1')
                            scrubberOnes++;

                    scrubberInputs = scrubberInputs.Where(p => p[i] == (scrubberOnes >= scrubberInputs.Count / 2m ? '0' : '1')).ToList();
                }

                if (oxygenInputs.Count == 1 && scrubberInputs.Count == 1)
                    break;
            }

            result = Convert.ToInt32(oxygenInputs[0], 2) * Convert.ToInt32(scrubberInputs[0], 2);

            #endregion

            var ms = Math.Round((DateTime.Now - start).TotalMilliseconds);

            if (result > 0) Answer2 = result.ToString();
            return $"Part 2 ({ms}ms): {result} ";
        }

        // Visualize

        public static void Visualize(int part, bool test)
        {
            inputs = InputManager.GetInputAsStrings(day, test);

            Console.Clear();

            if (part == 1)
                VisualizePart1();
            else if (part == 2)
                VisualizePart2();
            else
            {
                VisualizePart1();
                VisualizePart2();
            }

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Cyan;
        }

        static void VisualizePart1()
        {
        }

        static void VisualizePart2()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            for (int i = 3; i > 0; i--)
            {
                Console.Clear();
                Console.WriteLine($"Visualization for 2021.3.2");
                Console.WriteLine($"Starting in {i}");
                Thread.Sleep(1000);
            }

            var oxygenInputs = inputs.ToList();
            var scrubberInputs = inputs.ToList();

            for (int i = 0; i < oxygenInputs[0].Length; i++)
            {
                Console.Clear();

                for (int j = 0; j < Math.Max(oxygenInputs.Count, scrubberInputs.Count); j++)
                {
                    var oInput = j >= oxygenInputs.Count ? "            " : oxygenInputs[j];
                    var sInput = j >= scrubberInputs.Count ? "            " : scrubberInputs[j];

                    Console.WriteLine($"{oInput}   {sInput}");
                }

                var oxygenToRemove = new List<string>();

                if (oxygenInputs.Count > 1)
                {
                    var oxygenOnes = 0;

                    foreach (var input in oxygenInputs)
                        if (input[i] == '1')
                            oxygenOnes++;

                    oxygenToRemove = oxygenInputs.Where(p => p[i] == (oxygenOnes >= oxygenInputs.Count / 2m ? '0' : '1')).ToList();
                }

                var scrubberToRemove = new List<string>();

                if (scrubberInputs.Count > 1)
                {
                    var scrubberOnes = 0;

                    foreach (var input in scrubberInputs)
                        if (input[i] == '1')
                            scrubberOnes++;

                    scrubberToRemove = scrubberInputs.Where(p => p[i] == (scrubberOnes >= scrubberInputs.Count / 2m ? '1' : '0')).ToList();
                }

                Console.Clear();

                for (int j = 0; j < Math.Max(oxygenInputs.Count, scrubberInputs.Count); j++)
                {
                    var oInput = j >= oxygenInputs.Count ? "            " : oxygenInputs[j];
                    var sInput = j >= scrubberInputs.Count ? "            " : scrubberInputs[j];

                    if (oxygenToRemove.Contains(oInput))
                        Console.ForegroundColor = ConsoleColor.Red;
                    else
                        Console.ForegroundColor = ConsoleColor.Cyan;

                    Console.Write($"{oInput}   ");

                    if (scrubberToRemove.Contains(sInput))
                        Console.ForegroundColor = ConsoleColor.Red;
                    else
                        Console.ForegroundColor = ConsoleColor.Cyan;

                    Console.WriteLine($"{sInput}");
                }

                foreach(var oRemove in oxygenToRemove)
                    oxygenInputs.Remove(oRemove);

                foreach (var sRemove in scrubberToRemove)
                    scrubberInputs.Remove(sRemove);

                Console.ForegroundColor = ConsoleColor.Cyan;
                Thread.Sleep(1000);
                Console.Clear();

                for (int j = 0; j < Math.Max(oxygenInputs.Count, scrubberInputs.Count); j++)
                {
                    var oInput = j >= oxygenInputs.Count ? "            " : oxygenInputs[j];
                    var sInput = j >= scrubberInputs.Count ? "            " : scrubberInputs[j];

                    Console.WriteLine($"{oInput}   {sInput}");
                }

                Thread.Sleep(1000);

                if (oxygenInputs.Count == 1 && scrubberInputs.Count == 1)
                    break;
            }

            Console.Clear();
            Console.WriteLine($"{Convert.ToInt32(oxygenInputs[0], 2)}   {Convert.ToInt32(scrubberInputs[0], 2)}");
            Thread.Sleep(1000);
            Console.Clear();
            Console.WriteLine($"{Convert.ToInt32(oxygenInputs[0], 2)} * {Convert.ToInt32(scrubberInputs[0], 2)}");
            Thread.Sleep(1000);
            Console.Clear();
            Console.WriteLine($"{Convert.ToInt32(oxygenInputs[0], 2) * Convert.ToInt32(scrubberInputs[0], 2)}");
        }
    }
}