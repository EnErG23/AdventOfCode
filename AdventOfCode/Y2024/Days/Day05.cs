using AdventOfCode.Models;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2024.Days
{
    public class Day05 : Day
    {
        public Day05(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
        {
            int result = 0;

            List<(int, int)> pageRules = new List<(int, int)>();

            var switchInput = false;

            foreach (var input in Inputs)
            {
                if (input == "")
                {
                    switchInput = true;
                    continue;
                }

                if (switchInput)
                {
                    var pages = input.Split(',').Select(i => int.Parse(i)).ToList();

                    if (IsCorrectOrder(pageRules, pages))
                        result += pages[pages.Count() / 2];
                }
                else
                {
                    pageRules.Add((int.Parse(input.Split('|')[0]), int.Parse(input.Split('|')[1])));
                }
            }

            return result.ToString();
        }

        public override string RunPart2()
        {
            int result = 0;

            List<(int, int)> pageRules = new List<(int, int)>();

            var switchInput = false;

            foreach (var input in Inputs)
            {
                if (input == "")
                {
                    switchInput = true;
                    continue;
                }

                if (switchInput)
                {
                    var pages = input.Split(',').Select(i => int.Parse(i)).ToList();

                    if (!IsCorrectOrder(pageRules, pages))
                    {
                        Console.WriteLine($"Before: { string.Join(", ", pages)}");
                        pages = FixOrder(pageRules, pages);
                        Console.WriteLine($"After: {string.Join(", ", pages)}");
                        result += pages[pages.Count() / 2];
                    }
                }
                else
                {
                    pageRules.Add((int.Parse(input.Split('|')[0]), int.Parse(input.Split('|')[1])));
                }
            }

            return result.ToString();
        }

        private bool IsCorrectOrder(List<(int, int)> pageRules, List<int> pages)
        {
            for (int i = 1; i < pages.Count(); i++)
            {
                for (int j = 0; j < i; j++)
                {
                    foreach (var pageRule in pageRules.Where(p => p.Item1 == pages[i]))
                    {
                        if (pageRule.Item2 == pages[j])
                            return false;
                    }
                }
            }

            return true;
        }

        private List<int> FixOrder(List<(int, int)> pageRules, List<int> pages)
        {
            List<int> newPages = pages.ToList();

            for (int i = 1; i < pages.Count(); i++)
            {
                for (int j = 0; j < i; j++)
                {
                    foreach (var pageRule in pageRules.Where(p => p.Item1 == pages[i]))
                    {
                        if (pageRule.Item2 == pages[j])
                        {
                            newPages[i] = pages[j];
                            newPages[j] = pages[i];

                            if (IsCorrectOrder(pageRules, newPages))
                                return newPages;
                            else
                                return FixOrder(pageRules, newPages);
                        }
                    }
                }
            }

            if (IsCorrectOrder(pageRules, newPages))
                return newPages;

            return FixOrder(pageRules, newPages);
        }
    }
}