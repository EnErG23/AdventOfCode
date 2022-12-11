using AdventOfCode.Models;

namespace AdventOfCode.Y2022.Days
{
    public class Day10 : Day
    {
        public Day10(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
        {
            int x = 1;
            int cycle = 0;
            int signalStrengthSum = 0;

            foreach (var input in Inputs)
            {
                var commands = input.Split(" ");

                switch (commands[0])
                {
                    case "noop":
                        cycle++;

                        if (cycle == 20 || (cycle - 20) % 40 == 0)
                            signalStrengthSum += cycle * x;
                        break;
                    case "addx":
                        for (int i = 0; i < 2; i++)
                        {
                            cycle++;

                            if (cycle == 20 || (cycle - 20) % 40 == 0)
                                signalStrengthSum += cycle * x;

                            if (i == 1)
                                x += int.Parse(commands[1]);
                        }
                        break;
                }
            }

            return signalStrengthSum.ToString();
        }

        public override string RunPart2()
        {
            int x = 1;
            int cycle = 0;
            List<char[]> crtRows = new List<char[]>();
            string crtRow = "........................................";

            for (int i = 0; i < 6; i++)            
                crtRows.Add(crtRow.ToCharArray());            

            foreach (var input in Inputs)
            {
                var commands = input.Split(" ");

                switch (commands[0])
                {
                    case "noop":
                        cycle++;

                        if (x - 1 <= cycle || cycle <= x + 1)
                            crtRows[(cycle - 1) / 40][(cycle - 1) % 40] = '#';

                        break;
                    case "addx":
                        for (int i = 0; i < 2; i++)
                        {
                            cycle++;

                            if (x - 1 <= cycle || cycle <= x + 1)
                                crtRows[(cycle - 1) / 40][(cycle - 1) % 40] = '#';

                            if (i == 1)
                                x += int.Parse(commands[1]);
                        }
                        break;
                }
            }

            foreach (var r in crtRows)
            {
                foreach (char c in r)
                {
                    Console.Write(c);
                }
                Console.WriteLine();
            }

            return "undefined";
        }
    }
}