using AdventOfCode.Models;

namespace AdventOfCode.Y2020.Days
{
    public class Day10 : Day
    {        
        static List<int>? inputs;

        public Day10(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
        {
            inputs = Inputs.Select(i => int.Parse(i)).ToList();
            inputs.Add(0);
            inputs = inputs.OrderBy(i => i).ToList();
            inputs.Add(inputs[inputs.Count() - 1] + 3);

            long result = 0;

            var a = 0;
            var ones = 0;
            var threes = 0;

            foreach (var intInput in inputs.Skip(1))
            {
                var difference = intInput - inputs[a];

                if (difference == 1) ones++;
                else if (difference == 3) threes++;

                a++;
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

            long result = 0;

            result = 1;

            var count = 0;
            var a = 0;

            foreach (var l in inputs.Skip(1).Take(Inputs.Count() - 2))
            {
                if (inputs[a + 2] - inputs[a] < 4)
                {
                    count++;
                }
                else
                {
                    if (count > 0)
                    {
                        var fact = count;

                        for (var i = count - 1; i >= 1; i--)
                        {
                            fact = fact * i;
                        }

                        if (l - inputs[a - count] < 4)
                        {
                            fact++;
                        }

                        if (count > 1)
                        {
                            fact++;
                        }

                        result *= fact;

                        count = 0;
                    }
                }

                a++;
            }

            return result.ToString();
        }
    }
}