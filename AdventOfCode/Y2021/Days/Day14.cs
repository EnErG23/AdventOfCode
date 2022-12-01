using AdventOfCode.Models;

namespace AdventOfCode.Y2021.Days
{
    public class Day14 : Day
    {
        List<(string, string)>? rules;

        public Day14(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
        {
            string polymer = Inputs[0];

            rules = new();
            Inputs.Skip(2).ToList().ForEach(i => rules.Add((i.Split(" -> ")[0], i.Split(" -> ")[1])));

            for (int i = 0; i < 10; i++)
            {
                for (int p = 0; p < polymer.Length - 1; p += 2)
                {
                    string pair = polymer.Substring(p, 2);
                    string element = rules.First(r => r.Item1 == pair).Item2;

                    polymer = $"{polymer.Substring(0, p + 1)}{element}{polymer.Substring(p + 1)}";
                }

                //Console.WriteLine($"After step {i + 1}: {polymer.Substring(0, Math.Min(polymer.Length, 10))}...");
            }

            var polymerCounts = polymer.ToList()
                                       .GroupBy(p => p)
                                       .Select(p => new
                                       {
                                           Element = p.FirstOrDefault(),
                                           Occurence = (long)p.Count()
                                       })
                                       .OrderByDescending(p => p.Occurence);

            return (polymerCounts.First().Occurence - polymerCounts.Last().Occurence).ToString();
        }

        public override string RunPart2()
        {
            List<string> polymerSegments = new();

            for (int i = 0; i < Inputs[0].Length - 1; i++)
                polymerSegments.Add($"{Inputs[0][i]}{Inputs[0][i + 1]}");

            rules = new();
            Inputs.Skip(2).ToList().ForEach(i => rules.Add((i.Split(" -> ")[0], $"{(i.Split(" -> ")[0][0])}{(i.Split(" -> ")[1])}{(i.Split(" -> ")[0][1])}")));

            //for (int i = 0; i < 40; i++)
            for (int i = 0; i < 10; i++)
            {
                polymerSegments = ApplyRules(polymerSegments);

                Console.WriteLine($"After step {i + 1}: {String.Join("", polymerSegments.Select(p => p.Substring(0, p.Length - 1)).Take(5))}");
            }

            var polymer = String.Join("", polymerSegments);

            var polymerCounts = polymer.ToList()
                                       .GroupBy(p => p)
                                       .Select(p => new
                                       {
                                           Element = p.FirstOrDefault(),
                                           Occurence = (long)p.Count()
                                       })
                                       .OrderByDescending(p => p.Occurence);

            return (polymerCounts.First().Occurence - polymerCounts.Last().Occurence).ToString();
        }

        private List<string> ApplyRules(List<string> polymerSegments)
        {
            List<string> newPolymerSegments = new();

            for (int i = 0; i < polymerSegments.Count; i++)
                newPolymerSegments.Add(rules.First(r => r.Item1 == polymerSegments[i]).Item2);

            return newPolymerSegments;
        }
    }
}