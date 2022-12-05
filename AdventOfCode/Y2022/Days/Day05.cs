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
                int amountToMove = Math.Min(step[0], _stacks[step[1] - 1].Length);
                char[] packagesToMove = _stacks[step[1] - 1].Substring(0, amountToMove).ToCharArray();
                Array.Reverse(packagesToMove);

                _stacks[step[2] - 1] = new string(packagesToMove) + _stacks[step[2] - 1];
                _stacks[step[1] - 1] = _stacks[step[1] - 1].Substring(amountToMove);
            }

            return String.Join("", _stacks.Select(s => s[0]));
        }

        public override string RunPart2()
        {
            foreach (var step in _steps)
            {
                int amountToMove = Math.Min(step[0], _stacks[step[1] - 1].Length);

                _stacks[step[2] - 1] = _stacks[step[1] - 1].Substring(0, amountToMove) + _stacks[step[2] - 1];
                _stacks[step[1] - 1] = _stacks[step[1] - 1].Substring(amountToMove);
            }

            return String.Join("", _stacks.Select(s => s[0]));
        }
    }
}