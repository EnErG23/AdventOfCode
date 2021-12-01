﻿using AdventOfCode.Helpers;
using AdventOfCode.Y2020.Models;

namespace AdventOfCode.Y2020.Days
{
    public static class Day13
    {
        static int day = 13;
        static List<string>? inputs;

        public static string? Answer1 { get; set; }
        public static string? Answer2 { get; set; }

        public static void Run(int part, bool test)
        {
            inputs = InputManager.GetInputAsStrings(day, test);

            var start = DateTime.Now;

            string part1 = "";
            string part2 = "";

            switch (part)
            {
                case 1:
                    part1 = Part1();
                    break;
                case 2:
                    part2 = Part2();
                    break;
                default:
                    part1 = Part1();
                    part2 = Part2();
                    break;
            }

            var ms = Math.Round((DateTime.Now - start).TotalMilliseconds);

            Console.WriteLine($"Day {day} ({ms}ms):");
            if (part1 != "") Console.WriteLine($"    {part1}");
            if (part2 != "") Console.WriteLine($"    {part2}");
        }

        static string Part1()
        {
            long result = 0;

            var start = DateTime.Now;

            #region Solution

            var timeStamp = Convert.ToInt32(inputs[0]);
            var buses = inputs[1].Replace("x,", "").Split(',').Select(i => Convert.ToInt32(i));

            var iterateTimeStamp = timeStamp;

            while (true)
            {
                foreach (var bus in buses)
                {
                    if (iterateTimeStamp % bus == 0)
                    {
                        result = (iterateTimeStamp - timeStamp) * bus;
                        break;
                    }
                }
                if (result > 0) break;
                iterateTimeStamp++;
            }

            #endregion

            var ms = Math.Round((DateTime.Now - start).TotalMilliseconds);

            if (result > 0) Answer1 = result.ToString();
            return $"Part 1 ({ms}ms): {result} ";
        }

        static string Part2()
        {
            long result = 0;

            var start = DateTime.Now;

            #region Solution

            List<Bus> buses = inputs[1].Split(',')
                              .Select(i => new Bus { ID = Convert.ToInt32(i.Replace("x", "0")), Pos = inputs[1].Split(',').ToList().IndexOf(i) })
                              .Where(b => b.ID > 0)
                              .ToList();

            long[] n = buses.Select(b => b.ID).ToArray();
            long[] a = buses.Select(b => b.ID - b.Pos < 0 ? b.ID - (b.Pos % b.ID) : b.ID - b.Pos).ToArray();

            result = Solve(n, a);

            #endregion

            var ms = Math.Round((DateTime.Now - start).TotalMilliseconds);

            if (result > 0) Answer2 = result.ToString();
            return $"Part 2 ({ms}ms): {result} ";
        }

        public static long Solve(long[] n, long[] a)
        {
            long prod = n.Aggregate(1, (long i, long j) => i * j);
            long p;
            long sm = 0;

            for (long i = 0; i < n.Length; i++)
            {
                p = prod / n[i];
                sm += a[i] * ModularMultiplicativeInverse(p, n[i]) * p;
            }

            return sm % prod;
        }

        private static long ModularMultiplicativeInverse(long a, long mod)
        {
            long b = a % mod;

            for (long x = 1; x < mod; x++)
            {
                if ((b * x) % mod == 1)
                {
                    return x;
                }
            }
            return 1;
        }
    }
}