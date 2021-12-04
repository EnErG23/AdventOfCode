using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Y2021.Days
{
    public static class Day04
    {
        static int day = 4;
        static List<string>? inputs;

        public static string? Answer1 { get; set; }
        public static string? Answer2 { get; set; }

        public static void Run(int part, bool test)
        {
            inputs = InputManager.GetInputAsStrings(day, test);

            var start = DateTime.Now;

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
            long result = 0;

            var start = DateTime.Now;

            #region Solution

            var drawnNumbers = inputs[0].Split(',').Select(i => int.Parse(i)).ToList();

            var boards = new List<Board>();
            var board = new Board();

            foreach (var input in inputs.Skip(2))
            {
                if (input == "")
                {
                    boards.Add(board);
                    board = new Board();
                    continue;
                }

                if (input[0] == ' ')
                    board.Rows.Add(input.Substring(1).Replace("  ", " ").Split(' ').Select(i => new Number { Value = int.Parse(i), Marked = false }).ToList());
                else
                    board.Rows.Add(input.Replace("  ", " ").Split(' ').Select(i => new Number { Value = int.Parse(i), Marked = false }).ToList());
            }
            boards.Add(board);

            var winningNumber = 0;
            var winningBoard = new Board();

            foreach (var drawnNumber in drawnNumbers)
            {
                foreach (var b in boards)
                {
                    foreach (var r in b.Rows)
                    {
                        foreach (var n in r)
                        {
                            if (n.Value == drawnNumber)
                                n.Marked = true;
                        }
                    }

                    if (CheckBoardForWin(b, false))
                    {
                        winningBoard = b;
                        break;
                    }
                }

                if (winningBoard.Rows.Count > 0)
                {
                    winningNumber = drawnNumber;
                    break;
                }
            }

            var sum = 0;

            foreach (var row in winningBoard.Rows)
            {
                sum += row.Where(n => !n.Marked).Sum(n => n.Value);
            }

            result = sum * winningNumber;

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

            var drawnNumbers = inputs[0].Split(',').Select(i => int.Parse(i)).ToList();

            var boards = new List<Board>();
            var board = new Board();

            foreach (var input in inputs.Skip(2))
            {
                if (input == "")
                {
                    boards.Add(board);
                    board = new Board();
                    continue;
                }

                if (input[0] == ' ')
                    board.Rows.Add(input.Substring(1).Replace("  ", " ").Split(' ').Select(i => new Number { Value = int.Parse(i), Marked = false }).ToList());
                else
                    board.Rows.Add(input.Replace("  ", " ").Split(' ').Select(i => new Number { Value = int.Parse(i), Marked = false }).ToList());
            }
            boards.Add(board);

            var winningNumber = 0;
            var winningBoard = new Board();
            var winningBoards = 0;

            foreach (var drawnNumber in drawnNumbers)
            {
                foreach (var b in boards.Where(z => z.Winner == false).ToList())
                {                    
                    foreach (var r in b.Rows)
                    {
                        foreach (var n in r)
                        {
                            if (n.Value == drawnNumber)
                                n.Marked = true;
                        }
                    }

                    if (CheckBoardForWin(b, false))
                    {
                        b.Winner = true;
                        winningBoard = b;
                        winningBoards++;
                    }
                }

                if (winningBoards == boards.Count)
                {
                    winningNumber = drawnNumber;
                    break;
                }
            }

            var sum = 0;

            foreach (var row in winningBoard.Rows)
            {
                sum += row.Where(n => !n.Marked).Sum(n => n.Value);
            }            

            result = sum * winningNumber;

            #endregion

            var ms = Math.Round((DateTime.Now - start).TotalMilliseconds);

            if (result > 0) Answer2 = result.ToString();
            return $"Part 2 ({ms}ms): {result} ";
        }

        static bool CheckBoardForWin(Board b, bool checkDiags)
        {
            // Check rows
            foreach (var r in b.Rows)
                if (r.Where(x => x.Marked == false).Count() == 0)
                    return true;

            // Check cols
            for (int i = 0; i < b.Rows[0].Count; i++)
                if (b.Rows.Select(r => r[i]).Where(x => x.Marked == false).Count() == 0)
                    return true;

            // FUCKING CHECK USELESS FUCKING DIAGS
            if (checkDiags)
            {
                // Check diags
                var x1 = 0;
                var x2 = b.Rows[0].Count - 1;

                var diag1Marked = true;
                var diag2Marked = true;

                foreach (var r in b.Rows)
                {
                    if (!r[x1++].Marked)
                        diag1Marked = false;
                    if (!r[x2--].Marked)
                        diag2Marked = false;
                }

                if (diag1Marked)
                    return true;

                if (diag2Marked)
                    return true;
            }

            return false;
        }

        // Visualize

        static void PrintBoard(Board b)
        {
            Console.WriteLine();

            foreach (var r in b.Rows)
            {
                var str = "";
                foreach (var n in r)
                {
                    str += n.Marked ? "X " : $"{n.Value} ";
                }
                Console.WriteLine(str);
            }
            Console.WriteLine();
        }

    }

    public class Board
    {
        public List<List<Number>> Rows { get; set; } = new List<List<Number>>();
        public bool Winner { get; set; } = false;
    }

    public class Number
    {
        public int Value { get; set; } = 0;
        public bool Marked { get; set; } = false;
    }
}