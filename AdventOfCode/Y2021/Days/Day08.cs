using AdventOfCode.Models;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2021.Days
{
    public class Day08 : Day
    {
        public Day08(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
        {
            return Inputs.Sum(i => i.Substring(i.IndexOf("|") + 2)
                                    .Split(" ")
                                    .Count(o => (o.Length > 1 && o.Length < 5) || o.Length == 7))
                         .ToString();
        }

        public override string RunPart2()
        {
            long result = 0;

            foreach (var i in Inputs)
            {
                List<string> inputs = i.Substring(0, i.IndexOf("|") - 1).Split(" ").Select(p => String.Join("", p.OrderBy(c => c))).ToList();
                List<string> outputs = i.Substring(i.IndexOf("|") + 2).Split(" ").Select(p => String.Join("", p.OrderBy(c => c))).ToList();

                List<string> digitPatterns = DetermineDigitPatterns(inputs);

                result += Convert.ToInt32(String.Join("", outputs.Select(o => digitPatterns.IndexOf(o).ToString())));
            }

            return result.ToString();
        }

        private List<string> DetermineDigitPatterns(List<string> inputs)
        {
            string[] patterns = new string[10];

            patterns[1] = inputs.First(i => i.Length == 2);
            patterns[4] = inputs.First(i => i.Length == 4);

            char tr = patterns[1][0];
            char br = patterns[1][1];
            char tl = patterns[4].First(c => !patterns[1].Contains(c));
            char m = patterns[4].Last(c => !patterns[1].Contains(c));

            patterns[0] = inputs.First(i => i.Length == 6 && (i.Contains(tr) && i.Contains(br)) && !(i.Contains(tl) && i.Contains(m)));
            patterns[2] = inputs.First(i => i.Length == 5 && !(i.Contains(tr) && i.Contains(br)) && !(i.Contains(tl) && i.Contains(m)));
            patterns[3] = inputs.First(i => i.Length == 5 && (i.Contains(tr) && i.Contains(br)) && !(i.Contains(tl) && i.Contains(m)));
            patterns[5] = inputs.First(i => i.Length == 5 && !(i.Contains(tr) && i.Contains(br)) && (i.Contains(tl) && i.Contains(m)));
            patterns[6] = inputs.First(i => i.Length == 6 && !(i.Contains(tr) && i.Contains(br)) && (i.Contains(tl) && i.Contains(m)));
            patterns[7] = inputs.First(i => i.Length == 3);
            patterns[8] = inputs.First(i => i.Length == 7);
            patterns[9] = inputs.First(i => i.Length == 6 && (i.Contains(tr) && i.Contains(br)) && (i.Contains(tl) && i.Contains(m)));

            return patterns.ToList();
        }

        public override void VisualizePart2()
        {
            long result = 0;

            foreach (var i in Inputs)
            {
                List<string> inputs = i.Substring(0, i.IndexOf("|") - 1).Split(" ").Select(p => String.Join("", p.OrderBy(c => c))).ToList();
                List<string> outputs = i.Substring(i.IndexOf("|") + 2).Split(" ").Select(p => String.Join("", p.OrderBy(c => c))).ToList();

                List<string> digitPatterns = DetermineDigitPatterns(inputs);

                PrintDisplay(digitPatterns, outputs);

                result += Convert.ToInt32(String.Join("", outputs.Select(o => digitPatterns.IndexOf(o).ToString())));
            }

            Console.WriteLine();
            Console.WriteLine($"Total: {result}");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Thread.Sleep(500);
        }

        private void PrintDisplay(List<string> digits, List<string> outputs)
        {
            List<char> line1 = " ....    ....    ....    ....    ....    ....    ....    ....    ....    .... ".Replace(".", " ").ToCharArray().ToList();
            List<char> line2 = ".    .  .    .  .    .  .    .  .    .  .    .  .    .  .    .  .    .  .    .".Replace(".", " ").ToCharArray().ToList();
            List<char> line3 = " ....    ....    ....    ....    ....    ....    ....    ....    ....    .... ".Replace(".", " ").ToCharArray().ToList();
            List<char> line4 = ".    .  .    .  .    .  .    .  .    .  .    .  .    .  .    .  .    .  .    .".Replace(".", " ").ToCharArray().ToList();
            List<char> line5 = " ....    ....    ....    ....    ....    ....    ....    ....    ....    .... ".Replace(".", " ").ToCharArray().ToList();

            List<List<char>> linesToPrint = new List<List<char>> { line1, line2, line2, line3, line4, line4, line5 };

            //Console.Clear();
            //Console.ForegroundColor = ConsoleColor.Cyan;
            //linesToPrint.ForEach(l => Console.WriteLine(String.Join("", l)));

            //Thread.Sleep(500);

            int[] solveOrder = { 1, 7, 4, 9, 0 };

            List<char> usedChars = new();

            foreach (var s in solveOrder)
            {
                Console.Clear();

                switch (s)
                {
                    case 1:
                        char a1 = digits[s].First(d => !digits[2].Contains(d));
                        char b1 = digits[s].Replace(a1.ToString(), "")[0];

                        usedChars.Add(a1);
                        usedChars.Add(b1);

                        List<int> charsToChange1a = new List<int> { 5, 13, 21, 29, 37, 61, 69, 77 };
                        List<int> charsToChange1b = new List<int> { 5, 13, 29, 37, 45, 53, 61, 69, 77 };

                        charsToChange1a.ForEach(c => line2[c] = a1);
                        charsToChange1b.ForEach(c => line4[c] = b1);

                        foreach (var l in linesToPrint)
                        {
                            foreach (var c in l)
                            {
                                if (c == a1 || c == b1)
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                else if (c != ' ' && c != '.')
                                    Console.ForegroundColor = ConsoleColor.Red;
                                else
                                    Console.ForegroundColor = ConsoleColor.Cyan;

                                Console.Write(c);
                            }
                            Console.WriteLine();
                        }

                        break;
                    case 4:
                        var remainingLetters4 = digits[s];

                        usedChars.ForEach(u => remainingLetters4 = remainingLetters4.Replace(u.ToString(), ""));

                        char a4 = remainingLetters4.First(d => !digits[2].Contains(d));
                        char b4 = remainingLetters4.Replace(a4.ToString(), "")[0];

                        usedChars.Add(a4);
                        usedChars.Add(b4);

                        List<int> charsToChange4a = new List<int> { 0, 32, 40, 48, 64, 72 };
                        List<int> charsToChange4b = new List<int> { 17, 18, 19, 20, 25, 26, 27, 28, 33, 34, 35, 36, 41, 42, 43, 44, 49, 50, 51, 52, 65, 66, 67, 68, 73, 74, 75, 76 };

                        charsToChange4a.ForEach(c => line2[c] = a4);
                        charsToChange4b.ForEach(c => line3[c] = b4);

                        foreach (var l in linesToPrint)
                        {
                            foreach (var c in l)
                            {
                                if (c == a4 || c == b4)
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                else if (c != ' ' && c != '.')
                                    Console.ForegroundColor = ConsoleColor.Red;
                                else
                                    Console.ForegroundColor = ConsoleColor.Cyan;

                                Console.Write(c);
                            }
                            Console.WriteLine();
                        }

                        break;
                    case 7:
                        var remainingLetters7 = digits[s];

                        usedChars.ForEach(u => remainingLetters7 = remainingLetters7.Replace(u.ToString(), ""));

                        char a7 = remainingLetters7[0];

                        usedChars.Add(a7);

                        List<int> charsToChange7a = new List<int> { 1, 2, 3, 4, 17, 18, 19, 20, 25, 26, 27, 28, 41, 42, 43, 44, 49, 50, 51, 52, 57, 58, 59, 60, 65, 66, 67, 68, 73, 74, 75, 76 };

                        charsToChange7a.ForEach(c => line1[c] = a7);

                        foreach (var l in linesToPrint)
                        {
                            foreach (var c in l)
                            {
                                if (c == a7)
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                else if (c != ' ' && c != '.')
                                    Console.ForegroundColor = ConsoleColor.Red;
                                else
                                    Console.ForegroundColor = ConsoleColor.Cyan;

                                Console.Write(c);
                            }
                            Console.WriteLine();
                        }

                        break;
                    case 9:
                        var remainingLetters9 = digits[s];

                        usedChars.ForEach(u => remainingLetters9 = remainingLetters9.Replace(u.ToString(), ""));

                        char a9 = remainingLetters9[0];

                        usedChars.Add(a9);

                        List<int> charsToChange9a = new List<int> { 1, 2, 3, 4, 17, 18, 19, 20, 25, 26, 27, 28, 41, 42, 43, 44, 49, 50, 51, 52, 65, 66, 67, 68, 73, 74, 75, 76 };

                        charsToChange9a.ForEach(c => line5[c] = a9);

                        foreach (var l in linesToPrint)
                        {
                            foreach (var c in l)
                            {
                                if (c == a9)
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                else if (c != ' ' && c != '.')
                                    Console.ForegroundColor = ConsoleColor.Red;
                                else
                                    Console.ForegroundColor = ConsoleColor.Cyan;

                                Console.Write(c);
                            }
                            Console.WriteLine();
                        }

                        break;
                    case 0:
                        char a0 = digits[s].First(c => !usedChars.Contains(c));

                        List<int> charsToChange0a = new List<int> { 0, 16, 48, 64 };

                        charsToChange0a.ForEach(c => line4[c] = a0);

                        foreach (var l in linesToPrint)
                        {
                            foreach (var c in l)
                            {
                                if (c == a0)
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                else if (c != ' ' && c != '.')
                                    Console.ForegroundColor = ConsoleColor.Red;
                                else
                                    Console.ForegroundColor = ConsoleColor.Cyan;

                                Console.Write(c);
                            }
                            Console.WriteLine();
                        }

                        break;
                    default:
                        break;
                }

                Thread.Sleep(500);
            }

            Console.Clear();

            foreach (var l in linesToPrint)
            {
                foreach (var c in l)
                {
                    if (c != ' ' && c != '.')
                        Console.ForegroundColor = ConsoleColor.Red;
                    else
                        Console.ForegroundColor = ConsoleColor.Cyan;

                    Console.Write(c);
                }
                Console.WriteLine();
            }

            Thread.Sleep(1000);

            var toAdd = "";

            foreach (var o in outputs)
            {
                var digit = digits.IndexOf(o);

                Console.Clear();

                foreach (var l in linesToPrint)
                {
                    for (int c = 0; c < l.Count; c++)
                    {
                        if (c >= digit * 8 && c < (digit + 1) * 8 && l[c] != ' ' && l[c] != '.')
                            Console.ForegroundColor = ConsoleColor.Green;
                        else if (l[c] != ' ' && l[c] != '.')
                            Console.ForegroundColor = ConsoleColor.Red;
                        else
                            Console.ForegroundColor = ConsoleColor.Cyan;

                        Console.Write(l[c]);
                    }
                    Console.WriteLine();
                }

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{o} => {digit}");
                Console.WriteLine();
                Console.WriteLine($"{(toAdd == "" ? 0 : toAdd)}");

                Thread.Sleep(1000);

                toAdd += digit;

                Console.Clear();

                foreach (var l in linesToPrint)
                {
                    foreach (var c in l)
                    {
                        if (c != ' ' && c != '.')
                            Console.ForegroundColor = ConsoleColor.Red;
                        else
                            Console.ForegroundColor = ConsoleColor.Cyan;

                        Console.Write(c);
                    }
                    Console.WriteLine();
                }

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{toAdd}");

                Thread.Sleep(500);
            }

            Thread.Sleep(500);
        }
    }
}