using AdventOfCode.Models;
using System.Diagnostics;

namespace AdventOfCode.Y2020.Days
{
    public class Day05 : Day
    {
        public Day05(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
        {
            long result = 0;

            foreach (var input in Inputs)
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

			return result.ToString();
        }

        public override string RunPart2()
        {
            long result = 0;

            List<int> seatIDs = new List<int>();

            foreach (var input in Inputs)
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

			return result.ToString();
        }
    }
}