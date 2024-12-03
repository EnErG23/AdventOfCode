using AdventOfCode.Models;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2024.Days
{
    public class Day03 : Day
    {
        public Day03(int year, int day, bool test) : base(year, day, test) => Inputs = new List<string> { string.Join("", Inputs) };       

        public override string RunPart1() => AddMultiplications(Inputs[0]).ToString();

        public override string RunPart2()
        {
            if (Test)
                Inputs = new List<string> { "xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))" };

            var input = Inputs[0];

            string doPat = @"do\(\)";
            string dontPat = @"don't\(\)";

            Regex doRegex = new Regex(doPat, RegexOptions.IgnoreCase);
            Regex dontRegex = new Regex(dontPat, RegexOptions.IgnoreCase);

            Match doMatch = doRegex.Match(input);
            Match dontMatch = dontRegex.Match(input);

            var newInput = input.ToString();

            while (dontMatch.Success)
            {
                while (doMatch.Success && doMatch.Index < dontMatch.Index)
                {
                    doMatch = doMatch.NextMatch();
                }

                if (!doMatch.Success)
                {
                    newInput = newInput.Replace(input.Substring(dontMatch.Index), "");
                    break;
                }

                newInput = newInput.Replace(input.Substring(dontMatch.Index, doMatch.Index - dontMatch.Index), "");

                dontMatch = dontMatch.NextMatch();
            }

            return AddMultiplications(newInput).ToString();
        }

        private int AddMultiplications(string input)
        {
            int result = 0;

            string mulPat = @"mul\([0-9]{1,3},[0-9]{1,3}\)";
            Regex mulRegex = new Regex(mulPat, RegexOptions.IgnoreCase);
            Match mulMatch = mulRegex.Match(input);

            while (mulMatch.Success)
            {
                var numbers = mulMatch.Value.Replace("mul(", "").Replace(")", "").Split(",").Select(i => int.Parse(i)).ToList();
                result += numbers[0] * numbers[1];
                mulMatch = mulMatch.NextMatch();
            }

            return result;
        }
    }
}