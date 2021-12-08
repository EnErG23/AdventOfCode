using AdventOfCode.Models;

namespace AdventOfCode.Y2020.Days
{
    public class Day01 : Day
    {
        private List<int>? inputs;

        public Day01(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
        {
            inputs = Inputs.Select(i => int.Parse(i)).ToList();

            long result = 0;

            foreach (int x in inputs)
            {
                foreach (int y in inputs)
                {
                    if (x + y == 2020) 
                        result = x * y;

                    if (result != 0) break;
                }
                if (result != 0) break;
            }

            return result.ToString();;

            //foreach (int x in Inputs)
            //    foreach (int y in Inputs)
            //        if (x + y == 2020)
            //            return (x * y).ToString();

            //return Inputs[0].ToString();
        }

        public override string RunPart2()
        {
            if (inputs is null)
                inputs = Inputs.Select(i => int.Parse(i)).ToList();

            foreach (int x in inputs)
                foreach (int y in inputs)
                    foreach (int z in inputs)
                        if (x + y + z == 2020)
                            return (x * y * z).ToString();

            return Inputs[0].ToString();
        }
    }
}