using AdventOfCode.Helpers;
using AdventOfCode.Y2021.Models;

namespace AdventOfCode.Y2021.Days
{
    public static class Day04
    {
        static int day = 4;
        static List<string>? inputs;
        static List<Board>? boards;
        static List<int>? drawnNumbers;

        public static string? Answer1 { get; set; }
        public static string? Answer2 { get; set; }

        public static void Run(int part, bool test)
        {
            var start = DateTime.Now;

            inputs = InputManager.GetInputAsStrings(day, test);

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

            InputToBoards();

            foreach (var drawnNumber in drawnNumbers)
            {
                boards.ForEach(b => b.Rows.ForEach(r => r.ForEach(n => n.Marked = n.Value == drawnNumber ? true : n.Marked)));

                if (boards.Any(b => b.IsWinner))
                {
                    result = drawnNumber * boards.First(b => b.IsWinner).UnmarkedSum;
                    break;
                }
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

            InputToBoards();

            foreach (var drawnNumber in drawnNumbers)
            {
                boards.ForEach(b => b.Rows.ForEach(r => r.ForEach(n => n.Marked = n.Value == drawnNumber ? true : n.Marked)));

                if (boards.Count > 1)
                {
                    boards = boards.Where(b => !b.IsWinner).ToList();
                    continue;
                }

                if (boards[0].IsWinner)
                {
                    result = drawnNumber * boards[0].UnmarkedSum;
                    break;
                }
            }

            #endregion

            var ms = Math.Round((DateTime.Now - start).TotalMilliseconds);

            if (result > 0) Answer2 = result.ToString();
            return $"Part 2 ({ms}ms): {result} ";
        }

        static void InputToBoards()
        {
            boards = new List<Board>();

            drawnNumbers = inputs[0].Split(',').Select(i => int.Parse(i)).ToList();

            var board = new Board();

            foreach (var input in inputs.Skip(2))
            {
                if (input == "")
                {
                    boards.Add(board);
                    board = new Board();
                    continue;
                }

                board.Rows.Add((input[0] == ' ' ? input.Substring(1) : input).Replace("  ", " ").Split(" ").Select(i => new Number { Value = int.Parse(i) }).ToList());
            }

            boards.Add(board);
        }

        // Visualize
        public static void Visualize(int part, bool test)
        {
            inputs = InputManager.GetInputAsStrings(day, test);

            Console.Clear();

            if (part == 1)
                VisualizePart1();
            else if (part == 2)
                VisualizePart2();
            else
            {
                VisualizePart1();
                Thread.Sleep(5000);
                VisualizePart2();
            }

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Cyan;
        }

        static void VisualizePart1()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            for (int i = 3; i > 0; i--)
            {
                Console.Clear();
                Console.WriteLine($"Visualization for 2021.4.1");
                Console.WriteLine($"Starting in {i}");
                Thread.Sleep(1000);
            }

            InputToBoards();

            foreach (var drawnNumber in drawnNumbers)
            {
                Console.Clear();
                boards.ForEach(b => PrintBoard(b, false));
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Number drawn: {drawnNumber}");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Cyan;

                boards.ForEach(b => b.Rows.ForEach(r => r.ForEach(n => n.Marked = n.Value == drawnNumber ? true : n.Marked)));

                Thread.Sleep(1000);

                Console.Clear();
                boards.ForEach(b => PrintBoard(b, false));

                if (boards.Any(b => b.IsWinner))
                {
                    Thread.Sleep(1000);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("! BINGO !");
                    Console.WriteLine();
                    PrintBoard(boards.First(b => b.IsWinner), true);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"{drawnNumber} * {boards.First(b => b.IsWinner).UnmarkedSum} = {drawnNumber * boards.First(b => b.IsWinner).UnmarkedSum}");
                    break;
                }
            }
        }

        static void VisualizePart2()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            for (int i = 3; i > 0; i--)
            {
                Console.Clear();
                Console.WriteLine($"Visualization for 2021.4.2");
                Console.WriteLine($"Starting in {i}");
                Thread.Sleep(1000);
            }

            InputToBoards();

            foreach (var drawnNumber in drawnNumbers)
            {
                Console.Clear();
                boards.ForEach(b => PrintBoard(b, false));
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Number drawn: {drawnNumber}");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Cyan;

                boards.ForEach(b => b.Rows.ForEach(r => r.ForEach(n => n.Marked = n.Value == drawnNumber ? true : n.Marked)));

                Thread.Sleep(1000);

                Console.Clear();
                boards.ForEach(b => PrintBoard(b, false));

                if (boards.Count > 1)
                {
                    boards = boards.Where(b => !b.IsWinner).ToList();
                    continue;
                }

                if (boards[0].IsWinner)
                {
                    Thread.Sleep(1000);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("! LAST BINGO !");
                    Console.WriteLine();
                    PrintBoard(boards[0], true);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"{drawnNumber} * {boards[0].UnmarkedSum} = {drawnNumber * boards[0].UnmarkedSum}");
                    break;
                }
            }
        }

        static void PrintBoard(Board b, bool isWinner)
        {
            foreach (var r in b.Rows)
            {
                foreach (var n in r)
                {

                    if (n.Marked)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write($" X ");
                    }
                    else
                    {
                        Console.ForegroundColor = isWinner ? ConsoleColor.Green : ConsoleColor.Cyan;
                        Console.Write($"{(n.Value > 9 ? "" : " ")}{n.Value} ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}