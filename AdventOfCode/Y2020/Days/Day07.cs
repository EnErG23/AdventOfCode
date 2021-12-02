using AdventOfCode.Helpers;
using AdventOfCode.Y2020.Models;

namespace AdventOfCode.Y2020.Days
{
    public static class Day07
    {
        static int day = 7;
        static List<string>? inputs;

        public static string? Answer1 { get; set; }
        public static string? Answer2 { get; set; }

        public static void Run(int part, bool test)
        {
            inputs = InputManager.GetInputAsStrings(day, test);

            var start = DateTime.Now;

            string part1 = "";
            string part2 = "";

            if (part == 1)
                part1 = Part1();
            else if (part == 2)
                part2 = Part2();
            else
            {
                part1 = Part1();
                part2 = Part2();
            }

            var ms = Math.Round((DateTime.Now - start).TotalMilliseconds);

            Console.WriteLine($"Day {day} ({ms}ms):");
            if (part1 != "") Console.WriteLine($"    {part1}");
            if (part2 != "") Console.WriteLine($"    {part2}");
        }

        static string Part1()
        {
            long result = 0;

            var start = DateTime.Now;

            #region Solution

            List<string> newInputs = new List<string>();

            foreach (var input in inputs)
            {
                var tempInput = input.Replace("bags ", "").Replace("bags, ", "").Replace("bags.", "").Replace("contain ", "").Replace("bag", "").Replace(", ", "").Replace(".", "").Replace(" no other", "");

                for (int i = 0; i < 10; i++)
                {
                    tempInput = tempInput.Replace($"{i}", "");
                }

                tempInput = tempInput.Replace($"  ", ";");
                newInputs.Add(tempInput.Replace(" ", ""));
            }

            List<Bag> bags = new List<Bag>();

            foreach (var newInput in newInputs)
            {
                var newBags = newInput.Split(';').ToList();

                if (bags.Where(b => b.Name == newBags[0]).Count() > 0)
                {
                    var bag = bags.Where(b => b.Name == newBags[0]).First();

                    foreach (var childBag in newBags.GetRange(1, newBags.Count() - 1))
                    {
                        if (!bag.ChildBags.Contains(childBag))
                        {
                            bag.ChildBags.Add(childBag);
                        }
                    }
                }
                else
                {
                    var childBags = newBags.GetRange(1, newBags.Count() - 1);

                    Bag bag = new Bag
                    {
                        Name = newBags[0],
                        ChildBags = childBags
                    };

                    bags.Add(bag);
                }
            }

            bags = bags.OrderBy(b => b.Name).ToList();

            var hasParent = true;
            var bagNames = new List<string>() { "shinygold" };
            var possibleBags = new List<Bag>();

            while (hasParent)
            {
                if (bags.Where(b => b.ChildBags.Any(item => bagNames.Contains(item))).Count() > 0)
                {
                    possibleBags.AddRange(bags.Where(b => b.ChildBags.Any(item => bagNames.Contains(item))));

                    bagNames = bags.Where(b => b.ChildBags.Any(item => bagNames.Contains(item))).Select(b => b.Name).ToList();
                }
                else
                {
                    hasParent = false;
                }
            }

            result = possibleBags.Distinct().Count();

            #endregion

            var ms = Math.Round((DateTime.Now - start).TotalMilliseconds);

            if (result > 0) Answer1 = result.ToString();
            return $"Part 1 ({ms}ms): {result} ";
        }

        static string Part2()
        {
            long result = 0;

            var start = DateTime.Now;

            #region Solution

            result = -1;

            List<string> newInputs = new List<string>();

            foreach (var input in inputs)
            {
                var tempInput = input.Replace("bags ", "").Replace("bags, ", "").Replace("bags.", "").Replace("contain ", "").Replace("bag", "").Replace(", ", "").Replace(".", "").Replace(" no other", "");

                for (int i = 0; i < 10; i++)
                {
                    tempInput = tempInput.Replace($"{i}", $";{i}");
                }

                tempInput = tempInput.Replace($"  ", ";");
                newInputs.Add(tempInput.Replace(" ", ""));
            }

            List<Bag> bags = new List<Bag>();

            foreach (var newInput in newInputs)
            {
                var newBags = newInput.Split(';').ToList();

                if (bags.Where(b => b.Name == newBags[0]).Count() > 0)
                {
                    var bag = bags.Where(b => b.Name == newBags[0]).First();

                    foreach (var childBag in newBags.GetRange(1, newBags.Count() - 1))
                    {
                        if (!bag.ChildBags.Contains(childBag))
                        {
                            bag.ChildBags.Add(childBag);
                        }
                    }
                }
                else
                {
                    var childBags = newBags.GetRange(1, newBags.Count() - 1);

                    Bag bag = new Bag
                    {
                        Name = newBags[0],
                        ChildBags = childBags
                    };

                    bags.Add(bag);
                }
            }

            bags = bags.OrderBy(b => b.Name).ToList();

            var checkBags = bags.Where(b => b.Name == "shinygold");

            var hasChildren = true;

            while (hasChildren)
            {
                var tempCheckBags = new List<Bag>();

                foreach (var bag in checkBags)
                {
                    result++;

                    foreach (var child in bag.ChildBags)
                    {
                        for (int i = 0; i < Convert.ToInt32(child.Substring(0, 1)); i++)
                        {
                            tempCheckBags.Add(bags.Where(b => b.Name == child.Substring(1)).First());
                        }
                    }
                }

                if (tempCheckBags.Count() == 0)
                {
                    hasChildren = false;
                }

                checkBags = tempCheckBags;
            }

            #endregion

            var ms = Math.Round((DateTime.Now - start).TotalMilliseconds);

            if (result > 0) Answer2 = result.ToString();
            return $"Part 2 ({ms}ms): {result} ";
        }
    }
}