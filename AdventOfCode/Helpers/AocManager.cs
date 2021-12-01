using Microsoft.Extensions.Configuration;
using System.Diagnostics;

namespace AdventOfCode.Helpers
{
    internal class AocManager
    {
        public static int Year { get; set; } = GetYear();

        static readonly string website = $"https://adventofcode.com/{Year}";
        static readonly string? sessionID = new ConfigurationBuilder()
                                            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                                            .AddUserSecrets<Program>()
                                            .Build()
                                            .GetSection(nameof(AocConfig))
                                            .Get<AocConfig>()
                                            .SessionID;

        public static int GetYear()
        {
            DateTime date = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "Eastern Standard Time").Date;
            return date.Year - (date.Month < 12 ? 1 : 0);
        }
        public static void OpenAoc(int day)
        {
            var url = $"{website}/day/{day}";

            var prs = new ProcessStartInfo(@"C:\Program Files (x86)\BraveSoftware\Brave-Browser\Application\brave.exe")
            {
                Arguments = url
            };

            Process.Start(prs);

            if (!File.Exists($"Inputs\\{day}-test.txt"))
                GetTestInput(day);

            if (!File.Exists($"Inputs\\{day}.txt"))
                GetInput(day);
        }
        public static async void GetTestInput(int day)
        {
            var url = $"{website}/day/{day}";

            HttpClient client = new();
            client.DefaultRequestHeaders.Add("cookie", $"session={sessionID}");

            var response = await client.GetAsync(url);
            using StreamReader? streamReader = new(response.Content.ReadAsStreamAsync().Result);
            var result = streamReader.ReadToEnd() ?? "";

            result = result.IndexOf("For example") < 0 ? result : result[result.IndexOf("For example")..];
            result = result.IndexOf("<code>") < 0 ? result : result[(result.IndexOf("<code>") + 6)..];
            result = result.IndexOf("</code>") < 0 ? result : result[..result.IndexOf("</code>")];

            var path = $"Y{Year}\\Inputs\\{day}-test.txt";
            await File.WriteAllTextAsync(path, result);
        }
        public static async void GetInput(int day)
        {
            var url = $"{website}/day/{day}/input";

            HttpClient client = new();
            client.DefaultRequestHeaders.Add("cookie", $"session={sessionID}");

            var response = await client.GetAsync(url);
            using StreamReader? streamReader = new(response.Content.ReadAsStreamAsync().Result);
            var result = streamReader.ReadToEnd() ?? "";

            var path = $"Y{Year}\\Inputs\\{day}.txt";
            await File.WriteAllTextAsync(path, result);
        }
        public static async void SubmitAnswer(int day)
        {
            var zero = day < 10 ? "0" : "";
            Type? dayClass = Type.GetType($"AdventOfCode.Y{Year}.Days.Day{zero}{day}");
            object? answer = dayClass.GetProperty("Answer2").GetValue(dayClass, null) ?? dayClass.GetProperty("Answer1").GetValue(dayClass, null);
            var level = dayClass.GetProperty("Answer2").GetValue(dayClass, null) == null ? 1 : 2;

            HttpClient client = new();
            client.DefaultRequestHeaders.Add("cookie", $"session={sessionID}");

            var values = new Dictionary<string, string>
            {
                { "level", level.ToString() },
                { "answer", answer.ToString() ?? "" }
            };

            var content = new FormUrlEncodedContent(values);

            var url = $"{website}/day/{day}/answer";
            var response = await client.PostAsync(url, content);

            using StreamReader? streamReader = new(response.Content.ReadAsStreamAsync().Result);
            var result = streamReader.ReadToEnd();
            result = result[(result.IndexOf("<article><p>") + 12)..];
            result = result[..result.IndexOf("</p>")];
            Console.WriteLine(result);
        }
    }

    public class AocConfig
    {
        public string? SessionID { get; set; }
    }
}