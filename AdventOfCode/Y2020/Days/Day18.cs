﻿using AdventOfCode.Models;
using System.Diagnostics;

namespace AdventOfCode.Y2020.Days
{
    public class Day18 : Day
    {
        public Day18(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
        {
            long result = 0;

            foreach (var input in Inputs)
            {
                result += Solve(input.Replace(" ", ""));
            }

			return result.ToString();
        }

        public override string RunPart2()
        {
            long result = 0;

            foreach (var input in Inputs)
            {
                result += Convert.ToInt64(SolvePlusFirst(input.Replace(" ", "")));
            }

			return result.ToString();
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