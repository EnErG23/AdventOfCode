using AdventOfCode.Models;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Y2020.Days
{
    public class Day23 : Day
    {
        static List<int>? inputs;

        public Day23(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
        {
            inputs = Inputs.Select(i => int.Parse(i)).ToList();

            long result = 0;

            for (int i = 0; i < 100; i++)
            {
                //Console.WriteLine($"-- move {i + 1} --");

                var currentIndex = 0;

                var currentCup = inputs[currentIndex++];

                //Console.WriteLine($"cups: {string.Join(" ", Inputs).Replace(currentCup.ToString(), $"({currentCup})")}");

                var pickedUpCups = inputs.GetRange(currentIndex, 3); // TODO: fix randow new error

                //Console.WriteLine($"pick up: {string.Join(" ", pickedUpCups)}");

                inputs.RemoveRange(currentIndex, 3);
                var restingCups = inputs;

                var destinationCup = currentCup - 1 < restingCups.Min() ? restingCups.Max() : currentCup - 1;
                var destinationIndex = 0;

                while (destinationIndex == 0)
                {
                    if (inputs.Contains(destinationCup--))
                    {
                        //Console.WriteLine($"destination: {destinationCup + 1}");
                        destinationIndex = restingCups.IndexOf(destinationCup + 1) + 1;
                        break;
                    }
                }

                var tempInputs = new List<int>();

                tempInputs.AddRange(restingCups.GetRange(0, destinationIndex));
                restingCups.RemoveRange(0, destinationIndex);
                tempInputs.AddRange(pickedUpCups);
                tempInputs.AddRange(restingCups);

                currentIndex = tempInputs.IndexOf(currentCup) + 1 > tempInputs.Count() ? 0 : tempInputs.IndexOf(currentCup) + 1;

                inputs = new List<int>();
                inputs.Add(tempInputs[currentIndex]);
                inputs.AddRange(tempInputs.GetRange(currentIndex + 1, tempInputs.Count() - currentIndex - 1));
                inputs.AddRange(tempInputs.GetRange(0, currentIndex));

                //Console.WriteLine();
            }

            //Console.WriteLine($"final cups: {string.Join(" ", Inputs)}");

            var oneIndex = inputs.IndexOf(1);
            var resultCups = inputs.GetRange(oneIndex + 1, Inputs.Count() - oneIndex - 1);
            resultCups.AddRange(inputs.GetRange(0, oneIndex));

            result = Convert.ToInt64(string.Join("", resultCups));

			return result.ToString();
        }

        public override string RunPart2()
        {
            inputs = Inputs.Select(i => int.Parse(i)).ToList();

            long result = 0;            

            for (int i = 10; i <= 1000000; i++)
            {
                inputs.Add(i);
            }

            LinkedList<int> cups = new LinkedList<int>(inputs);

            var currentCup = cups.First;

            // Create index of LinkedList
            var index = new Dictionary<int, LinkedListNode<int>>();
            while (index.Count != cups.Count)
            {
                index.Add(currentCup.Value, currentCup);
                currentCup = currentCup.Next == null ? cups.First : currentCup.Next;
            }

            for (var i = 0; i < 10000000; i++)
            {
                var removedCups = new List<LinkedListNode<int>>(3);
                for (var j = 0; j < 3; j++)
                {
                    removedCups.Insert(0, currentCup.Next == null ? cups.First : currentCup.Next);
                    cups.Remove(currentCup.Next == null ? cups.First : currentCup.Next);
                }

                var destinationCup = GetNextDestinationCup(removedCups, currentCup.Value);
                var destinationCupNode = index[destinationCup];

                removedCups.ForEach(cup => cups.AddAfter(destinationCupNode ?? cups.First, cup));

                currentCup = currentCup.Next == null ? cups.First : currentCup.Next;
            }

            result = Convert.ToInt64(cups.Find(1).Next.Value) * Convert.ToInt64(cups.Find(1).Next.Next.Value);

			return result.ToString();
        }

        public static int GetNextDestinationCup(List<LinkedListNode<int>> removedCups, int currentCup)
        {
            var removedCupsValues = removedCups.Select(cup => cup.Value);

            var destinationCupValue = currentCup;
            while (true)
            {
                if (--destinationCupValue < 1)
                {
                    destinationCupValue = 1000000;
                }

                if (!removedCupsValues.Contains(destinationCupValue))
                {
                    return destinationCupValue;
                }
            }
        }
    }
}