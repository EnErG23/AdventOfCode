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
                validPairs += Compare(Inputs[i], Inputs[i + 1]) ? pairs : 0;
            }

            return validPairs.ToString();
        }

        public override string RunPart2()
        {
            var nonEmptyInputs = Inputs.Where(i => i != "").ToList();

            nonEmptyInputs.Add("[[2]]");
            nonEmptyInputs.Add("[[6]]");

            nonEmptyInputs.Sort((i1, i2) => Compare(i1, i2) ? -1 : 1);
            
            return ((nonEmptyInputs.IndexOf("[[2]]") + 1) * (nonEmptyInputs.IndexOf("[[6]]") + 1)).ToString();
        }

        public bool Compare(string left, string right)
        {
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
                return true;
            //If the right list runs out of items first, the inputs are not in the right order.
            else if (right[0] == ']')
                return false;
            //  If exactly one value is an integer, convert the integer to a list which contains that integer as its only value, then retry the comparison. 
            else if (left[0] == '[')
            {
                left = left.Substring(1);
                right = $"{right.Substring(0, Math.Min(right.IndexOf(",") == -1 ? int.MaxValue : right.IndexOf(","), right.IndexOf("]") == -1 ? int.MaxValue : right.IndexOf("]")))}]{right.Substring(Math.Min(right.IndexOf(",") == -1 ? int.MaxValue : right.IndexOf(","), right.IndexOf("]") == -1 ? int.MaxValue : right.IndexOf("]")))}";

                return Compare(left, right);
            }
            //  If exactly one value is an integer, convert the integer to a list which contains that integer as its only value, then retry the comparison. 
            else if (right[0] == '[')
            {
                left = $"{left.Substring(0, Math.Min(left.IndexOf(",") == -1 ? int.MaxValue : left.IndexOf(","), left.IndexOf("]") == -1 ? int.MaxValue : left.IndexOf("]")))}]{left.Substring(Math.Min(left.IndexOf(",") == -1 ? int.MaxValue : left.IndexOf(","), left.IndexOf("]") == -1 ? int.MaxValue : left.IndexOf("]")))}";
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
                    return true;

                //If the left integer is higher than the right integer, the inputs are not in the right order.
                if (leftInt > rightInt)
                    return false;

                //Otherwise, the inputs are the same integer; continue checking the next part of the input.
                return Compare(left.Substring(Math.Min(left.IndexOf(",") == -1 ? int.MaxValue : left.IndexOf(","), left.IndexOf("]") == -1 ? int.MaxValue : left.IndexOf("]"))), right.Substring(Math.Min(right.IndexOf(",") == -1 ? int.MaxValue : right.IndexOf(","), right.IndexOf("]") == -1 ? int.MaxValue : right.IndexOf("]"))));
            }
        }
    }
}