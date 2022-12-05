using AdventOfCode.Models;

namespace AdventOfCode.Y2022.Days
{
    public class Day05 : Day
    {
        private readonly List<string> _stacks;
        private readonly List<List<int>> _steps;

        public Day05(int year, int day, bool test) : base(year, day, test)
        {
            //Parse stacks
            _stacks = new List<string>();

            for (int i = 0; i < int.Parse(Inputs.FirstOrDefault(i => i[1] == '1').Replace(" ", "").Last().ToString()); i++)
                _stacks.Add("");

            foreach (var input in Inputs)
            {
                if (input[1] == '1')
                    break;

                var row = input.Replace("    ", "[ ] ").Replace("][ ] ", "] [ ]").Replace("] [", "").Replace("[", "").Replace("]", "");

                int stack = 0;
                foreach (var package in row)
                    _stacks[stack++] += $"{package}";
            }

            _stacks = _stacks
                .Select(s => s.Replace(" ", ""))
                .ToList();

            //Parse steps
            _steps = Inputs
                .Where(i => i.Contains("move"))
                .Select(i => i.Replace("move ", "").Replace("from ", "").Replace("to ", "").Split(" ").Select(s => int.Parse(s)).ToList())
                .ToList();
        }

        public override string RunPart1()
        {
            foreach (var step in _steps)
            {
                int toMove = Math.Min(step[0], _stacks[step[1] - 1].Length);

                for (int i = 0; i < toMove; i++)
                {
                    _stacks[step[2] - 1] = _stacks[step[1] - 1][0] + _stacks[step[2] - 1];
                    _stacks[step[1] - 1] = _stacks[step[1] - 1].Substring(1);
                }
            }

            return String.Join("", _stacks.Select(s => s[0]));
        }

        public override string RunPart2()
        {
            foreach (var step in _steps)
            {
                int toMove = Math.Min(step[0], _stacks[step[1] - 1].Length);

                _stacks[step[2] - 1] = _stacks[step[1] - 1].Substring(0, toMove) + _stacks[step[2] - 1];
                _stacks[step[1] - 1] = _stacks[step[1] - 1].Substring(toMove);
            }

            return String.Join("", _stacks.Select(s => s[0]));
        }
    }
}