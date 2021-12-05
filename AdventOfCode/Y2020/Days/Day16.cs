using AdventOfCode.Helpers;
using AdventOfCode.Y2020.Models;

namespace AdventOfCode.Y2020.Days
{
    public static class Day16
    {
        static int day = 16;
        static List<string>? inputs;

        public static string? Answer1 { get; set; }
        public static string? Answer2 { get; set; }

        public static void Run(int part, bool test)
        {
            var start = DateTime.Now;

            inputs = InputManager.GetInputAsStrings(day, test);

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

            List<Restriction> restrictions = new List<Restriction>();

            for (int i = 0; i < 20; i++)
            {
                var tempString = inputs[i].Substring(inputs[i].IndexOf(": ") + 2);
                var min1 = Convert.ToInt32(tempString.Substring(0, tempString.IndexOf("-")));

                tempString = tempString.Substring(tempString.IndexOf("-") + 1);
                var max1 = Convert.ToInt32(tempString.Substring(0, tempString.IndexOf(" or ")));

                tempString = tempString.Substring(tempString.IndexOf("or ") + 3);
                var min2 = Convert.ToInt32(tempString.Substring(0, tempString.IndexOf("-")));

                tempString = tempString.Substring(tempString.IndexOf("-") + 1);
                var max2 = Convert.ToInt32(tempString);

                Restriction restriction = new Restriction
                {
                    Min1 = min1,
                    Max1 = max1,
                    Min2 = min2,
                    Max2 = max2
                };
                restrictions.Add(restriction);
            }

            for (int i = 25; i < inputs.Count(); i++)
            {
                var numbers = inputs[i].Split(',').Select(n => Convert.ToInt32(n));

                foreach (var number in numbers)
                {
                    var passesOne = false;

                    foreach (var rest in restrictions)
                    {
                        if (((number >= rest.Min1) & (number <= rest.Max1)) | ((number >= rest.Min2) & (number <= rest.Max2)))
                        {
                            passesOne = true;
                            break;
                        }
                    }

                    if (!passesOne)
                    {
                        result += number;
                    }
                }
            }

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

            result = 1;

            List<Restriction> restrictions = new List<Restriction>();
            List<int> possiblePositions = new List<int>();

            // GET GENERAL POSSIBLE POSITIONS
            for (int i = 0; i < inputs[25].Split(',').Count(); i++)
            {
                possiblePositions.Add(i);
            }

            // GET RESTRICTIONS
            for (int i = 0; i < 20; i++)
            {
                var tempString = inputs[i];
                var name = tempString.Substring(0, tempString.IndexOf(": "));

                tempString = tempString.Substring(tempString.IndexOf(": ") + 2);
                var min1 = Convert.ToInt32(tempString.Substring(0, tempString.IndexOf("-")));

                tempString = tempString.Substring(tempString.IndexOf("-") + 1);
                var max1 = Convert.ToInt32(tempString.Substring(0, tempString.IndexOf(" or ")));

                tempString = tempString.Substring(tempString.IndexOf("or ") + 3);
                var min2 = Convert.ToInt32(tempString.Substring(0, tempString.IndexOf("-")));

                tempString = tempString.Substring(tempString.IndexOf("-") + 1);
                var max2 = Convert.ToInt32(tempString);

                Restriction restriction = new Restriction
                {
                    Name = name,
                    PossiblePositions = possiblePositions,
                    Min1 = min1,
                    Max1 = max1,
                    Min2 = min2,
                    Max2 = max2
                };
                restrictions.Add(restriction);
            }

            List<int> validTickets = new List<int>();

            // GET VALID TICKETS
            for (int i = 25; i < inputs.Count(); i++)
            {
                var numbers = inputs[i].Split(',').Select(n => Convert.ToInt32(n));

                var isValidTicket = true;

                foreach (var number in numbers)
                {
                    var passesOne = false;

                    foreach (var rest in restrictions)
                    {
                        if (((number >= rest.Min1) & (number <= rest.Max1)) | ((number >= rest.Min2) & (number <= rest.Max2)))
                        {
                            passesOne = true;
                            break;
                        }
                    }

                    if (!passesOne)
                    {
                        isValidTicket = false;
                        break;
                    }
                }

                if (isValidTicket)
                    validTickets.Add(i);
            }

            // ELIMINATE RESTRICTION POSSIBILITIES BY CHECKING NEARBY TICKET VALUES
            foreach (var validTicket in validTickets)
            {
                var numbers = inputs[validTicket].Split(',').Select(n => Convert.ToInt32(n)).ToList();

                foreach (var rest in restrictions.Where(r => r.PossiblePositions.Count() > 1))
                {
                    List<int> newPossiblePositions = rest.PossiblePositions.ToList();

                    foreach (var possiblePosition in rest.PossiblePositions)
                    {
                        var number = numbers[possiblePosition];

                        if (!(((number >= rest.Min1) & (number <= rest.Max1)) | ((number >= rest.Min2) & (number <= rest.Max2))))
                            newPossiblePositions.Remove(possiblePosition);
                    }

                    rest.PossiblePositions = newPossiblePositions;
                }

                if (restrictions.Sum(r => r.PossiblePositions.Count()) == restrictions.Count())
                    break;
            }

            // ELIMINATE RESTRICTION POSSIBILITIES BY CHECKING ONLY POSSIBILITY IN OTHER RESTRICTIONS
            foreach (var restriction in restrictions.OrderBy(r => r.PossiblePositions.Count()))
            {
                foreach (var res in restrictions.Where(r => r.Name != restriction.Name))
                {
                    res.PossiblePositions.Remove(restriction.PossiblePositions.First());
                }
            }

            var myTicket = inputs[22].Split(',').Select(n => Convert.ToInt32(n)).ToList();

            // MULTIPLY DEPARTURE VALUES OF MY TICKET
            foreach (var restriction in restrictions.Where(r => r.Name.Contains("departure")))
            {
                var pos = restriction.PossiblePositions.First();
                result *= myTicket[pos];
            }

            #endregion

            var ms = Math.Round((DateTime.Now - start).TotalMilliseconds);

            if (result > 0) Answer2 = result.ToString();
            return $"Part 2 ({ms}ms): {result} ";
        }
    }
}