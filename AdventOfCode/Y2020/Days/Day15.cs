using AdventOfCode.Models;
using System.Diagnostics;

namespace AdventOfCode.Y2020.Days
{
    public class Day15 : Day
    {
        public Day15(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
        {
            var spokenNumbers = Inputs[0].Split(',').Select(i => Convert.ToInt64(i)).ToList();

            for (int i = spokenNumbers.Count(); i < 2020; i++)
            {
                var number = 0;

                if (spokenNumbers.Count(s => s == spokenNumbers[i - 1]) > 1)
                    number = i - 1 - spokenNumbers.Take(spokenNumbers.Count() - 1).ToList().LastIndexOf(spokenNumbers[i - 1]);

                spokenNumbers.Add(number);
            }

            return spokenNumbers.Last().ToString();
        }

        public override string RunPart2()
        {
            var spokenNumbers = Inputs[0].Split(',').Select(i => Convert.ToInt64(i)).ToList();

            //init array length 30m
            var rounds = 30000000;
            var numbers = new long[rounds];

            //enter initial values
            for (int i = 0; i < spokenNumbers.Count() - 1; i++)
                numbers[spokenNumbers[i]] = i + 1;

            //start with last value
            var pos = spokenNumbers.Count;
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

            return numbers.ToList().IndexOf(rounds).ToString();
        }
    }
}