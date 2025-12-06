using AdventOfCode.Models;

namespace AdventOfCode.Y2025.Days
{
    public class Day06 : Day
    {
        private readonly List<List<string>> _numberLists;
        private readonly List<char> _operators;

        public Day06(int year, int day, bool test) : base(year, day, test)
        {
            _numberLists = new();

            foreach (var input in Inputs.SkipLast(1))
                _numberLists.Add(input.Split(" ")
                    .Where(i => !string.IsNullOrEmpty(i))
                    .Select(i => i.Trim())
                    .ToList());

            _operators = new();

            foreach (var input in Inputs.TakeLast(1))
                _operators = input.Replace(" ", "").ToCharArray().ToList();
        }

        public override string RunPart1()
        {
            long result = 0;

            for (int i = 0; i < _numberLists[0].Count; i++)
            {
                var operatorS = _operators[i];

                long problemResult = 0;

                if (operatorS == '*')
                    problemResult++;

                foreach (var numberList in _numberLists)
                    if (operatorS == '+')
                        problemResult += long.Parse(numberList[i]);
                    else
                        problemResult *= long.Parse(numberList[i]);

                result += problemResult;
            }

            return result.ToString();
        }

        public override string RunPart2()
        {
            long result = 0;

            var lengthLongestInput = Inputs.Aggregate("", (max, cur) => max.Length > cur.Length ? max : cur).Length;

            var operatorC = '+';
            long problemResult = 0;

            for (int i = 0; i < lengthLongestInput; i++)
            {
                string numberS = "";

                try
                {
                    var newOperatorS = Inputs[Inputs.Count - 1][i];

                    if (!string.IsNullOrEmpty(newOperatorS.ToString().Trim()))
                    {
                        result += problemResult;
                        operatorC = newOperatorS;

                        if (operatorC == '+')
                            problemResult = 0;
                        else
                            problemResult = 1;
                    }
                }
                catch { }

                foreach (var input in Inputs.SkipLast(1))
                {
                    numberS += input[i];
                }

                if (!string.IsNullOrEmpty(numberS.Trim()))
                    if (operatorC == '+')
                        problemResult += long.Parse(numberS.Trim());
                    else
                        problemResult *= long.Parse(numberS.Trim());
            }

            result += problemResult;

            return result.ToString();
        }
    }
}