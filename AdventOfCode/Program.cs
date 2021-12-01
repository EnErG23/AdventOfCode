using AdventOfCode.Helpers;

var year = AocManager.Year;
var day = 0;
var part = 0;

PrintHeader();

while (true)
{
    Console.WriteLine();
    Console.WriteLine("What would you like to do?");

    string? input = Console.ReadLine();
    bool test = input.Contains("test");
    var inputs = input.Replace("test ", "").Replace(" test", "").Replace(" day", "").Replace(" part", "").Replace(" year", "").Split(' ');
    Console.WriteLine();

    var command = inputs[0];
    day = inputs.Length > 1 ? Convert.ToInt32(inputs[1]) : 0;
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
            if (day == 0)
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
    {
        RunDay(i, 0, test);
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