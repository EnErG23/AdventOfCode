using AdventOfCode.Models;

namespace AdventOfCode.Y2024.Days
{
    public class Day02 : Day
    {
        public Day02(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1() => Inputs.Count(i => CheckIfSafe(i.Split(" ").Select(i => int.Parse(i)).ToList())).ToString();

        public override string RunPart2()
        {
            var result = 0;

            foreach (var report in Inputs.Select(i => i.Split(" ").Select(i => int.Parse(i)).ToList()))
            {            
                if (CheckIfSafe(report))
                {
                    result++;
                }
                else
                {
                    for (int i = 0; i < report.Count; i++)
                    {
                        var reportCopy = report.ToList();
                        reportCopy.RemoveAt(i);

                        if (CheckIfSafe(reportCopy))
                        {
                            result++;
                            break;
                        }
                    }
                }
            }

            return result.ToString();
        }

        private bool CheckIfSafe(List<int> report)
        {
            var reportIsOrdered = report.SequenceEqual(report.OrderBy(r => r).ToList());
            var reportIsOrderedDesc = report.SequenceEqual(report.OrderByDescending(r => r).ToList());

            if (reportIsOrdered)
            {
                bool isSafe = true;

                for (int i = 0; i < report.Count - 1; i++)
                {
                    isSafe = report[i + 1] > report[i] && report[i + 1] <= report[i] + 3;

                    if (!isSafe)
                        return isSafe;
                }

                return isSafe;
            }
            else if (reportIsOrderedDesc)
            {
                bool isSafe = true;

                for (int i = 0; i < report.Count - 1; i++)
                {
                    isSafe = report[i + 1] < report[i] && report[i + 1] >= report[i] - 3;

                    if (!isSafe)
                        return isSafe;
                }

                return isSafe;
            }
            else
            {
                return false;
            }
        }
    }
}