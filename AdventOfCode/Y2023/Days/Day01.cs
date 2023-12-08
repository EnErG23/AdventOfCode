using AdventOfCode.Models;

namespace AdventOfCode.Y2023.Days
{
    public class Day01 : Day
    {
        public Day01(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1() => Inputs.Sum(i => int.Parse($"{i.First(c => char.IsDigit(c))}{i.Last(c => char.IsDigit(c))}")).ToString();

        public override string RunPart2()
        {
            var inputs = Test ? new List<string> { "two1nine", "eightwothree", "abcone2threexyz", "xtwone3four", "4nineeightseven2", "zoneight234", "7pqrstsixteen" } : Inputs;
            var numbers = new List<string> { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

            var result = 0;

            foreach (var input in inputs)
            {
                char firstDigit = 'x';
                char lastDigit = 'x';

                for (int i = 0; i < input.Length; i++)
                {
                    if (firstDigit == 'x')
                        if (char.IsDigit(input[i]))
                            firstDigit = input[i];
                        else
                            for (int j = 1; j <= numbers.Count; j++)
                                if (input.Substring(i, Math.Min(numbers[j - 1].Length, input.Length - i)) == numbers[j - 1])
                                    firstDigit = char.Parse($"{j}");

                    if (lastDigit == 'x')
                        if (char.IsDigit(input[input.Length - i - 1]))
                            lastDigit = input[input.Length - i - 1];
                        else
                            for (int j = 1; j <= numbers.Count; j++)
                                if (input.Substring(input.Length - i - 1, Math.Min(numbers[j - 1].Length, input.Length - (input.Length - i) + 1)) == numbers[j - 1])
                                    lastDigit = char.Parse($"{j}");

                    if (firstDigit != 'x' && lastDigit != 'x')
                        break;
                }

                result += int.Parse($"{firstDigit}{lastDigit}");
            }

            return result.ToString();
        }
    }
}