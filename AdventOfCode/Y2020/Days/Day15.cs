using AdventOfCode.Models;
using System.Diagnostics;

namespace AdventOfCode.Y2020.Days
{
    public class Day15 : Day
    {
        public Day15(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
        {
            long result = 0;

            List<long> spokenNumbers = new List<long>();

            spokenNumbers.AddRange(Inputs[0].Split(',').Select(i => Convert.ToInt64(i)).ToList());

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

			return result.ToString();
        }

        public override string RunPart2()
        {
            long result = 0;

            var spokenNumbers = Inputs[0].Split(',').Select(i => Convert.ToInt64(i)).ToArray();

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

			return result.ToString();
        }
    }
}