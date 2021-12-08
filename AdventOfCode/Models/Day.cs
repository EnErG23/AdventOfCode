using AdventOfCode.Helpers;
using System.Diagnostics;

namespace AdventOfCode.Models
{
    public abstract class Day
    {
        private int year;
        private int day;
        private string time = "00:00.00ms";
        private string time1 = "00:00.00ms";
        private string time2 = "00:00.00ms";

        public string Answer1 { get; set; } = "Unsolved";
        public string Answer2 { get; set; } = "Unsolved";

        public List<string> Inputs { get; set; }

        public Day(int year, int day, bool test)
        {
            this.year = year;
            this.day = day;
            Inputs = InputManager.GetInputAsStrings(year, day, test);
        }

        public void Run(int? part)
        {
            Stopwatch sw = Stopwatch.StartNew();

            if (part == 0 || part == 1)
            {
                Stopwatch sw1 = Stopwatch.StartNew();
                Answer1 = RunPart1();
                sw1.Stop();
                time1 = sw1.Elapsed.ToString("mm\\:ss\\.ffffff");
            }

            if (part == 0 || part == 2)
            {
                Stopwatch sw2 = Stopwatch.StartNew();
                Answer2 = RunPart2();
                sw2.Stop();
                time2 = sw2.Elapsed.ToString("mm\\:ss\\.ffffff");
            }

            sw.Stop();
            time = sw.Elapsed.ToString("mm\\:ss\\.ffffff");

            Print();
        }

        public void Print()
        {
            Console.WriteLine($"Day {day} ({time}):");
            Console.WriteLine($"    Part 1 ({time1}): {Answer1}");
            Console.WriteLine($"    Part 2 ({time2}): {Answer2}");
        }

        public abstract string RunPart1();

        public abstract string RunPart2();

        public void Visualize(int? part)
        {
            Console.Clear();

            if (part == 0 || part == 1)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;

                for (int i = 3; i > 0; i--)
                {
                    Console.Clear();
                    Console.WriteLine($"Visualization for {year}.{day}.2");
                    Console.WriteLine($"Starting in {i}");
                    Thread.Sleep(1000);
                }

                VisualizePart1();
            }

            if (part == 0 || part == 2)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;

                for (int i = 3; i > 0; i--)
                {
                    Console.Clear();
                    Console.WriteLine($"Visualization for {year}.{day}.2");
                    Console.WriteLine($"Starting in {i}");
                    Thread.Sleep(1000);
                }

                VisualizePart2();
            }

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Cyan;
        }

        public virtual void VisualizePart1() { }

        public virtual void VisualizePart2() { }
    }
}