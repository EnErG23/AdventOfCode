using AdventOfCode.Models;

namespace AdventOfCode.Y2020.Days
{
    public class Day25 : Day
    {
        private int? cardPublicKey;
        private int? doorPublicKey;
        private int divisor = 20201227;

        public Day25(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
        {
            cardPublicKey = int.Parse(Inputs[0]);
            doorPublicKey = int.Parse(Inputs[1]);

            List<long> table = new List<long>();

            int subjectNumber = 7;
            long value = 1;

            for (int i = 0; i < divisor; i++)
            {
                table.Add(value);
                value = (value * subjectNumber) % divisor;
            }

            return $"{Transform((int)cardPublicKey, table.IndexOf((int)doorPublicKey))}";
        }

        public override string RunPart2()
        {
            return "undefined";
        }

        private long Transform(int subjectNumber, int loopSize)
        {
            long result = 1;

            for (int i = 0; i < loopSize; i++)            
                result = (result * subjectNumber) % divisor;

            return result;
        }
    }
}
