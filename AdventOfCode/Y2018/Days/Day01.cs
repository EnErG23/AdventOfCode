using AdventOfCode.Models;
using System.Diagnostics.Tracing;
using System.Xml.Schema;

namespace AdventOfCode.Y2018.Days
{
    public class Day01 : Day
    {
        private List<int>? inputs;

        public Day01(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
        {
            inputs = Inputs.Select(i => int.Parse(i)).ToList();

            return inputs.Sum().ToString();
        }

        public override string RunPart2()
        {
            inputs = Inputs.Select(i => int.Parse(i)).ToList();

            var freqs = new List<int>();

            var totSum = 0;

            while (true)
            {
                for (int i = 1; i <= inputs.Count; i++)
                {
                    var sum = inputs.GetRange(0, i).Sum() + totSum;                    

                    if (freqs.Contains(sum))
                        return sum.ToString();

                    freqs.Add(sum);
                }

                totSum += inputs.Sum();
            }
        }
    }
}