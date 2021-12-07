using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Models
{
    public abstract class Day
    {
        private int day;
        private string answer1 = "Unsolved";
        private string answer2 = "Unsolved";
        private double time = 0;
        private double time1 = 0;
        private double time2 = 0;

        public List<string> Inputs { get; set; }

        public Day(int day, bool test)
        {
            this.day = day;
            Inputs = InputManager.GetInputAsStrings(day, test);
        }

        public void Run(int? part)
        {
            Stopwatch sw = Stopwatch.StartNew();

            if (part == 0 || part == 1)
            {
                Stopwatch sw1 = Stopwatch.StartNew();
                answer1 = RunPart1();
                sw1.Stop();
                time1 = sw1.Elapsed.TotalMilliseconds;
            }

            if (part == 0 || part == 2)
            {
                Stopwatch sw2 = Stopwatch.StartNew();
                answer2 = RunPart2();
                sw2.Stop();
                time2 = sw2.Elapsed.TotalMilliseconds;
            }

            sw.Stop();
            time = sw.Elapsed.TotalMilliseconds;

            Print();
        }

        public void Print()
        {
            Console.WriteLine($"Day {day} ({time}ms):");
            Console.WriteLine($"    Part 1 ({time1}ms): {answer1}");
            Console.WriteLine($"    Part 2 ({time2}ms): {answer2}");
        }

        public abstract string RunPart1();

        public abstract string RunPart2();

        public void Visualize(int? part)
        {
            Console.Clear();

            if (part == 0 || part == 1)
                VisualizePart1();

            if (part == 0 || part == 2)
                VisualizePart2();

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Cyan;
        }

        public virtual void VisualizePart1() { }

        public virtual void VisualizePart2() { }
    }
}