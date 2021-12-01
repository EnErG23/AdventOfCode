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
        case "clear":
            Console.Clear();
            PrintHeader();
            break;
        default:
            Console.WriteLine("Invalid command");
            break;
    }
}

void AddSkipDays()
{
    // 2020 - Not yet copied from other repo
    //skipDays.Add("20204");
    //skipDays.Add("20205");
    //skipDays.Add("20206");
    //skipDays.Add("20207");
    //skipDays.Add("20208");
    //skipDays.Add("20209");
    //skipDays.Add("202010");
    //skipDays.Add("202012");
    //skipDays.Add("202013");
    //skipDays.Add("202015");
    //skipDays.Add("202016");
    //skipDays.Add("202018");
    //skipDays.Add("202019");
    //skipDays.Add("202020");
    //skipDays.Add("202021");
    //skipDays.Add("202022");
    //skipDays.Add("202023");
    //skipDays.Add("202024");
    //skipDays.Add("202025");

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
        if (skipDays.Contains($"{year}{i}"))
        {
            Console.WriteLine($"Skip day {i}");
            Console.WriteLine("---------------------------------------");
        }
        else
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