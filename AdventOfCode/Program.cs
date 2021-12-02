using AdventOfCode.Helpers;

var year = AocManager.Year;
var day = 0;
var part = 0;

var skipDays = new List<string>();
AddSkipDays();

PrintHeader();

while (true)
{
    Console.WriteLine();
    Console.WriteLine("What would you like to do?");

    string? input = Console.ReadLine().ToLower();
    bool test = input.Contains("test");
    var inputs = input.Replace("test ", "").Replace(" test", "").Replace(" day", "").Replace(" part", "").Replace(" year", "").Split(' ');
    Console.WriteLine();

    var command = inputs[0];
    day = inputs.Length > 1 ? Convert.ToInt32(inputs[1]) : day;
    part = inputs.Length > 2 ? Convert.ToInt32(inputs[2]) : 0;

    switch (command)
    {
        case "year":
            year = day;
            AocManager.Year = year;
            Console.Clear();
            PrintHeader();
            break;
        case "read":
            AocManager.OpenAoc(day);
            break;
        case "print":
            InputManager.PrintInput(day, test);
            break;
        case "run":
            if (inputs.Length == 1)
                RunAllDays(test);
            else
                RunDay(day, part, test);
            break;
        case "submit":
            AocManager.SubmitAnswer(day);
            Thread.Sleep(2000);
            break;
        case "visualize":
            VisualizeDay(day, part, test);
            break;
        case "clear":
            Console.Clear();
            PrintHeader();
            break;
        case "exit": 
            return;
        default:
            Console.WriteLine("Invalid command");
            break;
    }
}

void AddSkipDays()
{
    // 2019    
    skipDays.Add("20195");
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

void PrintHeader()
{
    Console.WriteLine("############################");
    Console.WriteLine("###                      ###");
    Console.WriteLine("###    Advent of Code    ###");
    Console.WriteLine($"###         {year}         ###");
    Console.WriteLine("###                      ###");
    Console.WriteLine("############################");
}

void RunAllDays(bool test)
{
    DateTime start = DateTime.Now;

    for (int i = 1; i <= 25; i++)
        if (!skipDays.Contains($"{year}{i}"))
        {
            RunDay(i, 0, test);
            day = i;
        }

    var time = DateTime.Now - start;

    Console.WriteLine($"All days completed in {time.Minutes}m {time.Seconds}s {time.Milliseconds}ms");
    Console.WriteLine("---------------------------------------");
}

void RunDay(int day, int part, bool test)
{
    try
    {
        var zero = day < 10 ? "0" : "";
        Type? dayClass = Type.GetType($"AdventOfCode.Y{year}.Days.Day{zero}{day}");
        var method = dayClass.GetMethod("Run");
        method.Invoke(null, new object[] { part, test });
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
    }

    Console.WriteLine("---------------------------------------");
}

void VisualizeDay(int day, int part, bool test)
{
    try
    {
        var zero = day < 10 ? "0" : "";
        Type? dayClass = Type.GetType($"AdventOfCode.Y{year}.Days.Day{zero}{day}");
        var method = dayClass.GetMethod("Visualize");
        method.Invoke(null, new object[] { part, test });
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
    }

    Console.WriteLine();
}