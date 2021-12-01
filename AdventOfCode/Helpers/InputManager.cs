namespace AdventOfCode.Helpers
{
    internal class InputManager
    {
        public static void PrintInput(int day, bool test)
        {
            foreach (var line in InputManager.GetInputAsStrings(day, test))
                Console.WriteLine(line);
        }
        public static List<string> GetInputAsStrings(int day, bool test)
        {
            var inputs = new List<string>();

            StreamReader file = test ? new StreamReader($@"Inputs\{day}-test.txt") : new StreamReader($@"Inputs\{day}.txt");
            string? line;

            while ((line = file.ReadLine()) != null)
            {
                inputs.Add(line);
            }

            return inputs;
        }
        public static List<int> GetInputAsInts(int day, bool test) => GetInputAsStrings(day, test).Select(i => int.Parse(i)).ToList();        
        public static List<long> GetInputAsLongs(int day, bool test) => GetInputAsStrings(day, test).Select(i => long.Parse(i)).ToList();        

    }
}