using AdventOfCode.Models;

namespace AdventOfCode.Y2024.Days
{
    public class Day07 : Day
    {        
        public Day07(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
        {
            decimal result = 0;

            foreach (var input in Inputs)
            {
                decimal calcResult = decimal.Parse(input.Split(":")[0]);
                List<decimal> values = input.Split(":")[1].Trim().Split(" ").Select(v => decimal.Parse(v)).ToList();

                result += CheckCalculations(calcResult, values.First(), values.GetRange(1, values.Count() - 1), false) > 0 ? calcResult : 0;
            }

            return result.ToString();
        }

        public override string RunPart2()
        {
            decimal result = 0;

            foreach (var input in Inputs)
            {
                decimal calcResult = decimal.Parse(input.Split(":")[0]);
                List<decimal> values = input.Split(":")[1].Trim().Split(" ").Select(v => decimal.Parse(v)).ToList();

                result += CheckCalculations(calcResult, values.First(), values.GetRange(1, values.Count() - 1), true) > 0 ? calcResult : 0;
            }

            return result.ToString();
        }

        private decimal CheckCalculations(decimal result, decimal intermediateResult, List<decimal> values, bool useConcat)
        {
            decimal options = 0;

            if (values.Count() == 0)
                return result == intermediateResult ? 1 : 0;
            else
            {
                decimal value = values.First();

                var newValues = values.ToList();
                newValues.RemoveAt(0);

                options += CheckCalculations(result, intermediateResult + value, newValues, useConcat);
                options += CheckCalculations(result, intermediateResult * value, newValues, useConcat);

                if (useConcat)
                    options += CheckCalculations(result, decimal.Parse(intermediateResult.ToString() + value.ToString()), newValues, useConcat);
            }

            return options;
        }
    }
}