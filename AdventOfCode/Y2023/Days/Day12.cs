using AdventOfCode.Models;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2023.Days
{
    public class Day12 : Day
    {
        private List<Record> _records;
        private List<(List<int>, long)>? _checked;

        public Day12(int year, int day, bool test) : base(year, day, test) => _records = Inputs.Select(i => new Record(i.Split(" ")[0], i.Split(" ")[1].Split(",").Select(i => int.Parse(i)).ToList())).ToList();

        public override string RunPart1()
        {
            long result = 0;

            foreach (var record in _records)
            {
                _checked = new();
                result += CalculatePossibilities(record, 0, 0, 0);
            }

            return result.ToString();
        }

        public override string RunPart2()
        {
            long result = 0;

            foreach (var record in _records)
            {
                _checked = new();

                record.Springs = $"{record.Springs}?{record.Springs}?{record.Springs}?{record.Springs}?{record.Springs}";
                
                var springGroups = record.SpringGroups.ToList();
                record.SpringGroups.AddRange(springGroups);
                record.SpringGroups.AddRange(springGroups);
                record.SpringGroups.AddRange(springGroups);
                record.SpringGroups.AddRange(springGroups);

                result += CalculatePossibilities(record, 0, 0, 0);
            }

            return result.ToString();
        }

        private long CalculatePossibilities(Record record, int springIndex, int groupIndex, int currentGroupLength)
        {
            if (_checked.Exists(c => c.Item1[0] == springIndex && c.Item1[1] == groupIndex && c.Item1[2] == currentGroupLength))
                return _checked.First(c => c.Item1[0] == springIndex && c.Item1[1] == groupIndex && c.Item1[2] == currentGroupLength).Item2;

            if (springIndex == record.Springs.Length)
                return (groupIndex == record.SpringGroups.Count && currentGroupLength == 0) || (groupIndex == record.SpringGroups.Count - 1 && record.SpringGroups[groupIndex] == currentGroupLength) ? 1 : 0;

            long possibilities = 0;

            foreach (var c in new char[] { '.', '#' })
                if (record.Springs[springIndex] == c || record.Springs[springIndex] == '?')
                    if (c == '.' && currentGroupLength == 0)
                        possibilities += CalculatePossibilities(record, springIndex + 1, groupIndex, 0);
                    else if (c == '.' && currentGroupLength > 0 && groupIndex < record.SpringGroups.Count && record.SpringGroups[groupIndex] == currentGroupLength)
                        possibilities += CalculatePossibilities(record, springIndex + 1, groupIndex + 1, 0);
                    else if (c == '#')
                        possibilities += CalculatePossibilities(record, springIndex + 1, groupIndex, currentGroupLength + 1);

            _checked.Add((new List<int>() { springIndex, groupIndex, currentGroupLength }, possibilities));

            return possibilities;
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