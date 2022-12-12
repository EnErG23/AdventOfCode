using AdventOfCode.Models;

namespace AdventOfCode.Y2022.Days
{
    public class Day11 : Day
    {
        private List<Monkey> _monkeys;

        public Day11(int year, int day, bool test) : base(year, day, test)
        {
            _monkeys = new List<Monkey>();

            for (int i = 0; i < Inputs.Count; i += 7)
            {
                //Console.WriteLine(long.Parse(Inputs[i].Replace("Monkey ", "").Replace(":", "")));
                //Console.WriteLine(Inputs[i + 1].Replace("  Starting items: ", "").Split(", ").Select(x => long.Parse(x)).ToList());
                //Console.WriteLine(Inputs[i + 2].Replace("  Operation: new = old ", "").Split(" ")[0]);
                //Console.WriteLine(Inputs[i + 2].Replace("  Operation: new = old ", "").Split(" ")[1]);
                //Console.WriteLine(long.Parse(Inputs[i + 3].Replace("  Test: divisible by ", "")));
                //Console.WriteLine(long.Parse(Inputs[i + 4].Replace("    If true: throw to monkey ", "")));
                //Console.WriteLine(long.Parse(Inputs[i + 5].Replace("    If false: throw to monkey ", "")));

                _monkeys.Add(new Monkey(int.Parse(Inputs[i].Replace("Monkey ", "").Replace(":", "")),
                        Inputs[i + 1].Replace("  Starting items: ", "").Split(", ").Select(x => long.Parse(x)).ToList(),
                        Inputs[i + 2].Replace("  Operation: new = old ", "").Split(" ")[0],
                        Inputs[i + 2].Replace("  Operation: new = old ", "").Split(" ")[1],
                        int.Parse(Inputs[i + 3].Replace("  Test: divisible by ", "")),
                        int.Parse(Inputs[i + 4].Replace("    If true: throw to monkey ", "")),
                        int.Parse(Inputs[i + 5].Replace("    If false: throw to monkey ", ""))));
            }
        }

        public override string RunPart1()
        {
            PlayGame(20, true);

            return (_monkeys.Max(m => m.Inspects) * _monkeys.OrderByDescending(m => m.Inspects).Skip(1).First().Inspects).ToString();
        }

        public override string RunPart2()
        {
            PlayGame(10000, false);

            return (_monkeys.Max(m => m.Inspects) * _monkeys.OrderByDescending(m => m.Inspects).Skip(1).First().Inspects).ToString();
        }

        public void PlayGame(int rounds, bool relief)
        {
            for (int i = 0; i < rounds; i++)
            {
                foreach (var monkey in _monkeys)
                {
                    //Console.WriteLine($"Monkey {monkey.Id}");

                    for (int j = 0; j < monkey.Items.Count; j++)
                    {
                        //Console.Write($"Item {monkey.Items[j]} ");

                        // INSPECT
                        long factor = monkey.Factor == "old" ? monkey.Items[j] : long.Parse(monkey.Factor);

                        if (monkey.Operation == "+")
                            monkey.Items[j] = (monkey.Items[j] + factor) / (relief ? 3 : 1);
                        else
                            monkey.Items[j] = (monkey.Items[j] * factor) / (relief ? 3 : 1);

                        //Console.Write($"=> {monkey.Items[j]} ");
                        monkey.Inspects++;

                        // TEST
                        if (monkey.Items[j] % monkey.Test == 0)
                        {
                            //Console.WriteLine($"=> {monkey.TrueMonkey}");
                            _monkeys.FirstOrDefault(m => m.Id == monkey.TrueMonkey).Items.Add(monkey.Items[j]);
                        }
                        else
                        {
                            //Console.WriteLine($"=> {monkey.FalseMonkey}");
                            _monkeys.FirstOrDefault(m => m.Id == monkey.FalseMonkey).Items.Add(monkey.Items[j]);
                        }
                        //Console.WriteLine("-----------------------------");
                    }

                    monkey.Items = new List<long>();
                }

                if (i < 20 || (i + 1) % 1000 == 0)
                {
                    Console.WriteLine($"After round {i + 1}, the monkeys are holding items with these worry levels:");
                    PrintMonkeys();
                    Console.WriteLine();
                }
            }
        }

        public void PrintMonkeys()
        {
            foreach (var monkey in _monkeys)
            {
                Console.Write($"Monkey {monkey.Id} ({monkey.Inspects}): ");
                Console.WriteLine(String.Join(", ", monkey.Items));
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