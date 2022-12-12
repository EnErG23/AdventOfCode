using AdventOfCode.Models;

namespace AdventOfCode.Y2022.Days
{
    public class Day11 : Day
    {
        private List<Monkey> _monkeys;

        public Day11(int year, int day, bool test) : base(year, day, test) => _monkeys = new List<Monkey>();

        public void SetMonkeys()
        {
            _monkeys = new List<Monkey>();

            for (int i = 0; i < Inputs.Count; i += 7)
                _monkeys.Add(new Monkey(int.Parse(Inputs[i].Replace("Monkey ", "").Replace(":", "")),
                        Inputs[i + 1].Replace("  Starting items: ", "").Split(", ").Select(x => long.Parse(x)).ToList(),
                        Inputs[i + 2].Replace("  Operation: new = old ", "").Split(" ")[0],
                        Inputs[i + 2].Replace("  Operation: new = old ", "").Split(" ")[1],
                        int.Parse(Inputs[i + 3].Replace("  Test: divisible by ", "")),
                        int.Parse(Inputs[i + 4].Replace("    If true: throw to monkey ", "")),
                        int.Parse(Inputs[i + 5].Replace("    If false: throw to monkey ", ""))));
        }

        public override string RunPart1()
        {
            SetMonkeys();

            PlayGame(20, true);

            return (_monkeys.Max(m => m.Inspects) * _monkeys.OrderByDescending(m => m.Inspects).Skip(1).First().Inspects).ToString();
        }

        public override string RunPart2()
        {
            SetMonkeys();

            PlayGame(10000, false);

            return (_monkeys.Max(m => m.Inspects) * _monkeys.OrderByDescending(m => m.Inspects).Skip(1).First().Inspects).ToString();
        }

        public void PlayGame(int rounds, bool relief)
        {
            var LCM = 1;

            foreach (var monkey in _monkeys)
                LCM *= monkey.Test;

            for (int i = 0; i < rounds; i++)
                foreach (var monkey in _monkeys)
                {
                    for (int j = 0; j < monkey.Items.Count; j++)
                    {
                        // INSPECT
                        long factor = monkey.Factor == "old" ? monkey.Items[j] : long.Parse(monkey.Factor);
                        long newItem = monkey.Items[j];

                        if (monkey.Operation == "+")
                            newItem = monkey.Items[j] + factor;
                        else
                            newItem = monkey.Items[j] * factor;

                        monkey.Inspects++;

                        // RELIEF / LOWER NUMBERS
                        monkey.Items[j] = relief ? newItem / 3 : newItem % LCM;

                        // TEST
                        if (monkey.Items[j] % monkey.Test == 0)
                            _monkeys.FirstOrDefault(m => m.Id == monkey.TrueMonkey).Items.Add(monkey.Items[j]);
                        else
                            _monkeys.FirstOrDefault(m => m.Id == monkey.FalseMonkey).Items.Add(monkey.Items[j]);
                    }

                    monkey.Items = new List<long>();
                }
        }

        public class Monkey
        {
            public int Id { get; set; }
            public List<long> Items { get; set; }
            public string Operation { get; set; }
            public string Factor { get; set; }
            public int Test { get; set; }
            public int TrueMonkey { get; set; }
            public int FalseMonkey { get; set; }
            public long Inspects { get; set; }

            public Monkey(int id, List<long> items, string operation, string factor, int test, int trueMonkey, int falseMonkey)
            {
                Id = id;
                Items = items;
                Operation = operation;
                Factor = factor;
                Test = test;
                TrueMonkey = trueMonkey;
                FalseMonkey = falseMonkey;
                Inspects = 0;
            }
        }
    }
}