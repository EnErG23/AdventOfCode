using AdventOfCode.Helpers;
using AdventOfCode.Y2020.Models;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2020.Days
{
    public static class Day19
    {
        static int day = 19;
        static List<string>? inputs;
        public static List<MessageRule> rules = new List<MessageRule>();

        public static string? Answer1 { get; set; }
        public static string? Answer2 { get; set; }

        public static void Run(int part, bool test)
        {
            inputs = InputManager.GetInputAsStrings(day, test);

            var start = DateTime.Now;

            string part1 = "";
            string part2 = "";

            switch (part)
            {
                case 1:
                    part1 = Part1();
                    break;
                case 2:
                    part2 = Part2();
                    break;
                default:
                    part1 = Part1();
                    part2 = Part2();
                    break;
            }

            var ms = Math.Round((DateTime.Now - start).TotalMilliseconds);

            Console.WriteLine($"Day {day} ({ms}ms):");
            if (part1 != "") Console.WriteLine($"    {part1}");
            if (part2 != "") Console.WriteLine($"    {part2}");
        }

        static string Part1()
        {
            long result = 0;

            var start = DateTime.Now;

            #region Solution

            inputs = inputs.Where(i => i != "").ToList();

            foreach (var input in inputs.Where(i => i.Contains(":")))
            {
                var id = input.Substring(0, input.IndexOf(":"));
                var subRules = new List<List<string>>();

                if (!input.Contains("\""))
                    foreach (var subRule in input.Substring(input.IndexOf(":") + 1).Split('|'))
                        subRules.Add(subRule.Split(' ').Where(s => s != "").ToList());

                var match = input.Contains("\"") ? input.Substring(input.IndexOf("\"") + 1, input.LastIndexOf("\"") - input.IndexOf("\"") - 1) : "";

                var messageRule = new MessageRule
                {
                    ID = id,
                    SubRules = subRules,
                    Match = match
                };

                rules.Add(messageRule);
            }

            var messages = new List<string>();

            foreach (var input in inputs.Where(i => !i.Contains(":")))
                messages.Add(input);

            var pattern = $@"^{BuildPattern("0")}$";

            Regex rgx = new Regex(pattern);

            foreach (var message in messages)
                result += rgx.IsMatch(message) ? 1 : 0;

            #endregion

            var ms = Math.Round((DateTime.Now - start).TotalMilliseconds);

            if (result > 0) Answer1 = result.ToString();
            return $"Part 1 ({ms}ms): {result} ";
        }

        static string Part2()
        {
            long result = 0;

            var start = DateTime.Now;

            #region Solution



            #endregion

            var ms = Math.Round((DateTime.Now - start).TotalMilliseconds);

            if (result > 0) Answer2 = result.ToString();
            return $"Part 2 ({ms}ms): {result} ";
        }

        static string BuildPattern(string ruleID)
        {
            var result = "";

            var rule = rules.Find(r => r.ID == ruleID);

            if (rule.Match != "")
            {
                result += rule.Match;
            }
            else
            {
                result += "(";
                var tempResult = "";
                foreach (var subRule in rule.SubRules)
                {
                    foreach (var subRuleID in subRule)
                    {
                        tempResult += BuildPattern(subRuleID);
                    }
                    tempResult += "|";
                }
                result += $"{tempResult.Substring(0, tempResult.Length - 1)})";
            }

            return result;
        }

        static string BuildPattern2(string ruleID)
        {
            var result = "";

            var rule = rules.Find(r => r.ID == ruleID);

            if (rule.Match != "")
            {
                result += rule.Match;
            }
            else
            {
                if (ruleID == "8")
                    return $"(({BuildPattern2("42")})+)";

                if (ruleID == "11")
                {
                    var elevenResult = "(";

                    for (int i = 1; i < 11; i++)
                    {
                        elevenResult += $"(({BuildPattern2("42")}{{{i}}})({BuildPattern2("31")}{{{i}}}))|";
                    }

                    return $"{elevenResult.Substring(0, elevenResult.Length - 1)})";
                }

                result += "(";

                var tempResult = "";
                foreach (var subRule in rule.SubRules)
                {
                    foreach (var subRuleID in subRule)
                    {
                        tempResult += BuildPattern2(subRuleID);
                    }
                    tempResult += "|";
                }
                result += $"{tempResult.Substring(0, tempResult.Length - 1)})";
            }

            return result;
        }
    }
}