using AdventOfCode.Models;

namespace AdventOfCode.Y2022.Days
{
    public class Day10 : Day
    {
        private List<char[]> _crtRows;

        public Day10(int year, int day, bool test) : base(year, day, test)
        {
            _crtRows = new List<char[]>();
        }

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
            string crtRow = "........................................";

            for (int i = 0; i < 6; i++)
                _crtRows.Add(crtRow.ToCharArray());

            foreach (var input in Inputs)
            {
                var commands = input.Split(" ");

                switch (commands[0])
                {
                    case "noop":
                        cycle++;

                        if (x - 1 <= ((cycle - 1) % 40) && ((cycle - 1) % 40) <= x + 1)
                            _crtRows[(cycle - 1) / 40][(cycle - 1) % 40] = '#';

                        break;
                    case "addx":
                        for (int i = 0; i < 2; i++)
                        {
                            cycle++;

                            if (x - 1 <= ((cycle - 1) % 40) && ((cycle - 1) % 40) <= x + 1)
                                _crtRows[(cycle - 1) / 40][(cycle - 1) % 40] = '#';

                            if (i == 1)
                                x += int.Parse(commands[1]);
                        }
                        break;
                }
            }


            return "See visual";
        }

        public override void VisualizePart2()
        {
            RunPart2();

            foreach (var r in _crtRows)
            {
                foreach (char c in r)
                {
                    Console.Write(c);
                }
                Console.WriteLine();
            }
        }
    }
}