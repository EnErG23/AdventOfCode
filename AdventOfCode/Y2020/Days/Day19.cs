using AdventOfCode.Models;
using System.Diagnostics;
using AdventOfCode.Y2020.Models;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2020.Days
{
    public class Day19 : Day
    {
        public static List<MessageRule> rules = new List<MessageRule>();

        public Day19(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
        {
            long result = 0;

            Inputs = Inputs.Where(i => i != "").ToList();

            foreach (var input in Inputs.Where(i => i.Contains(":")))
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

            foreach (var input in Inputs.Where(i => !i.Contains(":")))
                messages.Add(input);

            var pattern = $@"^{BuildPattern("0")}$";

            Regex rgx = new Regex(pattern);

            foreach (var message in messages)
                result += rgx.IsMatch(message) ? 1 : 0;

			return result.ToString();
        }

        public override string RunPart2()
        {
            long result = 0;



			return result.ToString();
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