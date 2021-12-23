using AdventOfCode.Models;

namespace AdventOfCode.Y2021.Days
{
    public class Day13 : Day
    {
        private List<int>? rValues;
        private List<int>? cValues;
        private List<string>? folds;
        private bool[,]? page;

        public Day13(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
        {
            CreatePage();

            page = Fold(page, folds[0]);

            long result = 0;

            for (int r = 0; r < page.GetLength(0); r++)
                for (int c = 0; c < page.GetLength(1); c++)
                    if (page[r, c])
                        result++;

            return result.ToString();
        }

        public override string RunPart2()
        {
            if (page is null)
            {
                CreatePage();

                foreach (var fold in folds)
                    page = Fold(page, fold);
            }
            else
                foreach (var fold in folds.Skip(1))
                    page = Fold(page, fold);

            //PrintPage(page);

            return "Vis. for answer";
        }

        private void CreatePage()
        {
            rValues = new();
            cValues = new();
            folds = new();

            bool dotCoords = true;

            foreach (var input in Inputs)
            {
                if (input == "")
                {
                    dotCoords = false;
                    continue;
                }

                if (dotCoords)
                {
                    rValues.Add(int.Parse(input.Split(",")[1]));
                    cValues.Add(int.Parse(input.Split(",")[0]));
                }
                else
                {
                    folds.Add(input.Replace("fold along ", ""));
                }
            }

            page = new bool[rValues.Max() + 1, cValues.Max() + 1];

            for (int i = 0; i < rValues.Count; i++)
                page[rValues[i], cValues[i]] = true;
        }

        private void PrintPage(bool[,] page)
        {
            for (int r = 0; r < page.GetLength(0); r++)
            {
                for (int c = 0; c < page.GetLength(1); c++)
                {
                    Console.Write(page[r, c] ? "█" : " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private bool[,] Fold(bool[,] page, string fold)
        {
            var dir = fold.Split("=")[0];
            var line = int.Parse(fold.Split("=")[1]);

            if (dir == "y")
                return FoldHorizontally(page, line);
            else
                return FoldVertically(page, line);
        }

        private bool[,] FoldHorizontally(bool[,] page, int line)
        {
            bool[,] newPage = new bool[line, page.GetLength(1)];

            for (int r = 0; r < newPage.GetLength(0); r++)
                for (int c = 0; c < newPage.GetLength(1); c++)
                    newPage[r, c] = (page[r, c] || page[page.GetLength(0) - r - 1, c]);

            return newPage;
        }

        private bool[,] FoldVertically(bool[,] page, int line)
        {
            bool[,] newPage = new bool[page.GetLength(0), line];

            for (int r = 0; r < newPage.GetLength(0); r++)
                for (int c = 0; c < newPage.GetLength(1); c++)
                    newPage[r, c] = (page[r, c] || page[r, page.GetLength(1) - c - 1]);

            return newPage;
        }

        public override void VisualizePart2()
        {
            CreatePage();

            Console.ForegroundColor = ConsoleColor.Red;

            Console.Clear();
            PrintPage(page);
            Thread.Sleep(1000);

            foreach (var fold in folds)
            {
                page = Fold(page, fold);

                Console.Clear();
                PrintPage(page);
                Thread.Sleep(1000);
            }
        }
    }
}