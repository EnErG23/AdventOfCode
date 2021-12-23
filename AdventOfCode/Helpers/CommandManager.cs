using AdventOfCode.Models;
using System.Diagnostics;

namespace AdventOfCode.Helpers
{
    internal class CommandManager
    {
        static int year = AocManager.Year;
        static int day = 0;
        static int part = 0;
        static readonly List<string> skipDays = new();

        static object? lastDay;

        public static void RequestInput()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("What would you like to do?");

            string? input = Console.ReadLine().ToLower();

            HandleInput(input);
        }

        public static void HandleInput(string input)
        {
            bool test = input.Contains("test") || input.Contains("t ") || input.Contains(" t") || input.Contains(" t ");
            var inputs = input.Replace("test ", "").Replace(" test", "").Replace("t ", "").Replace(" t ", "").Replace(" t", "").Replace(" day", "").Replace(" part", "").Split(' ');
            Console.WriteLine();

            var command = inputs[0][0];
            try { day = inputs.Length > 1 ? Convert.ToInt32(inputs[1]) : day; } catch { Console.WriteLine("Wrong parameter"); return; }
            try { part = inputs.Length > 2 ? Convert.ToInt32(inputs[2]) : 0; } catch { Console.WriteLine("Wrong parameter"); return; }

            switch (command)
            {
                case 'y': //year
                    year = day < 100 ? day + 2000 : day;
                    AocManager.Year = year;
                    PrintHeader();
                    break;
                case 'o': //open
                    AocManager.OpenAoc(day);
                    break;
                case 'i': //input
                    AocManager.GetInputs(day);
                    break;
                case 'p': //print
                    InputManager.PrintInput(day, test);
                    break;
                case 'r': //run
                    if (inputs.Length == 1)
                        RunAllDays(test);
                    else
                        RunDay(day, part, test, true);
                    break;
                case 's': //submit
                    AocManager.SubmitAnswer(lastDay);
                    Thread.Sleep(2000);
                    break;
                case 'v': //visualize
                    VisualizeDay(day, part, test);
                    break;
                case 'c': //clear
                    PrintHeader();
                    break;
                case 'e': //exit
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid command");
                    break;
            }
        }

        public static void AddSkipDays()
        {
            // 2019
            skipDays.Add("20197");
            skipDays.Add("20198");
            skipDays.Add("20199");
            skipDays.Add("201910");
            skipDays.Add("201911");
            skipDays.Add("201912");
            skipDays.Add("201913");
            skipDays.Add("201914");
            skipDays.Add("201915");
            skipDays.Add("201916");
            skipDays.Add("201917");
            skipDays.Add("201918");
            skipDays.Add("201919");
            skipDays.Add("201920");
            skipDays.Add("201921");
            skipDays.Add("201922");
            skipDays.Add("201923");
            skipDays.Add("201924");
            skipDays.Add("201925");

            // 2020 - Too long
            //skipDays.Add("202011"); // 10s
            //skipDays.Add("202014"); // 33s
            skipDays.Add("202017");

            // Current year
            var currentDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "Eastern Standard Time").Date;
            for (int i = (currentDate.Day > 25 ? 1 : currentDate.Day + 1); i <= 25; i++)
            {
                skipDays.Add($"{currentDate.Year}{i}");
            }

            // 2021 - Too long
            skipDays.Add("202115"); // Hours
        }

        public static void PrintHeader()
        {
            Console.Clear();

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("╔════════════════════════╗");
            Console.WriteLine("║                        ║");
            Console.Write("║     ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Advent of Code");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("     ║");
            Console.WriteLine();
            Console.Write($"║          ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(year);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("          ║");
            Console.WriteLine();
            Console.WriteLine("║                        ║");
            Console.WriteLine("╚════════════════════════╝");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.White;
        }

        static void RunAllDays(bool test)
        {
            PrintHeader();

            Console.ForegroundColor = ConsoleColor.Green;

            PrintDayHeader();

            Stopwatch sw = Stopwatch.StartNew();

            for (int i = 1; i <= 25; i++)
                if (!skipDays.Contains($"{year}{i}"))
                {
                    RunDay(i, 0, test, false);
                    day = i;
                }

            sw.Stop();

            Console.Write($"║         ║          ║                      ║   ");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(sw.Elapsed.ToString("mm\\:ss\\.ffffff"));

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("   ║");

            Console.WriteLine();

            Console.WriteLine("╚═════════╩══════════╩══════════════════════╩══════════════════╝");
            Console.WriteLine();
        }

        static void RunDay(int day, int part, bool test, bool onlyDay)
        {
            if (onlyDay)
                PrintDayHeader();

            try
            {
                var zero = day < 10 ? "0" : "";
                Type dayClass = Type.GetType($"AdventOfCode.Y{year}.Days.Day{zero}{day}");
                lastDay = Activator.CreateInstance(dayClass, new object[] { AocManager.Year, day, test });
                var method = dayClass.GetMethod("Run");
                method.Invoke(lastDay, new object[] { part });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            if (onlyDay)
            {
                Console.WriteLine("╚═════════╩══════════╩══════════════════════╩══════════════════╝");
                Console.WriteLine();
            }
            else
                Console.WriteLine("╠═════════╬══════════╬══════════════════════╬══════════════════╣");
        }

        public static void PrintDayHeader()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("╔═════════╦══════════╦══════════════════════╦══════════════════╗");

            #region Headers

            Console.Write("║   ");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Day");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("   ║   ");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Part");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("   ║        ");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Answer");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("        ║       ");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Time");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("       ║");

            Console.WriteLine();

            #endregion

            Console.WriteLine("╠═════════╬══════════╬══════════════════════╬══════════════════╣");
        }

        static void VisualizeDay(int day, int part, bool test)
        {
            try
            {
                var zero = day < 10 ? "0" : "";
                Type dayClass = Type.GetType($"AdventOfCode.Y{year}.Days.Day{zero}{day}");
                lastDay = Activator.CreateInstance(dayClass, new object[] { AocManager.Year, day, test });
                var method = dayClass.GetMethod("Visualize");
                method.Invoke(lastDay, new object[] { part });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            Console.WriteLine();
        }
    }
}