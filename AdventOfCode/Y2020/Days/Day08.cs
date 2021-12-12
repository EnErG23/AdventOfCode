using AdventOfCode.Models;
using System.Diagnostics;

namespace AdventOfCode.Y2020.Days
{
    public class Day08 : Day
    {
        public Day08(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
        {
            long result = 0;

            var inputs = Inputs.ToList();

            var ranLines = new List<int>();

            var i = 0;

            while (true)
            {
                if (ranLines.Contains(i))
                    break;

                ranLines.Add(i);

                var command = inputs[i].Substring(0, 3);

                switch (command)
                {
                    case "acc":
                        var accValue = Convert.ToInt32(inputs[i].Substring(5));
                        if (inputs[i].Contains("+"))
                            result += accValue;
                        else
                            result -= accValue;

                        i++;
                        break;
                    case "jmp":
                        var jmpValue = Convert.ToInt32(inputs[i].Substring(5));
                        if (inputs[i].Contains("+"))
                            i += jmpValue;
                        else
                            i -= jmpValue;
                        break;
                    default:
                        i++;
                        break;
                }
            }

            return result.ToString();
        }

        public override string RunPart2()
        {
            long result = 0;

            for (int j = 0; j < Inputs.Count() - 1; j++)
            {
                result = 0;

                var inputs = Inputs.ToList();

                if (inputs[j].Contains("jmp"))
                    inputs[j] = inputs[j].Replace("jmp", "nop");
                else if (inputs[j].Contains("nop"))
                    inputs[j] = inputs[j].Replace("nop", "jmp");
                else
                    continue;

                var ranLines = new List<int>();

                var i = 0;
                var found = false;

                while (true)
                {
                    if (i > inputs.Count() - 1)
                    {
                        found = true;
                        break;
                    }

                    if (ranLines.Contains(i))
                    {
                        break;
                    }

                    ranLines.Add(i);

                    var command = inputs[i].Substring(0, 3);

                    switch (command)
                    {
                        case "acc":
                            var accValue = Convert.ToInt32(inputs[i].Substring(5));
                            if (inputs[i].Contains("+"))
                                result += accValue;
                            else
                                result -= accValue;

                            i++;
                            break;
                        case "jmp":
                            var jmpValue = Convert.ToInt32(inputs[i].Substring(5));
                            if (inputs[i].Contains("+"))
                                i += jmpValue;
                            else
                                i -= jmpValue;
                            break;
                        default:
                            i++;
                            break;
                    }
                }

                if (found) break;
            }

            return result.ToString();
        }
    }
}