using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Y2021.Days
{
    public static class Day06
    {
        static int day = 6;
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

            var fishes = inputs[0].Split(",").Select(i => int.Parse(i)).ToList();

            var daysPassed = 0;
            var daysToPass = 80;

            while (daysPassed < daysToPass - 1)
            {
                //Console.WriteLine(String.Join(", ", fishes));

                var smallestTime = fishes.Min();
                //Console.WriteLine(smallestTime);

                var newFishes = fishes.Count(f => f - smallestTime == 0);
                //Console.WriteLine(newFishes);

                fishes = fishes.Select(f => f - smallestTime == 0 ? 7 : f - smallestTime).ToList();

                for (int n = 0; n < newFishes; n++)
                {
                    fishes.Add(9);
                }

                daysPassed += smallestTime;
                //Console.WriteLine(daysPassed);
            }

            Console.WriteLine(String.Join(", ", fishes));
            result = fishes.Count;

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

            var fishes = inputs[0].Split(",").Select(i => int.Parse(i)).ToList();

            var daysPassed = 1;
            var daysToPass = 256;            
            result = fishes.Count;

            var addAfterXDays = new long[9];

            for (int i = 0; i < fishes.Count; i++)
                addAfterXDays[fishes[i]]++;

            Console.WriteLine(String.Join(",", addAfterXDays));

            while (daysPassed <= daysToPass)
            {

                var fishesToAdd = addAfterXDays[0];
                result += fishesToAdd;

                for (int i = 0; i < 8; i++)
                    addAfterXDays[i] = addAfterXDays[i + 1];

                addAfterXDays[8] = 0;

                addAfterXDays[6] += fishesToAdd;
                addAfterXDays[8] += fishesToAdd;

                Console.WriteLine($"After {daysPassed} days:");
                Console.WriteLine(String.Join(",", addAfterXDays));

                daysPassed++;
            }

            #endregion

            var ms = Math.Round((DateTime.Now - start).TotalMilliseconds);

            if (result > 0) Answer2 = result.ToString();
            return $"Part 2 ({ms}ms): {result} ";
        }
    }
}