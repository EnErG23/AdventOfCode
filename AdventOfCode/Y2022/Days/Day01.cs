using AdventOfCode.Models;

namespace AdventOfCode.Y2022.Days
{
    public class Day01 : Day
    {
        private List<int>? elveCalories;

        public Day01(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
        {
            elveCalories = new List<int>();

            int calories = 0;

            foreach (var input in Inputs)
            {
                if (input == "")
                {
                    elveCalories.Add(calories);
                    calories = 0;
                }
                else
                {
                    calories += int.Parse(input);
                }
            }
            elveCalories.Add(calories);

            return elveCalories.Max(e => e).ToString();
        }

        public override string RunPart2()
        {
            if (elveCalories == null)
            {
                elveCalories = new List<int>();

                int calories = 0;

                foreach (var input in Inputs)
                {
                    if (input == "")
                    {
                        elveCalories.Add(calories);
                        calories = 0;
                    }
                    else
                    {
                        calories += int.Parse(input);
                    }
                }
                elveCalories.Add(calories);
            }

            return elveCalories.OrderByDescending(e => e).Take(3).Sum(e => e).ToString();
        }
    }
}