using AdventOfCode.Models;

namespace AdventOfCode.Y2022.Days
{
    public class Day01 : Day
    {
        List<int> ElveCalories = new List<int>();

        public Day01(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
        {
            int calories = 0;

            foreach (var input in Inputs)
            {
                if (input == "")
                {
                    ElveCalories.Add(calories);
                    calories = 0;
                }
                else
                {
                    calories += int.Parse(input);
                }
            }

            return ElveCalories.Max(e => e).ToString();
        }

        public override string RunPart2()
        {
            return ElveCalories.OrderByDescending(e => e).Take(3).Sum(e => e).ToString();
        }
    }
}