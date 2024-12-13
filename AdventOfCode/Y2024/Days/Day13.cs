using AdventOfCode.Models;

namespace AdventOfCode.Y2024.Days
{
    public class Day13 : Day
    {
        private List<ClawMachine> _clawMachines;

        public Day13(int year, int day, bool test) : base(year, day, test)
        {
            _clawMachines = new();

            for (int i = 0; i < Inputs.Count(); i += 4)
            {
                var line = Inputs[i].Trim();
                line = line.Replace("Button A: X+", "").Replace(" Y+", "");
                var moves = line.Split(",").Select(m => decimal.Parse(m.ToString())).ToList();
                var buttonA = (moves[0], moves[1]);

                line = Inputs[i + 1].Trim();
                moves = line.Replace("Button B: X+", "").Replace(" Y+", "").Split(",").Select(m => decimal.Parse(m.ToString())).ToList();
                var buttonB = (moves[0], moves[1]);

                line = Inputs[i + 2].Trim();
                moves = line.Replace("Prize: X=", "").Replace(" Y=", "").Split(",").Select(m => decimal.Parse(m.ToString())).ToList();
                var prize = (moves[0], moves[1]);

                _clawMachines.Add(new ClawMachine(buttonA, buttonB, prize));
            }
        }

        public override string RunPart1() => _clawMachines.Sum(c => c.GetCheapestSolve(0)).ToString();

        public override string RunPart2() => _clawMachines.Sum(c => c.GetCheapestSolve(10000000000000)).ToString();
    }

    class ClawMachine
    {
        public (decimal, decimal) ButtonA;
        public (decimal, decimal) ButtonB;
        public (decimal, decimal) Prize;

        public ClawMachine((decimal, decimal) buttonA, (decimal, decimal) buttonB, (decimal, decimal) prize)
        {
            ButtonA = buttonA;
            ButtonB = buttonB;
            Prize = prize;
        }

        public decimal GetCheapestSolve(decimal offset)
        {
            decimal A1 = ButtonA.Item1;
            decimal B1 = ButtonB.Item1;
            decimal C1 = Prize.Item1 + offset;

            decimal A2 = ButtonA.Item2;
            decimal B2 = ButtonB.Item2;
            decimal C2 = Prize.Item2 + offset;

            decimal delta = A1 * B2 - A2 * B1;

            if (delta == 0)
                throw new ArgumentException("Lines are parallel");

            decimal x = (B2 * C1 - B1 * C2) / delta;
            decimal y = (A1 * C2 - A2 * C1) / delta;

            return (x % 1 == 0 && y % 1 == 0) ? (3 * x) + y : 0;
        }
    }
}