using AdventOfCode.Helpers;
using System.Diagnostics;

namespace AdventOfCode.Models
{
    public abstract class Day
    {
        private int year;
        private int day;
        private TimeSpan time;
        private TimeSpan time1;
        private TimeSpan time2;

        public string Answer1 { get; set; } = "Undefined";
        public string Answer2 { get; set; } = "Undefined";

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
                time1 = sw1.Elapsed;
            }

            if (part == 0 || part == 2)
            {
                Stopwatch sw2 = Stopwatch.StartNew();
                Answer2 = RunPart2();
                sw2.Stop();
                time2 = sw2.Elapsed;
            }

            sw.Stop();
            time = sw.Elapsed;

            Print();
        }

        public void Print()
        {
            #region Part 1

            Console.Write("║   ");

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($" {(day < 10 ? $" {day}" : day)}");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("   ║   ");

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("   1");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("   ║   ");

            Console.ForegroundColor = ConsoleColor.White;
            var answer1 = "";

            for (int i = 0; i < (16 - Answer1.Length); i++)
                answer1 += " ";

            answer1 += Answer1;
            Console.Write(answer1);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("   ║   ");

            if (time1 > new TimeSpan(0, 0, 0, 1))
                Console.ForegroundColor = ConsoleColor.Red;
            else if (time1 > new TimeSpan(0, 0, 0, 0, 100))
                Console.ForegroundColor = ConsoleColor.Yellow;
            else
                Console.ForegroundColor = ConsoleColor.Green;

            Console.Write(time1.ToString("mm\\:ss\\.ffffff"));

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("   ║");
            Console.WriteLine();

            #endregion

            #region Part 2

            Console.Write("║   ");

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("   ");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("   ║   ");

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("   2");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("   ║   ");

            Console.ForegroundColor = ConsoleColor.White;
            var answer2 = "";

            for (int i = 0; i < (16 - Answer2.Length); i++)
                answer2 += " ";

            answer2 += Answer2;
            Console.Write(answer2);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("   ║   ");

            if (time2 > new TimeSpan(0, 0, 0, 1))
                Console.ForegroundColor = ConsoleColor.Red;
            else if (time2 > new TimeSpan(0, 0, 0, 0, 100))
                Console.ForegroundColor = ConsoleColor.Yellow;
            else
                Console.ForegroundColor = ConsoleColor.Green;

            Console.Write(time2.ToString("mm\\:ss\\.ffffff"));

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("   ║");
            Console.WriteLine();

            #endregion

            #region Total

            Console.Write("║         ║          ║                      ║   ");

            if (time > new TimeSpan(0, 0, 0, 1))
                Console.ForegroundColor = ConsoleColor.Red;
            else if (time > new TimeSpan(0, 0, 0, 0, 100))
                Console.ForegroundColor = ConsoleColor.Yellow;
            else
                Console.ForegroundColor = ConsoleColor.Green;

            Console.Write(time.ToString("mm\\:ss\\.ffffff"));

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("   ║");
            Console.WriteLine();

            #endregion
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

                Console.Clear();
                Console.WriteLine($"Visualization for {year}.{day}.1");
                Thread.Sleep(1000);

                VisualizePart1();
            }

            if (part == 0 || part == 2)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;

                Console.Clear();
                Console.WriteLine($"Visualization for {year}.{day}.2");
                Thread.Sleep(1000);

                VisualizePart2();
            }

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Cyan;
        }

        public virtual void VisualizePart1() { }

        public virtual void VisualizePart2() { }
    }
}