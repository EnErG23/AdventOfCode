using AdventOfCode.Models;
using System;
using System.Diagnostics.Tracing;
using System.Xml.Schema;

namespace AdventOfCode.Y2018.Days
{
    public class Day02 : Day
    {
        public Day02(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
        {
            var twos = 0;
            var threes = 0;

            foreach (var input in Inputs)
            {
                bool two = false;
                bool three = false;

                foreach (var letter in input.Distinct())
                {
                    if (two && three)
                        break;

                    if (!two)
                        two = input.Count(i => i == letter) == 2;

                    if (!three)
                        three = input.Count(i => i == letter) == 3;
                }

                twos += two ? 1 : 0;
                threes += three ? 1 : 0;
            }

            return $"{twos * threes}";
        }

        public override string RunPart2()
        {
            var inputs = Test ? new List<string>() { "abcde", "fghij", "klmno", "pqrst", "fguij", "axcye", "wvxyz" } : Inputs;

            foreach (var input in inputs)
            {
                foreach (var input2 in inputs.Where(t => t != input))
                {
                    int diff = 0;
                    char diffC = '0';

                    for (int i = 0; i < input.Length; i++)
                    {
                        if (input[i] != input2[i])
                        {
                            diffC = input[i];
                            diff++;
                        }

                        if (diff > 1)
                            break;
                    }

                    if (diff == 1)
                        return input.Replace(diffC.ToString(), "");
                }
            }

            return "undefined";
        }
    }
}