using AdventOfCode.Helpers;
using System.Diagnostics;

namespace AdventOfCode.Y2020.Days
{
    public class Day18
    {
        static readonly int day = 18;
        static List<string>? inputs;

        public static string? Answer1 { get; set; }
        public static string? Answer2 { get; set; }

        public static void Run(int part, bool test)
        {
            Stopwatch sw = Stopwatch.StartNew();

            inputs = InputManager.GetInputAsStrings(day, test);

            string part1 = "";
            string part2 = "";

            if (part == 1)
                part1 = Part1();
            else if (part == 2)
                part2 = Part2();
            else
            {
                part1 = Part1();
                part2 = Part2();
            }

            sw.Stop();
			var ms = sw.Elapsed.TotalMilliseconds;

            Console.WriteLine($"Day {day} ({ms}ms):");
            if (part1 != "") Console.WriteLine($"    {part1}");
            if (part2 != "") Console.WriteLine($"    {part2}");
        }

        private static string Part1()
        {
            Stopwatch sw = Stopwatch.StartNew();

            long result = 0;

            #region Solution

            foreach (var input in inputs)
            {
                result += Solve(input.Replace(" ", ""));
            }

            #endregion

            sw.Stop();
			var ms = sw.Elapsed.TotalMilliseconds;

            if (result > 0) Answer1 = result.ToString();
            return $"Part 1 ({ms}ms): {result} ";
        }

        private static string Part2()
        {
            Stopwatch sw = Stopwatch.StartNew();

            long result = 0;

            #region Solution

            foreach (var input in inputs)
            {
                result += Convert.ToInt64(SolvePlusFirst(input.Replace(" ", "")));
            }

            #endregion

            sw.Stop();
			var ms = sw.Elapsed.TotalMilliseconds;

            if (result > 0) Answer2 = result.ToString();
            return $"Part 2 ({ms}ms): {result} ";
        }

        static long Solve(string input)
        {
            long result = 0;

            while (input.Length > 1)
            {
                var firstChar = input.Substring(0, 1);
                var secondChar = input.Substring(1, 1);

                switch (firstChar)
                {
                    case "(":
                        result = Solve(input.Substring(1, GetEndingBracket(input.Substring(1))));
                        input = input.Substring(GetEndingBracket(input.Substring(1)) + 2);
                        break;
                    case ")":
                        input = input.Substring(1);
                        break;
                    case "+":
                        if (secondChar == "(")
                        {
                            result += Solve(input.Substring(2, GetEndingBracket(input.Substring(2))));
                            input = input.Substring(GetEndingBracket(input.Substring(2)) + 2);
                        }
                        else
                        {
                            result += Convert.ToInt64(secondChar);
                            input = input.Substring(2);
                        }
                        break;
                    case "*":
                        if (secondChar == "(")
                        {
                            result *= Solve(input.Substring(2, GetEndingBracket(input.Substring(2))));
                            input = input.Substring(GetEndingBracket(input.Substring(2)) + 2);
                        }
                        else
                        {
                            result *= Convert.ToInt64(secondChar);
                            input = input.Substring(2);
                        }
                        break;
                    default:
                        result = Convert.ToInt64(firstChar);
                        input = input.Substring(1);
                        break;
                }
            }

            return result;
        }

