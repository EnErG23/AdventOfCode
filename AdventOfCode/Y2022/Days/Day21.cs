using AdventOfCode.Models;

namespace AdventOfCode.Y2022.Days
{
    public class Day21 : Day
    {
        private List<Monkey> _monkeys;

        public Day21(int year, int day, bool test) : base(year, day, test)
        {
            _monkeys = new();

            foreach (string input in Inputs)
            {
                var words = input.Replace(":", "").Split(" ");

                if (words.Length > 2)
                {
                    _monkeys.Add(new Monkey(words[0], words[1], words[2], words[3]));
                }
                else
                {
                    _monkeys.Add(new Monkey(words[0], long.Parse(words[1])));
                }
            }
        }


        public override string RunPart1()
            => _monkeys.FirstOrDefault(m => m.Name == "root").GetNumber(_monkeys).ToString();

        public override string RunPart2()
        {
            var monkeyAffectedByHumn = _monkeys.FirstOrDefault(m => m.Name == "humn");
            List<Monkey> monkeysAffectedByHumn = new() { monkeyAffectedByHumn };

            var otherMonkey = "otherMonkey";

            // Find the monkeys that rely on the value of monkey "humn"
            // Get the other monkey of which we need to compare the value
            foreach (var monkey in _monkeys)
            {
                var newMonkeyAffectedByHumn = _monkeys.FirstOrDefault(m => m.Monkey1 == monkeyAffectedByHumn.Name || m.Monkey2 == monkeyAffectedByHumn.Name);

                if (newMonkeyAffectedByHumn.Name == "root")
                {
                    otherMonkey = _monkeys.FirstOrDefault(m => m.Name == "root").Monkey1 == monkeyAffectedByHumn.Name ? _monkeys.FirstOrDefault(m => m.Name == "root").Monkey2 : _monkeys.FirstOrDefault(m => m.Name == "root").Monkey1;
                    break;
                }

                monkeyAffectedByHumn = newMonkeyAffectedByHumn;
                monkeysAffectedByHumn.Add(monkeyAffectedByHumn);
            }

            // Get the value we need to match
            long value = _monkeys.FirstOrDefault(m => m.Name == otherMonkey).GetNumber(_monkeys);

            // Reverse calculations from value till we get the number of monkey "humn"
            for (int i = monkeysAffectedByHumn.Count() - 1; i >= 0; i--)
            {
                var monkey = monkeysAffectedByHumn[i];

                if (i > 0)
                {
                    bool isMonkey1Unknown = monkeysAffectedByHumn[i - 1].Name == monkey.Monkey1;
                    var calcValue = isMonkey1Unknown ? _monkeys.FirstOrDefault(m => m.Name == monkey.Monkey2).GetNumber(_monkeys) : _monkeys.FirstOrDefault(m => m.Name == monkey.Monkey1).GetNumber(_monkeys);

                    switch (monkey.Operation)
                    {
                        case "+":
                            value -= calcValue;
                            break;
                        case "-":
                            value = isMonkey1Unknown ? value + calcValue : calcValue - value;
                            break;
                        case "*":
                            value /= calcValue;
                            break;
                        case "/":
                            value = isMonkey1Unknown ? value * calcValue : calcValue / value;
                            break;
                    }
                }
            }

            return value.ToString();
        }
    }

    public class Monkey
    {
        public string Name { get; set; }
        public long Number { get; set; }
        public string Monkey1 { get; set; }
        public string Operation { get; set; }
        public string Monkey2 { get; set; }

        public Monkey(string name, long number)
        {
            Name = name;
            Number = number;
        }

        public Monkey(string name, string monkey1, string operation, string monkey2)
        {
            Name = name;
            Number = 0;
            Monkey1 = monkey1;
            Operation = operation;
            Monkey2 = monkey2;
        }

        public long GetNumber(List<Monkey> monkeys)
        {
            if (Number == 0)
                switch (Operation)
                {
                    case "+":
                        Number = monkeys.FirstOrDefault(m => m.Name == Monkey1).GetNumber(monkeys) + monkeys.FirstOrDefault(m => m.Name == Monkey2).GetNumber(monkeys);
                        break;
                    case "-":
                        Number = monkeys.FirstOrDefault(m => m.Name == Monkey1).GetNumber(monkeys) - monkeys.FirstOrDefault(m => m.Name == Monkey2).GetNumber(monkeys);
                        break;
                    case "*":
                        Number = monkeys.FirstOrDefault(m => m.Name == Monkey1).GetNumber(monkeys) * monkeys.FirstOrDefault(m => m.Name == Monkey2).GetNumber(monkeys);
                        break;
                    case "/":
                        Number = monkeys.FirstOrDefault(m => m.Name == Monkey1).GetNumber(monkeys) / monkeys.FirstOrDefault(m => m.Name == Monkey2).GetNumber(monkeys);
                        break;
                }

            return Number;
        }
    }
}