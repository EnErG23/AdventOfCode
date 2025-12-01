using AdventOfCode.Models;

namespace AdventOfCode.Y2025.Days
{
    public class Day01 : Day
    {
        public Day01(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
        {
            long result = 0;

            long dial = 50;

            foreach (var input in Inputs)
            {
                var rot = input[0];
                var clicks = long.Parse(input[1..]);

                if (rot == 'L')
                {
                    dial -= clicks % 100;

                    if (dial < 0)
                        dial = 100 + (dial % 100);
                }
                else if (rot == 'R')
                {
                    dial += clicks % 100;

                    if (dial > 99)
                        dial = (dial % 100);
                }

                if (dial == 0)
                    result++;
            }

            return result.ToString();
        }

        public override string RunPart2()
        {
            long result = 0;

            long dial = 50;

            foreach (var input in Inputs)
            {
                var rot = input[0];
                var clicks = long.Parse(input[1..]);

                if (rot == 'L')
                {
                    var extra = dial == 0 ? 0 : 1;

                    dial -= clicks % 100;

                    if (dial < 0)
                    {
                        dial += 100;
                        result += extra;
                    }                    
                }
                else if (rot == 'R')
                {
                    dial += clicks % 100;

                    if (dial > 99)
                    {
                        dial -= 100;         
                        var extra = dial == 0 ? 0 : 1;               
                        result += extra;
                    }
                }

                result += (clicks / 100);

                if (dial == 0)
                    result++;
            }

            return result.ToString();
        }
    }
}