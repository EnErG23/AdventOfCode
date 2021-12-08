using AdventOfCode.Models;
using System.Diagnostics;
namespace AdventOfCode.Y2020.Days
{
    public class Day06 : Day
    {
        public Day06(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
        {
            long result = 0;

            List<string> passports = new List<string>();
            string passport = "";

            foreach (string input in Inputs)
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

			return result.ToString();
        }

        public override string RunPart2()
        {
            long result = 0;

            List<string> passports = new List<string>();
            string passport = "";

            bool reset = true;

            foreach (string input in Inputs)
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

			return result.ToString();
        }
    }
}