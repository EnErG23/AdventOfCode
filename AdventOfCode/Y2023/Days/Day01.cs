using AdventOfCode.Models;
using System.ComponentModel.DataAnnotations;
using System.Threading;

namespace AdventOfCode.Y2023.Days
{
    public class Day01 : Day
    {
        public Day01(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
        {
            var result = 0;

            foreach (var input in Inputs)
            {
                char firstNumber = 'x';
                char lastNumber = 'x';

                foreach (var letter in input)
                {
                    if (Char.IsDigit(letter))
                    {
                        if (firstNumber == 'x')
                        {
                            firstNumber = letter;
                        }
                        else
                        {
                            lastNumber = letter;
                        }
                    }
                }

                if (lastNumber == 'x')
                    lastNumber = firstNumber;

                result += int.Parse($"{firstNumber}{lastNumber}");
            }

            return result.ToString();
        }

        public override string RunPart2()
        {
            var inputs = Test ? new List<string> { "two1nine", "eightwothree", "abcone2threexyz", "xtwone3four", "4nineeightseven2", "zoneight234", "7pqrstsixteen" } : Inputs;
            var numbers = new List<string> { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

            var result = 0;

            foreach (var input in inputs)
            {
                char firstNumber = 'x';
                char lastNumber = 'x';

                for (int i = 0; i < input.Length; i++)
                {
                    var letter = input[i];

                    if (Char.IsDigit(letter))
                    {
                        if (firstNumber == 'x')
                        {
                            firstNumber = letter;
                        }
                        else
                        {
                            lastNumber = letter;
                        }
                    }
                    else
                    {
                        for (int j = 0; j < numbers.Count; j++)
                        {
                            var number = numbers[j];

                            if (input.Substring(i, Math.Min(number.Length, input.Length - i)) == number)
                            {
                                if (firstNumber == 'x')
                                {
                                    firstNumber = char.Parse($"{j + 1}");
                                }
                                else
                                {
                                    lastNumber = char.Parse($"{j + 1}");
                                }
                            }
                        }
                    }
                }

                if (lastNumber == 'x')
                    lastNumber = firstNumber;

                result += int.Parse($"{firstNumber}{lastNumber}");
            }

            return result.ToString();
        }
    }
}