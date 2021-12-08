using AdventOfCode.Models;
using System.Diagnostics;

namespace AdventOfCode.Y2020.Days
{
    public class Day04 : Day
    {
        public Day04(int year, int day, bool test) : base(year, day, test) { }

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
                passport += " " + input;
            }

            passports.Add(passport);

            foreach (string p in passports)
            {
                if (p.Contains("byr:") && p.Contains("iyr:") && p.Contains("eyr:") && p.Contains("hgt:") && p.Contains("hcl:") && p.Contains("ecl:") && p.Contains("pid:"))
                {
                    result++;
                }
            }

			return result.ToString();
        }

        public override string RunPart2()
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
                passport += " " + input;
            }

            passports.Add(passport);

            foreach (string p in passports)
            {
                //Console.WriteLine(p);
                if (p.Contains("byr:") && p.Contains("iyr:") && p.Contains("eyr:") && p.Contains("hgt:") && p.Contains("hcl:") && p.Contains("ecl:") && p.Contains("pid:"))
                {
                    bool valid = true;

                    var fields = p.Replace("  ", " ").Substring(1).Split(' ');

                    foreach (var field in fields)
                    {
                        var key = field.Substring(0, field.IndexOf(':'));
                        var value = field.Substring(field.IndexOf(':') + 1);

                        int i = 0;

                        switch (key)
                        {
                            case "byr":
                                if (int.TryParse(value, out i))
                                {
                                    if (Convert.ToInt32(value) > 1919 && Convert.ToInt32(value) < 2003)
                                    {
                                        valid = true;
                                    }
                                    else
                                    {
                                        //Console.WriteLine($"{key}:{value}");
                                        valid = false;
                                    }
                                }
                                else
                                {
                                    valid = false;
                                    //Console.WriteLine($"{key}:{value}");
                                }
                                break;
                            case "iyr":
                                if (int.TryParse(value, out i))
                                {
                                    if (Convert.ToInt32(value) > 2009 && Convert.ToInt32(value) < 2021)
                                    {
                                        valid = true;
                                    }
                                    else
                                    {
                                        //Console.WriteLine($"{key}:{value}");
                                        valid = false;
                                    }
                                }
                                else
                                {
                                    //Console.WriteLine($"{key}:{value}");
                                    valid = false;
                                }
                                break;
                            case "eyr":
                                if (int.TryParse(value, out i))
                                {
                                    if (Convert.ToInt32(value) > 2019 && Convert.ToInt32(value) < 2031)
                                    {
                                        valid = true;
                                    }
                                    else
                                    {
                                        //Console.WriteLine($"{key}:{value}");
                                        valid = false;
                                    }
                                }
                                else
                                {
                                    //Console.WriteLine($"{key}:{value}");
                                    valid = false;
                                }
                                break;
                            case "hgt":
                                if (value.Contains("cm"))
                                {
                                    if (int.TryParse(value.Replace("cm", ""), out i))
                                    {
                                        if (Convert.ToInt32(value.Replace("cm", "")) > 149 && Convert.ToInt32(value.Replace("cm", "")) < 194)
                                        {
                                            valid = true;
                                        }
                                        else
                                        {
                                            //Console.WriteLine($"{key}:{value}");
                                            valid = false;
                                        }
                                    }
                                }
                                else if (value.Contains("in"))
                                {
                                    if (int.TryParse(value.Replace("in", ""), out i))
                                    {
                                        if (Convert.ToInt32(value.Replace("in", "")) > 58 && Convert.ToInt32(value.Replace("in", "")) < 77)
                                        {
                                            valid = true;
                                        }
                                        else
                                        {
                                            //Console.WriteLine($"{key}:{value}");
                                            valid = false;
                                        }
                                    }
                                }
                                else
                                {
                                    //Console.WriteLine($"{key}:{value}");
                                    valid = false;
                                }
                                break;
                            case "hcl":
                                if (value.Substring(0, 1) == "#")
                                {
                                    if (value.Substring(1).Count() == 6)
                                    {
                                        valid = true;
                                    }
                                    else
                                    {
                                        //Console.WriteLine($"{key}:{value}");
                                        valid = false;
                                    }
                                }
                                else
                                {
                                    //Console.WriteLine($"{key}:{value}");
                                    valid = false;
                                }
                                break;
                            case "ecl":
                                List<string> possibleValues = new List<string>() { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };

                                if (possibleValues.Contains(value))
                                {
                                    valid = true;
                                }
                                else
                                {
                                    //Console.WriteLine($"{key}:{value}");
                                    valid = false;
                                }
                                break;
                            case "pid":
                                if (int.TryParse(value, out i))
                                {
                                    if (value.Count() == 9)
                                    {
                                        valid = true;
                                    }
                                    else
                                    {
                                        //Console.WriteLine($"{key}:{value}");
                                        valid = false;
                                    }
                                }
                                else
                                {
                                    //Console.WriteLine($"{key}:{value}");
                                    valid = false;
                                }
                                break;
                            default:
                                break;
                        }

                        if (!valid) break;
                    }

                    if (valid)
                    {
                        result++;
                    }
                }
            }

			return result.ToString();
        }
    }
}