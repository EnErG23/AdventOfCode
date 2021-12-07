using AdventOfCode.Models;
using AdventOfCode.Helpers;

namespace AdventOfCode.Y2021.Days
{
    public class Day03 : Day
    {
        private const int day = 3;

        public Day03(bool test) : base(day, test) { }

        public override string RunPart1()
        {
            var gamma = "";
            var epsilon = "";

            for (int i = 0; i < Inputs[0].Length; i++)
            {
                var ones = 0;

                foreach (var input in Inputs)
                    if (input[i] == '1')
                        ones++;

                gamma += ones > Inputs.Count / 2 ? '1' : '0';
                epsilon += ones > Inputs.Count / 2 ? '0' : '1';
            }

            return (Convert.ToInt32(gamma, 2) * Convert.ToInt32(epsilon, 2)).ToString();
        }

        public override string RunPart2()
        {
            long result = 0;

            var oxygenInputs = Inputs.ToList();
            var scrubberInputs = Inputs.ToList();

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

            return result.ToString();
        }        

        public override void VisualizePart2()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            for (int i = 3; i > 0; i--)
            {
                Console.Clear();
                Console.WriteLine($"Visualization for 2021.{day}.2");
                Console.WriteLine($"Starting in {i}");
                Thread.Sleep(1000);
            }

            var oxygenInputs = Inputs.ToList();
            var scrubberInputs = Inputs.ToList();

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