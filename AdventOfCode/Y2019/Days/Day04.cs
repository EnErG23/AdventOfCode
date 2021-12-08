using AdventOfCode.Models;

namespace AdventOfCode.Y2019.Days
{
    public class Day04 : Day
    {
        static readonly int day = 4;
        static List<int>? range;

        public Day04(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
        {
            range = Inputs[0].Split("-").Select(i => int.Parse(i)).ToList();

            long result = 0;

            for (int i = range[0]; i < range[1]; i++)
            {
                var password = i.ToString();

                var req4 = true;

                for (int j = 1; j < password.Length; j++)
                    if (password[j - 1] > password[j])
                    {
                        req4 = false;
                        break;
                    }

                if (req4)
                    for (int j = 1; j < password.Length; j++)
                        if (password[j - 1] == password[j])
                        {
                            result++;
                            break;
                        }
            }

            return result.ToString();
        }

        public override string RunPart2()
        {
            if (range is null)
                range = Inputs[0].Split("-").Select(i => int.Parse(i)).ToList();

            long result = 0;

            for (int i = range[0]; i < range[1]; i++)
            {
                var password = i.ToString();

                var req3 = false;
                var req4 = true;
                var req5 = false;

                for (int j = 0; j < password.Length - 1; j++)
                    if (password[j] > password[j + 1])
                    {
                        req4 = false;
                        break;
                    }

                if (req4)
                {
                    for (int j = 0; j < password.Length - 1; j++)
                    {
                        if (password.Where(p => p == password[j]).Count() > 1)
                            req3 = true;
                        if (password.Where(p => p == password[j]).Count() == 2)
                            req5 = true;
                    }
                    if (req3 & req5)
                        result++;
                }
            }

            return result.ToString();
        }
    }
}