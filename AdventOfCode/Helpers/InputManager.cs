namespace AdventOfCode.Helpers
{
    internal class InputManager
    {
        public static void PrintInput(int day, bool test)
        {
            foreach (var line in InputManager.GetInputAsStrings(AocManager.Year, day, test))
                Console.WriteLine(line);
        }

        public static List<string> GetInputAsStrings(int year, int day, bool test)
        {
            var inputs = new List<string>();            

            StreamReader file;
            string? line;

            try
            {
                file = test ? new StreamReader($@"Y{year}\Inputs\{day}-test.txt") : new StreamReader($@"Y{year}\Inputs\{day}.txt");
            }
            catch {
                CommandManager.HandleInput($"i {day}");
                Thread.Sleep(2000);
                file = test ? new StreamReader($@"Y{year}\Inputs\{day}-test.txt") : new StreamReader($@"Y{year}\Inputs\{day}.txt");
            }

            while ((line = file.ReadLine()) != null)
            {
                inputs.Add(line);
            }

            return inputs;
        }
    }
}