        static string SolvePlusFirst(string input)
        {
            while (input.Contains("+"))
            {
                var firstPlus = input.IndexOf("+");
                var charBefore = input.Substring(firstPlus - 1, 1);
                var charAfter = input.Substring(firstPlus + 1, 1);

                if (charBefore != ")" && charAfter != "(")
                {
                    var startIndex = firstPlus - 1;
                    while (true)
                    {
                        if (startIndex == 0)
                            break;

                        var charCheck = input.Substring(startIndex - 1, 1);
                        if (charCheck == "+" || charCheck == "*" || charCheck == "(" || charCheck == ")")
                            break;

                        startIndex--;
                    }
                    var numberBefore = input.Substring(startIndex, firstPlus - startIndex);

                    var endIndex = firstPlus + 1;
                    while (true)
                    {
                        if (endIndex == input.Length - 1)
                            break;

                        var charCheck = input.Substring(endIndex + 1, 1);
                        if (charCheck == "+" || charCheck == "*" || charCheck == "(" || charCheck == ")")
                            break;

                        endIndex++;
                    }
                    var numberAfter = input.Substring(firstPlus + 1, endIndex - firstPlus);

                    long newNumber = Convert.ToInt64(numberBefore) + Convert.ToInt64(numberAfter);

                    if (startIndex == 0)
                    {
                        if (endIndex + 1 > input.Length)
                        {
                            input = newNumber.ToString();
                        }
                        else
                        {
                            input = newNumber + input.Substring(endIndex + 1);
                        }
                    }
                    else
                    {
                        if (endIndex + 1 > input.Length)
                        {
                            input = input.Substring(0, startIndex) + newNumber.ToString();
                        }
                        else
                        {
                            input = input.Substring(0, startIndex) + newNumber + input.Substring(endIndex + 1);
                        }
                    }
                }
                else if (charBefore == ")")
                {
                    var startingBracket = GetStartingBracket(input.Substring(0, firstPlus - 1));
                    input = input.Substring(0, startingBracket - 1) + SolvePlusFirst(input.Substring(startingBracket, firstPlus - startingBracket - 1)) + input.Substring(firstPlus);
                }
                else if (charAfter == "(")
                {
                    var endingBracket = GetEndingBracket(input.Substring(firstPlus + 2));
                    if (firstPlus + endingBracket + 3 > input.Length)
                    {
                        input = input.Substring(0, firstPlus + 1) + SolvePlusFirst(input.Substring(firstPlus + 2, endingBracket));
                    }
                    else
                    {
                        input = input.Substring(0, firstPlus + 1) + SolvePlusFirst(input.Substring(firstPlus + 2, endingBracket)) + input.Substring(firstPlus + endingBracket + 3);
                    }
                }
            }

            input = input.Replace("(", "").Replace(")", "");

            while (input.Contains("*"))
            {
                var firstPlus = input.IndexOf("*");

                var numberBefore = input.Substring(0, firstPlus);

                var endIndex = firstPlus + 1;
                while (true)
                {
                    if (endIndex == input.Length - 1)
                        break;

                    if (input.Substring(endIndex + 1, 1) == "*")
                        break;

                    endIndex++;
                }
                var numberAfter = input.Substring(firstPlus + 1, endIndex - firstPlus);

                long newNumber = Convert.ToInt64(numberBefore) * Convert.ToInt64(numberAfter);

                if (input.Count(i => i == '*') > 1)
                {
                    input = newNumber + input.Substring(endIndex + 1);
                }
                else
                {
                    input = newNumber.ToString();
                }
            }

            return input;
        }

        static int GetEndingBracket(string input)
        {
            int result = 0;

            int skipBracket = 0;

            while (input.Length > 1)
            {
                var firstChar = input.Substring(0, 1);

                switch (firstChar)
                {
                    case "(":
                        skipBracket++;
                        break;
                    case ")":
                        if (skipBracket == 0)
                        {
                            return result;
                        }
                        skipBracket--;
                        break;
                    default:
                        break;
                }

                input = input.Substring(1);
                result++;
            }

            return result;
        }

        static int GetStartingBracket(string input)
        {
            int result = input.Length;

            int skipBracket = 0;

            while (input.Length > 1)
            {
                var lastChar = input.Substring(input.Length - 1, 1);

                switch (lastChar)
                {
                    case ")":
                        skipBracket++;
                        break;
                    case "(":
                        if (skipBracket == 0)
                        {
                            return result;
                        }
                        skipBracket--;
                        break;
                    default:
                        break;
                }

                input = input.Substring(0, input.Length - 1);
                result--;
            }

            return result;
        }
    }
}