using AdventOfCode.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AdventOfCode.Y2023.Days
{
    public class Day15 : Day
    {
        private List<(int, List<Lens>)>? _boxes;

        public Day15(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1() => Inputs[0].Split(',').Sum(s => Hash(s)).ToString();

        public override string RunPart2()
        {
            _boxes = new();

            foreach (var input in Inputs[0].Split(','))
            {
                string command = input.Contains("=") ? "=" : "-";

                string label = input.Substring(0, input.IndexOf(command));
                int hash = Hash(label);
                int focalLength = command == "=" ? int.Parse(input.Substring(input.Length - 1, 1)) : 0;

                if (command == "=")
                    if (!_boxes.Exists(b => b.Item1 == hash))
                        _boxes.Add((hash, new() { new Lens(label, focalLength) }));
                    else if (!_boxes.First(b => b.Item1 == hash).Item2.Exists(l => l.Label == label))
                        _boxes.First(b => b.Item1 == hash).Item2.Add(new Lens(label, focalLength));
                    else
                        _boxes.First(b => b.Item1 == hash).Item2.First(l => l.Label == label).FocalLength = focalLength;
                else if (_boxes.Exists(b => b.Item1 == hash) && _boxes.First(b => b.Item1 == hash).Item2.Exists(l => l.Label == label))
                    _boxes.First(b => b.Item1 == hash).Item2.Remove(_boxes.First(b => b.Item1 == hash).Item2.First(l => l.Label == label));
            }

            long result = 0;

            foreach (var box in _boxes.Where(b => b.Item2.Count > 0))
                for (int i = 0; i < box.Item2.Count; i++)
                    result += (box.Item1 + 1) * (i + 1) * (box.Item2[i].FocalLength);

            return result.ToString();
        }

        public int Hash(string s)
        {
            int v = 0;

            foreach (var av in Encoding.ASCII.GetBytes(s))
                v = ((v + av) * 17) % 256;

            return v;
        }
    }

    public class Lens
    {
        public string Label { get; set; }
        public int FocalLength { get; set; }

        public Lens(string label, int focalLength)
        {
            Label = label;
            FocalLength = focalLength;
        }
    }
}