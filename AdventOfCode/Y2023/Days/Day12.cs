using AdventOfCode.Models;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2023.Days
{
    public class Day12 : Day
    {
        private List<Record> _records;

        public Day12(int year, int day, bool test) : base(year, day, test) => _records = Inputs.Select(i => new Record(i.Split(" ")[0], i.Split(" ")[1].Split(",").Select(i => int.Parse(i)).ToList())).ToList();

        public override string RunPart1() => _records.Sum(r => GetArrangements(r.Springs, r.SpringGroups)).ToString();

        public override string RunPart2()
        {
            foreach (var record in _records)
            {
                record.Springs = $"{record.Springs}?{record.Springs}?{record.Springs}?{record.Springs}?{record.Springs}";
                record.SpringGroups.AddRange(record.SpringGroups);
                record.SpringGroups.AddRange(record.SpringGroups);
                record.SpringGroups.AddRange(record.SpringGroups);
                record.SpringGroups.AddRange(record.SpringGroups);
            }

            return _records.Sum(r => GetArrangements(r.Springs, r.SpringGroups)).ToString();
        }

        private int GetArrangements(string springs, List<int> springCounts)
        {
            int arrangements = 0;

            var regex = new Regex(Regex.Escape("?"));

            foreach (var s in new string[] { ".", "#" })
            {
                var newString = regex.Replace(springs, s, 1);
                var firstUnknown = newString.IndexOf("?");

                if (firstUnknown < 0)
                    arrangements += IsValid(newString, springCounts) ? 1 : 0;
                else if (firstUnknown < 2 || newString.Substring(firstUnknown - 2, 2) != "#." || IsValid(newString.Substring(0, firstUnknown), springCounts.Take(GetSpringCount(newString.Substring(0, firstUnknown)).Count()).ToList()))
                {
                    var shortString = newString.Substring(0, firstUnknown);

                    while (shortString.Contains(".."))
                        shortString = shortString.Replace("..", ".");

                    var tempArrangements = GetArrangements(newString, springCounts);
                    arrangements += tempArrangements;
                }
            }

            return arrangements;
        }

        private List<int> GetSpringCount(string springs)
        {
            while (springs.Contains(".."))
                springs = springs.Replace("..", ".");

            if (springs[0] == '.')
                springs = springs.Substring(1);

            if (springs == null || springs == "")
                return new List<int> { 0 };

            if (springs[springs.Length - 1] == '.')
                springs = springs.Substring(0, springs.Length - 1);

            return springs.Split(".").Select(s => s.Length).ToList();
        }

        private bool IsValid(string springs, List<int> springCounts)
        {
            List<int> springCount = GetSpringCount(springs);

            if (springCount.Count != springCounts.Count)
                return false;

            for (int i = 0; i < springCount.Count; i++)
                if (springCount[i] != springCounts[i])
                    return false;

            return true;
        }
    }

    public class Record
    {
        public string Springs { get; set; }
        public List<int> SpringGroups { get; set; }

        public Record(string springs, List<int> springGroups)
        {
            Springs = springs;
            SpringGroups = springGroups;
        }
    }
}