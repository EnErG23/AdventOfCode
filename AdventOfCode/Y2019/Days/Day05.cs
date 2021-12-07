using AdventOfCode.Models;

namespace AdventOfCode.Y2019.Days
{
    public class Day05 : Day
    {
        private const int day = 5;
        private List<int>? inputs;

        public Day05(bool test) : base(day, test) { }

        public override string RunPart1()
        {
            inputs = Inputs[0].Split(',').Select(i => int.Parse(i)).ToList();

            return IntCode(1).ToString();
        }

        public override string RunPart2()
        {
            inputs = Inputs[0].Split(',').Select(i => int.Parse(i)).ToList();

            return IntCode(5).ToString();
        }

        private long IntCode(int input)
        {
            var i = 0;

            while (i < inputs.Count)
            {
                var instruction = inputs[i].ToString();

                while (instruction.Length < 5)
                    instruction = "0" + instruction;

                var opcode = instruction.Substring(3, 2);

                if (opcode == "99")
                    break;

                var modeParam1 = instruction.Substring(2, 1);
                var modeParam2 = instruction.Substring(1, 1);
                var modeParam3 = instruction[..1];

                var param1 = 0;
                var param2 = 0;
                var param3 = 0;

                switch (opcode)
                {
                    case "01":
                        param1 = modeParam1 == "1" ? inputs[i + 1] : inputs[inputs[i + 1]];
                        param2 = modeParam2 == "1" ? inputs[i + 2] : inputs[inputs[i + 2]];

                        inputs[inputs[i + 3]] = param1 + param2;
                        i += 4;
                        break;
                    case "02":
                        param1 = modeParam1 == "1" ? inputs[i + 1] : inputs[inputs[i + 1]];
                        param2 = modeParam2 == "1" ? inputs[i + 2] : inputs[inputs[i + 2]];

                        inputs[inputs[i + 3]] = param1 * param2;
                        i += 4;
                        break;
                    case "03":
                        inputs[inputs[i + 1]] = input;
                        i += 2;
                        break;
                    case "04":
                        param1 = modeParam1 == "1" ? inputs[i + 1] : inputs[inputs[i + 1]];

                        if (param1 > 0)
                            return param1;
                        i += 2;
                        break;
                    case "05":
                        param1 = modeParam1 == "1" ? inputs[i + 1] : inputs[inputs[i + 1]];
                        param2 = modeParam2 == "1" ? inputs[i + 2] : inputs[inputs[i + 2]];

                        i = param1 != 0 ? param2 : i += 3;
                        break;
                    case "06":
                        param1 = modeParam1 == "1" ? inputs[i + 1] : inputs[inputs[i + 1]];
                        param2 = modeParam2 == "1" ? inputs[i + 2] : inputs[inputs[i + 2]];

                        i = param1 == 0 ? param2 : i += 3;
                        break;
                    case "07":
                        param1 = modeParam1 == "1" ? inputs[i + 1] : inputs[inputs[i + 1]];
                        param2 = modeParam2 == "1" ? inputs[i + 2] : inputs[inputs[i + 2]];
                        param3 = modeParam3 == "1" ? inputs[i + 3] : inputs[inputs[i + 3]];

                        inputs[inputs[i + 3]] = param1 < param2 ? 1 : 0;
                        i += 4;
                        break;
                    case "08":
                        param1 = modeParam1 == "1" ? inputs[i + 1] : inputs[inputs[i + 1]];
                        param2 = modeParam2 == "1" ? inputs[i + 2] : inputs[inputs[i + 2]];
                        param3 = modeParam3 == "1" ? inputs[i + 3] : inputs[inputs[i + 3]];

                        inputs[inputs[i + 3]] = param1 == param2 ? 1 : 0;
                        i += 4;
                        break;
                    default:
                        i++;
                        break;
                }
            }

            return inputs[0];
        }
    }
}