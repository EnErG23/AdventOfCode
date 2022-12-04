using AdventOfCode.Models;

namespace AdventOfCode.Y2022.Days
{
    public class Day04 : Day
    {
        public Day04(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
        {
            var assignmentPairs = Inputs;

            int overlaps = 0;
            
            foreach(var assignmentPair in assignmentPairs)
            {
                var a1s1 = int.Parse(assignmentPair.Split(",")[0].Split("-")[0]);
                var a1s2 = int.Parse(assignmentPair.Split(",")[0].Split("-")[1]);
                var a2s1 = int.Parse(assignmentPair.Split(",")[1].Split("-")[0]);
                var a2s2 = int.Parse(assignmentPair.Split(",")[1].Split("-")[1]);

                if ((a1s1 <= a2s1 && a1s2 >= a2s2) || (a1s1 >= a2s1 && a1s2 <= a2s2))
                    overlaps++;
            }

            return overlaps.ToString();
        }

        public override string RunPart2()
        {
            var assignmentPairs = Inputs;

            int overlaps = 0;

            foreach (var assignmentPair in assignmentPairs)
            {
                var a1s1 = int.Parse(assignmentPair.Split(",")[0].Split("-")[0]);
                var a1s2 = int.Parse(assignmentPair.Split(",")[0].Split("-")[1]);
                var a2s1 = int.Parse(assignmentPair.Split(",")[1].Split("-")[0]);
                var a2s2 = int.Parse(assignmentPair.Split(",")[1].Split("-")[1]);

                if (!(a1s1 > a2s2 || a1s2 < a2s1))
                    overlaps++;
            }

            return overlaps.ToString();
        }
    }
}