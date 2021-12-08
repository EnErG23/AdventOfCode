using AdventOfCode.Models;

namespace AdventOfCode.Y2021.Days
{
    public class Day08 : Day
    {
        public Day08(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
        {
            return Inputs.Sum(i => i.Substring(i.IndexOf("|") + 2)
                                    .Split(" ")
                                    .Count(o => (o.Length > 1 && o.Length < 5) || o.Length == 7))
                         .ToString();
        }

        public override string RunPart2()
        {
            long result = 0;

            foreach (var i in Inputs)
            {
                List<string> inputs = i.Substring(0, i.IndexOf("|") - 1).Split(" ").Select(p => String.Join("", p.OrderBy(c => c))).ToList();
                List<string> outputs = i.Substring(i.IndexOf("|") + 2).Split(" ").Select(p => String.Join("", p.OrderBy(c => c))).ToList();

                List<string> digitPatterns = DetermineDigitPatterns(inputs);

                result += Convert.ToInt32(String.Join("", outputs.Select(o => digitPatterns.IndexOf(o).ToString())));
            }

            return result.ToString();
        }

        private List<string> DetermineDigitPatterns(List<string> inputs)
        {
            string[] patterns = new string[10];

            patterns[1] = inputs.First(i => i.Length == 2);
            patterns[4] = inputs.First(i => i.Length == 4);

            char tr = patterns[1][0];
            char br = patterns[1][1];
            char tl = patterns[4].First(c => !patterns[1].Contains(c));
            char m = patterns[4].Last(c => !patterns[1].Contains(c));

            patterns[0] = inputs.First(i => i.Length == 6 && (i.Contains(tr) && i.Contains(br)) && !(i.Contains(tl) && i.Contains(m)));
            patterns[2] = inputs.First(i => i.Length == 5 && !(i.Contains(tr) && i.Contains(br)) && !(i.Contains(tl) && i.Contains(m)));
            patterns[3] = inputs.First(i => i.Length == 5 && (i.Contains(tr) && i.Contains(br)) && !(i.Contains(tl) && i.Contains(m)));
            patterns[5] = inputs.First(i => i.Length == 5 && !(i.Contains(tr) && i.Contains(br)) && (i.Contains(tl) && i.Contains(m)));
            patterns[6] = inputs.First(i => i.Length == 6 && !(i.Contains(tr) && i.Contains(br)) && (i.Contains(tl) && i.Contains(m)));
            patterns[7] = inputs.First(i => i.Length == 3);
            patterns[8] = inputs.First(i => i.Length == 7);
            patterns[9] = inputs.First(i => i.Length == 6 && (i.Contains(tr) && i.Contains(br)) && (i.Contains(tl) && i.Contains(m)));

            return patterns.ToList();
        }
    }
}