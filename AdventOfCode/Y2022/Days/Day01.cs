using AdventOfCode.Models;

namespace AdventOfCode.Y2022.Days
{
    public class Day01 : Day
    {
        private List<int>? elfCalories;

        public Day01(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
        {
            elfCalories = new List<int>();

            int calories = 0;

            foreach (var input in Inputs)
            {
                if (input == "")
                {
                    elfCalories.Add(calories);
                    calories = 0;
                }
                else
                {
                    calories += int.Parse(input);
                }
            }
            elfCalories.Add(calories);

            return elfCalories.Max().ToString();
        }

        public override string RunPart2()
        {
            if (elfCalories == null)
            {
                elfCalories = new List<int>();

                int calories = 0;

                foreach (var input in Inputs)
                {
                    if (input == "")
                    {
                        elfCalories.Add(calories);
                        calories = 0;
                    }
                    else
                    {
                        calories += int.Parse(input);
                    }
                }
                elfCalories.Add(calories);
            }

            return elfCalories.OrderByDescending(e => e).Take(3).Sum().ToString();
        }
    }
}