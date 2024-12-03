using AdventOfCode.Models;

namespace AdventOfCode.Y2024.Days
{
    public class Day01 : Day
    {
        public Day01(int year, int day, bool test) : base(year, day, test) => Inputs = Inputs.Select(i => i.Replace("   ", " ")).ToList();

        public override string RunPart1()
        {
            var result = 0;

            var left = new List<int>();
            var right = new List<int>();

            foreach (var input in Inputs)
            {
                left.Add(int.Parse(input.Split(" ")[0]));
                right.Add(int.Parse(input.Split(" ")[1]));
            }

            left = left.OrderBy(i => i).ToList();
            right = right.OrderBy(i => i).ToList();

            for(int i = 0; i < left.Count; i++)
            {
                result += Math.Abs(left[i] - right[i]);
            }

            return result.ToString();
        }

        public override string RunPart2()
        {
            var result = 0;

            var left = new List<int>();
            var right = new List<int>();

            foreach (var input in Inputs)
            {
                left.Add(int.Parse(input.Split(" ")[0]));
                right.Add(int.Parse(input.Split(" ")[1]));
            }

            foreach (var l in left)
            {
                result += l * right.Count(r => r == l);
            }


            return result.ToString();
        }
    }
}