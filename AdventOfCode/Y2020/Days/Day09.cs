using AdventOfCode.Models;
using System.Diagnostics;

namespace AdventOfCode.Y2020.Days
{
    public class Day09 : Day
    {
        private List<long>? inputs;
        private int p;

        public Day09(int year, int day, bool test) : base(year, day, test)
        {
            p = test ? 5 : 25;
        }

        public override string RunPart1()
        {
            inputs = Inputs.Select(i => long.Parse(i)).ToList();

            long result = 0;

            var i = p;

            foreach (var input in inputs.Skip(p))
            {
                var preamble = inputs.GetRange(i - p, p);
                var found = false;

                foreach (var pre1 in preamble)
                {
                    foreach (var pre2 in preamble.Where(pr => pr != pre1))
                    {
                        if (pre1 + pre2 == input)
                            found = true;

                        if (found) break;
                    }
                    if (found) break;
                }

                if (!found)
                {
                    result = input;
                    break;
                }

                i++;
            }

            return result.ToString();
        }

        public override string RunPart2()
        {
            if (inputs is null)
                inputs = Inputs.Select(i => long.Parse(i)).ToList();

            long result = 0;

            long firstError = 0;
            var i = p;

            foreach (var input in inputs.Skip(p))
            {
                var preamble = inputs.GetRange(i - p, p);
                var found = false;

                foreach (var pre1 in preamble)
                {
                    foreach (var pre2 in preamble.Where(pr => pr != pre1))
                    {
                        if (pre1 + pre2 == input)
                        {
                            found = true;
                        }
                        if (found) break;
                    }
                    if (found) break;
                }
                if (!found)
                {
                    firstError = input;
                    break;
                }
                i++;
            }

            long subTotal = 0;
            var a = 0;
            var length = 1;

            foreach (var input1 in inputs)
            {
                subTotal = 0;
                a = inputs.IndexOf(input1);
                length = 1;

                foreach (var input2 in inputs.GetRange(a, Inputs.Count() - a))
                {
                    subTotal += input2;

                    if (subTotal > firstError)
                    {
                        break;
                    }

                    if (subTotal == firstError)
                    {
                        result = inputs.GetRange(a, length).OrderBy(inp => inp).First() + inputs.GetRange(a, length).OrderByDescending(inp => inp).First();
                        break;
                    }

                    length++;
                }

                if (result != 0)
                {
                    break;
                }

            }

            return result.ToString();
        }
    }
}