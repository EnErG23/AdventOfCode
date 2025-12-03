using AdventOfCode.Models;

namespace AdventOfCode.Y2025.Days
{
    public class Day03 : Day
    {
        public Day03(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
        {
            long result = 0;

            foreach (var input in Inputs)
            {
                var joltage1 = input.Substring(0, input.Length - 1)
                    .Max(i => int.Parse(i.ToString()))
                    .ToString();

                var joltage2 = input[(input.IndexOf(joltage1) + 1)..]
                    .Max(i => int.Parse(i.ToString()))
                    .ToString();

                result += int.Parse(joltage1 + joltage2);
            }

            return result.ToString();
        }

        public override string RunPart2()
        {
            long result = 0;

            foreach (var input in Inputs)
            {
                var bankJoltage = string.Empty;

                var inputS = input.ToString();

                while (bankJoltage.Length < 12)
                {
                    var joltage = inputS[..(inputS.Length - (11 - bankJoltage.Length))]
                        .Max(i => int.Parse(i.ToString()))
                        .ToString();

                    inputS = inputS.Substring(inputS.IndexOf(joltage) + 1);

                    bankJoltage += joltage;
                }

                result += long.Parse(bankJoltage);
            }

            return result.ToString();
        }
    }
}