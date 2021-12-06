using AdventOfCode.Helpers;
using System.Diagnostics;

namespace AdventOfCode.Y2020.Days
{
    public static class Day15
    {
        static int day = 15;
        static List<string>? inputs;

        public static string? Answer1 { get; set; }
        public static string? Answer2 { get; set; }

        public static void Run(int part, bool test)
        {
            Stopwatch sw = Stopwatch.StartNew();

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

            sw.Stop();
			var ms = sw.Elapsed.TotalMilliseconds;

            Console.WriteLine($"Day {day} ({ms}ms):");
            if (part1 != "") Console.WriteLine($"    {part1}");
            if (part2 != "") Console.WriteLine($"    {part2}");
        }

        static string Part1()
        {
            Stopwatch sw = Stopwatch.StartNew();

            long result = 0;

            #region Solution

            List<long> spokenNumbers = new List<long>();

            spokenNumbers.AddRange(inputs[0].Split(',').Select(i => Convert.ToInt64(i)).ToList());

            for (int i = spokenNumbers.Count(); i < 2020; i++)
            {
                var number = 0;

                if (spokenNumbers.Count(s => s == spokenNumbers[i - 1]) > 1)
                {
                    number = i - 1 - spokenNumbers.Take(spokenNumbers.Count() - 1).ToList().LastIndexOf(spokenNumbers[i - 1]);
                }

                //if (i % 10000 == 0) Console.WriteLine($"{i})    Prev: {spokenNumbers[i - 1]}, Pos: {spokenNumbers.Take(spokenNumbers.Count() - 1).ToList().LastIndexOf(spokenNumbers[i - 1])} => {number}");
                spokenNumbers.Add(number);
            }

            result = spokenNumbers.Last();

            #endregion

            sw.Stop();
			var ms = sw.Elapsed.TotalMilliseconds;

            if (result > 0) Answer1 = result.ToString();
            return $"Part 1 ({ms}ms): {result} ";
        }

        static string Part2()
        {
            Stopwatch sw = Stopwatch.StartNew();

            long result = 0;

            #region Solution

            var spokenNumbers = inputs[0].Split(',').Select(i => Convert.ToInt64(i)).ToArray();

            //init array length 30m
            var rounds = 30000000;
            var numbers = new long[rounds];

            //enter initial values
            for (int i = 0; i < spokenNumbers.Count() - 1; i++)
            {
                numbers[spokenNumbers[i]] = i + 1;
                //Console.WriteLine($"{spokenNumbers[i]} => numbers[{spokenNumbers[i]}] = {i+1}");
            }

            //start with last value
            var pos = 6;
            var init = spokenNumbers.Last();

            //update array
            while (pos <= rounds)
            {
                long initNew = 0;

                if (numbers[init] != 0)
                    initNew = pos - numbers[init];

                numbers[init] = pos;
                init = initNew;
                pos++;
            }

            result = numbers.ToList().IndexOf(rounds);

            #endregion

            sw.Stop();
			var ms = sw.Elapsed.TotalMilliseconds;

            if (result > 0) Answer2 = result.ToString();
            return $"Part 2 ({ms}ms): {result} ";
        }
    }
}