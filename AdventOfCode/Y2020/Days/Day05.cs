using AdventOfCode.Helpers;

namespace AdventOfCode.Y2020.Days
{
    public static class Day05
    {
        static int day = 5;
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

            foreach (var input in inputs)
            {
                var seatID = 0;

                List<int> rows = Enumerable.Range(0, 128).ToList();

                for (int i = 8; i > 1; i--)
                {
                    if (input[8 - i] == 'F')
                    {
                        rows = rows.GetRange(0, rows.Count() / 2);
                    }
                    else
                    {
                        rows = rows.GetRange(rows.Count() / 2, rows.Count() / 2);
                    }
                    //Console.WriteLine(rows[0]);
                }

                List<int> columns = Enumerable.Range(0, 8).ToList();

                for (int i = 4; i > 1; i--)
                {
                    if (input.Substring(7)[4 - i] == 'L')
                    {
                        columns = columns.GetRange(0, columns.Count() / 2);
                    }
                    else
                    {
                        columns = columns.GetRange(columns.Count() / 2, columns.Count() / 2);
                    }
                    //Console.WriteLine(columns[0]);
                }

                seatID = (rows[0] * 8) + columns[0];

                result = Math.Max(result, seatID);
            }

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

            List<int> seatIDs = new List<int>();

            foreach (var input in inputs)
            {
                var seatID = 0;

                List<int> rows = Enumerable.Range(0, 128).ToList();

                for (int i = 8; i > 1; i--)
                {
                    if (input[8 - i] == 'F')
                    {
                        rows = rows.GetRange(0, rows.Count() / 2);
                    }
                    else
                    {
                        rows = rows.GetRange(rows.Count() / 2, rows.Count() / 2);
                    }
                }

                List<int> columns = Enumerable.Range(0, 8).ToList();

                for (int i = 4; i > 1; i--)
                {
                    if (input.Substring(7)[4 - i] == 'L')
                    {
                        columns = columns.GetRange(0, columns.Count() / 2);
                    }
                    else
                    {
                        columns = columns.GetRange(columns.Count() / 2, columns.Count() / 2);
                    }
                }

                seatID = (rows[0] * 8) + columns[0];

                seatIDs.Add(seatID);
            }

            List<int> allIDS = Enumerable.Range(9, 1031).ToList();

            result = allIDS.Where(a => a > 79 && a < 927).Where(a => !seatIDs.Contains(a)).First();

            #endregion

            var ms = Math.Round((DateTime.Now - start).TotalMilliseconds);

            if (result > 0) Answer2 = result.ToString();
            return $"Part 2 ({ms}ms): {result} ";
        }
    }
}