using AdventOfCode.Models;
using AdventOfCode.Y2023.Models;
using System;
using System.ComponentModel;
using System.Runtime.ExceptionServices;

namespace AdventOfCode.Y2023.Days
{
    public class Day19 : Day
    {
        private List<Workflow> _workflows;
        private List<Part> _parts;

        public Day19(int year, int day, bool test) : base(year, day, test)
        {
            bool part = false;
            _workflows = new();
            _parts = new();

            foreach (string input in Inputs)
            {
                if (input == "")
                {
                    part = true;
                    continue;
                }

                if (part)
                    _parts.Add(new Part(input.Replace("{x=", "").Replace("m=", "").Replace("a=", "").Replace("s=", "").Replace("}", "").Split(',').Select(i => long.Parse(i)).ToList()));
                else
                {
                    List<Rule> rules = new();

                    foreach (var rule in input.Substring(input.IndexOf('{') + 1, input.Length - input.IndexOf('{') - 2).Split(','))
                        if (!rule.Contains(':'))
                            rules.Add(new Rule('c', '=', 0, rule));
                        else
                        {
                            char c = rule[0];
                            char o = rule[1];
                            long v = long.Parse(rule.Substring(2, rule.IndexOf(':') - 2));
                            string d = rule.Substring(rule.IndexOf(':') + 1);

                            rules.Add(new Rule(c, o, v, d));
                        }

                    _workflows.Add(new Workflow(input.Substring(0, input.IndexOf('{')), rules));
                }
            }
        }

        public override string RunPart1() => _parts.Where(p => p.Accepted(_workflows)).Sum(p => p.Score).ToString();

        public override string RunPart2() => FollowWorkflow(new() { 1, 4000, 1, 4000, 1, 4000, 1, 4000 }).ToString();

        public long FollowWorkflow(List<long> partIntervals)
        {
            long parts = 0;

            List<(Workflow, List<long>)> paths = new() { (_workflows.First(w => w.Name == "in"), partIntervals) };

            while (paths.Any())
            {
                var path = paths.First();
                paths.Remove(path);
                var workflow = path.Item1;
                var pi = path.Item2;

                if (pi[0] > pi[1] || pi[2] > pi[3] || pi[4] > pi[5] || pi[6] > pi[7])
                    continue;

                foreach (var rule in workflow.Rules)
                {
                    var newPi = pi.ToList();

                    switch (rule.Operator)
                    {
                        case '<':
                            pi[rule.Category * 2 + 1] = rule.Value - 1;
                            newPi[rule.Category * 2] = rule.Value;
                            switch (rule.Destination)
                            {
                                case "A":
                                    parts += (pi[1] - pi[0] + 1) * (pi[3] - pi[2] + 1) * (pi[5] - pi[4] + 1) * (pi[7] - pi[6] + 1);
                                    break;
                                case "R":
                                    break;
                                default:
                                    paths.Add((_workflows.First(w => w.Name == rule.Destination), pi));
                                    break;
                            }
                            break;
                        case '>':
                            pi[rule.Category * 2] = rule.Value + 1;
                            newPi[rule.Category * 2 + 1] = rule.Value;
                            switch (rule.Destination)
                            {
                                case "A":
                                    parts += (pi[1] - pi[0] + 1) * (pi[3] - pi[2] + 1) * (pi[5] - pi[4] + 1) * (pi[7] - pi[6] + 1);
                                    break;
                                case "R":
                                    break;
                                default:
                                    paths.Add((_workflows.First(w => w.Name == rule.Destination), pi));
                                    break;
                            }
                            break;
                        default:
                            switch (rule.Destination)
                            {
                                case "A":
                                    parts += (pi[1] - pi[0] + 1) * (pi[3] - pi[2] + 1) * (pi[5] - pi[4] + 1) * (pi[7] - pi[6] + 1);
                                    break;
                                case "R":
                                    break;
                                default:
                                    paths.Add((_workflows.First(w => w.Name == rule.Destination), pi));
                                    break;
                            }
                            break;
                    }

                    pi = newPi;
                }
            }

            return parts;
        }
    }

    public class Workflow
    {
        public string Name { get; set; }
        public List<Rule> Rules { get; set; }

        public Workflow(string name)
        {
            Name = name;
            Rules = new List<Rule>();
        }

        public Workflow(string name, List<Rule> rules)
        {
            Name = name;
            Rules = rules;
        }
    }

    public class Rule
    {
        public int Category { get; set; }
        public char Operator { get; set; }
        public long Value { get; set; }
        public string Destination { get; set; }

        public Rule(char category, char op, long value, string destination)
        {
            switch (category)
            {
                case 'x':
                    Category = 0;
                    break;
                case 'm':
                    Category = 1;
                    break;
                case 'a':
                    Category = 2;
                    break;
                case 's':
                    Category = 3;
                    break;
            }

            Operator = op;
            Value = value;
            Destination = destination;
        }
    }

    public class Part
    {
        public List<long> Values { get; set; }
        public long Score => Values.Sum();

        public Part(List<long> values) => Values = values;

        public bool Accepted(List<Workflow> workflows)
        {
            Workflow workflow = workflows.First(w => w.Name == "in");

            while (true)
            {
                foreach (var rule in workflow.Rules)
                {
                    bool ruleMet = false;

                    switch (rule.Operator)
                    {
                        case '<':
                            if (Values[rule.Category] < rule.Value)
                                switch (rule.Destination)
                                {
                                    case "A":
                                        return true;
                                    case "R":
                                        return false;
                                    default:
                                        ruleMet = true;
                                        workflow = workflows.First(w => w.Name == rule.Destination);
                                        break;
                                }
                            break;
                        case '>':
                            if (Values[rule.Category] > rule.Value)
                                switch (rule.Destination)
                                {
                                    case "A":
                                        return true;
                                    case "R":
                                        return false;
                                    default:
                                        ruleMet = true;
                                        workflow = workflows.First(w => w.Name == rule.Destination);
                                        break;
                                }
                            break;
                        default:
                            switch (rule.Destination)
                            {
                                case "A":
                                    return true;
                                case "R":
                                    return false;
                                default:
                                    ruleMet = true;
                                    workflow = workflows.First(w => w.Name == rule.Destination);
                                    break;
                            }
                            break;
                    }

                    if (ruleMet)
                        break;
                }
            }
        }
    }
}
