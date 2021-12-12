using AdventOfCode.Models;
using AdventOfCode.Helpers;

namespace AdventOfCode.Y2019.Days
{
    public class Day02 : Day
    {
        private List<int>? inputs;

        public Day02(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
        {
            inputs = Inputs[0].Split(',').Select(i => int.Parse(i)).ToList();

            return IntCode(4, 12, 2).ToString();
        }

        public override string RunPart2()
        {
            inputs = Inputs[0].Split(',').Select(i => int.Parse(i)).ToList();

            long result = 0;

            var output = 19690720;

            for (int i = 0; i < Inputs[0].Split(',').Length - 1; i++)
            {
                for (int j = 0; j < Inputs[0].Split(',').Length - 1; j++)
                {
                    inputs = Inputs[0].Split(',').Select(i => int.Parse(i)).ToList();

                    if (IntCode(4, i, j) == output)
                    {
                        result = 100 * i + j;
                        break;
                    }
                }
                if (result != 0)
                    break;
            }

            return result.ToString();
        }

        private long IntCode(int pointer, int noun, int verb)
        {
            inputs[1] = noun;
            inputs[2] = verb;

            for (int i = 0; i < inputs.Count - 1; i += pointer)
            {
                if (inputs[i] == 1)
                    inputs[inputs[i + 3]] = inputs[inputs[i + 1]] + inputs[inputs[i + 2]];
                else if (inputs[i] == 2)
                    inputs[inputs[i + 3]] = inputs[inputs[i + 1]] * inputs[inputs[i + 2]];
                else if (inputs[i] == 99)
                    break;
            }

            return inputs[0];
        }
    }
}