using AdventOfCode.Models;
using System.Diagnostics;
using AdventOfCode.Y2020.Models;

namespace AdventOfCode.Y2020.Days
{
    public class Day07 : Day
    {
        public Day07(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
        {
            long result = 0;

            List<string> newInputs = new List<string>();

            foreach (var input in Inputs)
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

			return result.ToString();
        }

        public override string RunPart2()
        {
            long result = -1;

            List<string> newInputs = new List<string>();

            foreach (var input in Inputs)
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

			return result.ToString();
        }
    }
}