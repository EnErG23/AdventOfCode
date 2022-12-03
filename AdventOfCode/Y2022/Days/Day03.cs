using AdventOfCode.Models;

namespace AdventOfCode.Y2022.Days
{
    public class Day03 : Day
    {
        public Day03(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
        {
            var totalPriority = 0;

            foreach (var input in Inputs)
            {
                string firstCompartment = input.Substring(0, input.Length / 2);
                string secondCompartment = input.Substring(input.Length / 2, input.Length / 2);

                foreach (var item in firstCompartment)
                {
                    if (secondCompartment.Contains(item))
                    {
                        totalPriority += Char.IsUpper(item) ? ((int)item) - 38 : ((int)item) - 96;
                        break;
                    }
                }
            }

            return totalPriority.ToString();
        }

        public override string RunPart2()
        {
            var totalPriority = 0;

            for (var i = 0; i < Inputs.Count(); i += 3)
            {
                string firstElf = Inputs[i];
                string secondElf = Inputs[i + 1];
                string thirdElf = Inputs[i + 2];

                foreach (var item in firstElf)
                {
                    if (secondElf.Contains(item) && thirdElf.Contains(item))
                    {                        
                        totalPriority += Char.IsUpper(item) ? ((int)item) - 38 : ((int)item) - 96;
                        break;
                    }
                }
            }

            return totalPriority.ToString();
        }
    }
}