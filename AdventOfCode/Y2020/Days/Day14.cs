using AdventOfCode.Helpers;
using AdventOfCode.Y2020.Models;

namespace AdventOfCode.Y2020.Days
{
    public static class Day14
    {
        static int day = 14;
        static List<string>? inputs;

        public static string? Answer1 { get; set; }
        public static string? Answer2 { get; set; }

        public static void Run(int part, bool test)
        {
            var start = DateTime.Now;

            inputs = InputManager.GetInputAsStrings(day, test);

            string part1 = "";
            string part2 = "";

            if (part == 1)
                part1 = Part1();
            else if (part == 2)
                part2 = Part2();
            else
            {
                part1 = Part1();
                part2 = Part2();
            }

            var ms = Math.Round((DateTime.Now - start).TotalMilliseconds);

            Console.WriteLine($"Day {day} ({ms}ms):");
            if (part1 != "") Console.WriteLine($"    {part1}");
            if (part2 != "") Console.WriteLine($"    {part2}");
        }

        static string Part1()
        {
            var start = DateTime.Now;

            long result = 0;

            #region Solution

            List<Data> memory = new List<Data>();

            var mask = "";

            foreach (var input in inputs)
            {
                if (input.Substring(0, 4) == "mask")
                {
                    mask = input.Substring(input.IndexOf('=') + 2);
                }
                else
                {
                    var location = Convert.ToInt32(input.Substring(input.IndexOf('[') + 1).Substring(0, input.IndexOf(']') - 4));

                    var bitValue = Convert.ToString(Convert.ToInt32(input.Substring(input.IndexOf('=') + 2)), 2);
                    var maskedBitValue = ApplyMask(bitValue, mask);
                    var value = Convert.ToInt64(maskedBitValue, 2);

                    if (memory.Where(m => m.Location == location).Count() > 0)
                    {
                        memory.Where(m => m.Location == location).First().Value = value;
                    }
                    else
                    {
                        memory.Add(new Data { Location = location, Value = value });
                    }
                }
            }

            result = memory.Sum(m => m.Value);

            #endregion

            var ms = Math.Round((DateTime.Now - start).TotalMilliseconds);

            if (result > 0) Answer1 = result.ToString();
            return $"Part 1 ({ms}ms): {result} ";
        }

        static string Part2()
        {
            var start = DateTime.Now;

            long result = 0;

            #region Solution

            List<Data> memory = new List<Data>();

            var mask = "";

            //var l = 0;

            foreach (var input in inputs)
            {
                //l++;
                //Console.WriteLine($"{l} => {input}");

                if (input.Substring(0, 4) == "mask")
                {
                    mask = input.Substring(input.IndexOf('=') + 2);
                }
                else
                {
                    var bitLocation = Convert.ToString(Convert.ToInt32(input.Substring(input.IndexOf('[') + 1).Substring(0, input.IndexOf(']') - 4)), 2);
                    var maskedBitLocations = ApplyMask2(bitLocation, mask);

                    var value = Convert.ToInt32(input.Substring(input.IndexOf('=') + 2));

                    foreach (var location in maskedBitLocations.Select(m => Convert.ToInt64(m, 2)))
                    {
                        if (memory.Where(m => m.Location == location).Count() > 0)
                        {
                            memory.Where(m => m.Location == location).First().Value = value;
                        }
                        else
                        {
                            memory.Add(new Data { Location = location, Value = value });
                        }
                    }
                }
            }

            result = memory.Sum(m => m.Value);

            #endregion

            var ms = Math.Round((DateTime.Now - start).TotalMilliseconds);

            if (result > 0) Answer2 = result.ToString();
            return $"Part 2 ({ms}ms): {result} ";
        }

        static string ApplyMask(string value, string mask)
        {
            string newValue = "";

            while (value.Count() < mask.Count())
            {
                value = 0 + value;
            }

            for (int i = 0; i < value.Count(); i++)
            {
                switch (mask[i])
                {
                    case '0':
                        newValue += 0;
                        break;
                    case '1':
                        newValue += 1;
                        break;
                    default:
                        newValue += value[i];
                        break;
                }
            }

            return newValue;
        }

        static List<string> ApplyMask2(string value, string mask)
        {
            List<string> newValues = new List<string>();

            while (value.Count() < mask.Count())
            {
                value = 0 + value;
            }

            List<string> possibilities = new List<string>();

            for (var x = 0; x < Math.Pow(2, mask.Count(m => m == 'X')); x++)
            {
                var pos = Convert.ToString(x, 2);

                while (pos.Count() < mask.Count(m => m == 'X'))
                {
                    pos = 0 + pos;
                }

                possibilities.Add(pos);
            }

            foreach (var pos in possibilities)
            {
                var newValue = "";
                var j = 0;

                for (int i = 0; i < value.Count(); i++)
                {
                    switch (mask[i])
                    {
                        case 'X':
                            newValue += pos[j];
                            j++;
                            break;
                        case '1':
                            newValue += 1;
                            break;
                        default:
                            newValue += value[i];
                            break;
                    }
                }

                newValues.Add(newValue);
            }

            return newValues;
        }
    }
}