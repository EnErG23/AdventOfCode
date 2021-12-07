using System.Diagnostics;

namespace AdventOfCode.Helpers
{
    internal class CommandManager
    {
        static int year = AocManager.Year;
        static int day = 0;
        static int part = 0;
        static readonly List<string> skipDays = new();

        public static void RequestInput()
        {
            Console.WriteLine();
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
                    AocManager.OpenAocAndGetInput(day);
                    break;
                case 'i': //input
                    AocManager.GetInputOnly(day);
                    break;
                case 'p': //print
                    InputManager.PrintInput(day, test);
                    break;
                case 'r': //run
                    if (inputs.Length == 1)
                        RunAllDays(test);
                    else
                        RunDay(day, part, test);
                    break;
                case 's': //submit
                    AocManager.SubmitAnswer(day);
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
            skipDays.Add("20196");
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
            skipDays.Add("202011");
            skipDays.Add("202014");
            skipDays.Add("202017");

            // Current year
            var currentDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "Eastern Standard Time").Date;
            for (int i = (currentDate.Day > 25 ? 1 : currentDate.Day + 1); i <= 25; i++)
            {
                skipDays.Add($"{currentDate.Year}{i}");
            }
        }

        public static void PrintHeader()
        {
            Console.Clear();

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("############################");
            Console.WriteLine("###                      ###");
            Console.WriteLine("###    Advent of Code    ###");
            Console.WriteLine($"###         {year}         ###");
            Console.WriteLine("###                      ###");
            Console.WriteLine("############################");
        }

        static void RunAllDays(bool test)
        {
            Stopwatch sw = Stopwatch.StartNew();

            for (int i = 1; i <= 25; i++)
                if (!skipDays.Contains($"{year}{i}"))
                {
                    RunDay(i, 0, test);
                    day = i;
                }

            sw.Stop();

            Console.WriteLine($"All days completed in {sw.Elapsed.TotalMilliseconds}ms");
            Console.WriteLine("---------------------------------------");
        }

        static void RunDay(int day, int part, bool test)
        {
            try
            {
                var zero = day < 10 ? "0" : "";
                Type dayClass = Type.GetType($"AdventOfCode.Y{year}.Days.Day{zero}{day}");
                object? instance = Activator.CreateInstance(dayClass, new object[] { test });
                var method = dayClass.GetMethod("Run");
                method.Invoke(instance, new object[] { part });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            Console.WriteLine("---------------------------------------");
        }

        static void VisualizeDay(int day, int part, bool test)
        {
            try
            {
                var zero = day < 10 ? "0" : "";
                Type dayClass = Type.GetType($"AdventOfCode.Y{year}.Days.Day{zero}{day}");
                object? instance = Activator.CreateInstance(dayClass, new object[] { test });
                var method = dayClass.GetMethod("Visualize");
                method.Invoke(instance, new object[] { part });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            Console.WriteLine();
        }
    }
}