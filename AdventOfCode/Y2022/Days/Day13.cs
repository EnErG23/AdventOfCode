using AdventOfCode.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AdventOfCode.Y2022.Days
{
    public class Day13 : Day
    {
        public Day13(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
        {
            int validPairs = 0;
            int pairs = 0;

            for (int i = 0; i < Inputs.Count(); i += 3)
            {
                pairs++;

                Console.WriteLine($"== Pair {pairs} ==");

                var isValid = Compare(Inputs[i], Inputs[i + 1]);
                validPairs += isValid ? pairs : 0;

                Console.WriteLine($"{pairs}: {isValid}");
                Console.WriteLine();
                Console.WriteLine("---------------------------------------");
                Console.ReadLine();
            }

            return validPairs.ToString();
        }

        public override string RunPart2()
        {
            return "undefined";
        }

        public bool Compare(string left, string right)
        {
            Console.WriteLine(left);
            Console.WriteLine(right);
            Console.WriteLine();

            //  If both values are lists, compare the first value of each list, then the second value, and so on.
            //  OR
            //  If the lists are the same length and no comparison makes a decision about the order, continue checking the next part of the input.
            if ((left[0] == '[' && right[0] == '[') || (left[0] == ',' && right[0] == ',') || (left[0] == ']' && right[0] == ']'))
            {
                left = left.Substring(1);
                right = right.Substring(1);

                return Compare(left, right);
            }
            //If the left list runs out of items first, the inputs are in the right order.
            else if (left[0] == ']')
            {
                Console.WriteLine("Left side ran out of items, so inputs are in the right order");
                return true;
            }
            //If the right list runs out of items first, the inputs are not in the right order.
            else if (right[0] == ']')
            {
                Console.WriteLine("Right side ran out of items, so inputs are not in the right order");
                return false;
            }
            //  If exactly one value is an integer, convert the integer to a list which contains that integer as its only value, then retry the comparison. 
            else if (left[0] == '[')
            {
                left = left.Substring(1);
                right = $"{right[0]}]{right.Substring(1)}";

                return Compare(left, right);
            }
            //  If exactly one value is an integer, convert the integer to a list which contains that integer as its only value, then retry the comparison. 
            else if (right[0] == '[')
            {
                left = $"{left[0]}]{left.Substring(1)}";
                right = right.Substring(1);

                return Compare(left, right);
            }
            //If both values are integers, the lower integer should come first.
            else
            {
                int leftInt = int.Parse(left.Substring(0, Math.Min(left.IndexOf(",") == -1 ? int.MaxValue : left.IndexOf(","), left.IndexOf("]") == -1 ? int.MaxValue : left.IndexOf("]"))));
                int rightInt = int.Parse(right.Substring(0, Math.Min(right.IndexOf(",") == -1 ? int.MaxValue : right.IndexOf(","), right.IndexOf("]") == -1 ? int.MaxValue : right.IndexOf("]"))));

                //If the left integer is lower than the right integer, the inputs are in the right order.
                if (leftInt < rightInt)
                {
                    Console.WriteLine("Left side is smaller, so inputs are in the right order");
                    return true;
                }

                //If the left integer is higher than the right integer, the inputs are not in the right order.
                if (leftInt > rightInt)
                {
                    Console.WriteLine("Right side is smaller, so inputs are not in the right order");
                    return false;
                }

                //Otherwise, the inputs are the same integer; continue checking the next part of the input.
                return Compare(left.Substring(Math.Min(left.IndexOf(",") == -1 ? int.MaxValue : left.IndexOf(","), left.IndexOf("]") == -1 ? int.MaxValue : left.IndexOf("]"))), right.Substring(Math.Min(right.IndexOf(",") == -1 ? int.MaxValue : right.IndexOf(","), right.IndexOf("]") == -1 ? int.MaxValue : right.IndexOf("]"))));
            }
        }
    }
}