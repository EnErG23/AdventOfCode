using AdventOfCode.Models;
using AdventOfCode.Helpers;
using AdventOfCode.Y2021.Models;

namespace AdventOfCode.Y2021.Days
{
    public class Day04 : Day
    {
        static List<Board>? boards;
        static List<int>? drawnNumbers;

        public Day04(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
        {
            InputToBoards();

            foreach (var drawnNumber in drawnNumbers)
            {                
                boards.ForEach(b => b.Rows.ForEach(r => r.ForEach(n => n.Marked = n.Value == drawnNumber ? true : n.Marked)));

                if (boards.Any(b => b.IsWinner))                
                    return (drawnNumber * boards.First(b => b.IsWinner).UnmarkedSum).ToString();
            }

            return "";
        }

        public override string RunPart2()
        {
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
                    return (drawnNumber * boards[0].UnmarkedSum).ToString();
            }

            return "";
        }

        private void InputToBoards()
        {
            boards = new List<Board>();

            drawnNumbers = Inputs[0].Split(',').Select(i => int.Parse(i)).ToList();

            var board = new Board();

            foreach (var input in Inputs.Skip(2))
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

        public override void VisualizePart1()
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

        public override void VisualizePart2()
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