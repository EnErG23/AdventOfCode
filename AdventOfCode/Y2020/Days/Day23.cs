using AdventOfCode.Helpers;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Y2020.Days
{
    public static class Day23
    {
        static readonly int day = 23;
        static List<int>? inputs;

        public static string? Answer1 { get; set; }
        public static string? Answer2 { get; set; }

        public static void Run(int part, bool test)
        {
            Stopwatch sw = Stopwatch.StartNew();

            inputs = InputManager.GetInputAsInts(day, test);

            string part1 = "";
            string part2 = "";

            switch (part)
            {
                case 1:
                    part1 = Part1();
                    break;
                case 2:
                    part2 = Part2(test);
                    break;
                default:
                    part1 = Part1();
                    part2 = Part2(test);
                    break;
            }

            sw.Stop();
			var ms = sw.Elapsed.TotalMilliseconds;

            Console.WriteLine($"Day {day} ({ms}ms):");
            if (part1 != "") Console.WriteLine($"    {part1}");
            if (part2 != "") Console.WriteLine($"    {part2}");
        }

        private static string Part1()
        {
            Stopwatch sw = Stopwatch.StartNew();

            long result = 0;

            #region Solution

            for (int i = 0; i < 100; i++)
            {
                //Console.WriteLine($"-- move {i + 1} --");

                var currentIndex = 0;

                var currentCup = inputs[currentIndex++];

                //Console.WriteLine($"cups: {string.Join(" ", inputs).Replace(currentCup.ToString(), $"({currentCup})")}");

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

            //Console.WriteLine($"final cups: {string.Join(" ", inputs)}");

            var oneIndex = inputs.IndexOf(1);
            var resultCups = inputs.GetRange(oneIndex + 1, inputs.Count() - oneIndex - 1);
            resultCups.AddRange(inputs.GetRange(0, oneIndex));

            result = Convert.ToInt64(string.Join("", resultCups));

            #endregion

            sw.Stop();
			var ms = sw.Elapsed.TotalMilliseconds;

            if (result > 0) Answer1 = result.ToString();
            return $"Part 1 ({ms}ms): {result} ";
        }

        static string Part2(bool test)
        {
            Stopwatch sw = Stopwatch.StartNew();

            long result = 0;

            #region Solution

            inputs = InputManager.GetInputAsInts(day, test);

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

            #endregion

            sw.Stop();
			var ms = sw.Elapsed.TotalMilliseconds;

            if (result > 0) Answer2 = result.ToString();
            return $"Part 2 ({ms}ms): {result} ";
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