using AdventOfCode.Helpers;
namespace AdventOfCode.Y2020.Days
{
    public static class Day06
    {
        static int day = 6;
        static List<string>? inputs;

        public static string? Answer1 { get; set; }
        public static string? Answer2 { get; set; }

        public static void Run(int part, bool test)
        {
            inputs = InputManager.GetInputAsStrings(day, test);

            var start = DateTime.Now;

            string part1 = "";
            string part2 = "";

            switch (part)
            {
                case 1:
                    part1 = Part1();
                    break;
                case 2:
                    part2 = Part2();
                    break;
                default:
                    part1 = Part1();
                    part2 = Part2();
                    break;
            }

            var ms = Math.Round((DateTime.Now - start).TotalMilliseconds);

            Console.WriteLine($"Day {day} ({ms}ms):");
            if (part1 != "") Console.WriteLine($"    {part1}");
            if (part2 != "") Console.WriteLine($"    {part2}");
        }

        static string Part1()
        {
            long result = 0;

            var start = DateTime.Now;

            #region Solution

            List<string> passports = new List<string>();
            string passport = "";

            foreach (string input in inputs)
            {
                if (input == "")
                {
                    passports.Add(passport);
                    passport = "";
                }
                passport += input;
            }

            passports.Add(passport);

            result = passports.Select(p => p.Distinct().Count()).Sum();

            #endregion

            var ms = Math.Round((DateTime.Now - start).TotalMilliseconds);

            if (result > 0) Answer1 = result.ToString();
            return $"Part 1 ({ms}ms): {result} ";
        }

        static string Part2()
        {
            long result = 0;

            var start = DateTime.Now;

            #region Solution

            List<string> passports = new List<string>();
            string passport = "";

            bool reset = true;

            foreach (string input in inputs)
            {
                if (input == "")
                {
                    passports.Add(passport);
                    passport = "";
                    reset = true;
                }
                else
                {
                    if (reset)
                    {
                        passport = input;
                        reset = false;
                    }
                    else
                    {
                        var ppLetters = passport.Distinct();
                        foreach (var letter in ppLetters)
                        {
                            if (!input.Contains(letter))
                            {
                                passport = passport.Replace(letter.ToString(), "");
                            }
                        }

                        var letters = input.Distinct();
                        foreach (var letter in letters)
                        {
                            if (!passport.Contains(letter))
                            {
                                passport = passport.Replace(letter.ToString(), "");
                            }
                        }
                    }
                }
            }

            passports.Add(passport);

            result = passports.Select(p => p.Distinct().Count()).Sum();

            #endregion

            var ms = Math.Round((DateTime.Now - start).TotalMilliseconds);

            if (result > 0) Answer2 = result.ToString();
            return $"Part 2 ({ms}ms): {result} ";
        }
    }
}