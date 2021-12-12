using AdventOfCode.Models;

namespace AdventOfCode.Y2020.Days
{
    public class Day10 : Day
    {
        private List<int>? inputs;

        public Day10(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
        {
            inputs = Inputs.Select(i => int.Parse(i)).ToList();
            inputs.Add(0);
            inputs = inputs.OrderBy(i => i).ToList();
            inputs.Add(inputs[inputs.Count() - 1] + 3);

            long result = 0;

            var ones = 0;
            var threes = 0;

            for (int i = 1; i < inputs.Count; i++)
            {
                var difference = inputs[i] - inputs[i - 1];

                if (difference == 1)
                    ones++;
                else if (difference == 3)
                    threes++;
            }

            result = ones * threes;

            return result.ToString();
        }

        public override string RunPart2()
        {
            if (inputs is null)
            {
                inputs = Inputs.Select(i => int.Parse(i)).ToList();
                inputs.Add(0);
                inputs = inputs.OrderBy(i => i).ToList();
                inputs.Add(inputs[inputs.Count() - 1] + 3);
            }

            long result = 1;

            var count = 0;

            for (int i = 1; i < inputs.Count() - 1; i++)
            {
                if (inputs[i + 1] - inputs[i - 1] < 4)
                    count++;
                else
                {
                    if (count > 0)
                    {
                        var fact = count;

                        for (var j = count - 1; j >= 1; j--)
                            fact *= j;

                        if (inputs[i] - inputs[i - 1 - count] < 4)
                            fact++;

                        if (count > 1)
                            fact++;

                        result *= fact;

                        count = 0;
                    }
                }
            }

            return result.ToString();
        }
    }
}