using AdventOfCode.Models;
using System.Diagnostics;
using AdventOfCode.Y2020.Models;

namespace AdventOfCode.Y2020.Days
{
    public class Day13 : Day
    {
        public Day13(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
        {
            long result = 0;

            var timeStamp = Convert.ToInt32(Inputs[0]);
            var buses = Inputs[1].Replace("x,", "").Split(',').Select(i => Convert.ToInt32(i));

            var iterateTimeStamp = timeStamp;

            while (true)
            {
                foreach (var bus in buses)
                {
                    if (iterateTimeStamp % bus == 0)
                    {
                        result = (iterateTimeStamp - timeStamp) * bus;
                        break;
                    }
                }
                if (result > 0) break;
                iterateTimeStamp++;
            }

			return result.ToString();
        }

        public override string RunPart2()
        {
            long result = 0;

            List<Bus> buses = Inputs[1].Split(',')
                              .Select(i => new Bus { ID = Convert.ToInt32(i.Replace("x", "0")), Pos = Inputs[1].Split(',').ToList().IndexOf(i) })
                              .Where(b => b.ID > 0)
                              .ToList();

            long[] n = buses.Select(b => b.ID).ToArray();
            long[] a = buses.Select(b => b.ID - b.Pos < 0 ? b.ID - (b.Pos % b.ID) : b.ID - b.Pos).ToArray();

            result = Solve(n, a);

			return result.ToString();
        }

        public static long Solve(long[] n, long[] a)
        {
            long prod = n.Aggregate(1, (long i, long j) => i * j);
            long p;
            long sm = 0;

            for (long i = 0; i < n.Length; i++)
            {
                p = prod / n[i];
                sm += a[i] * ModularMultiplicativeInverse(p, n[i]) * p;
            }

            return sm % prod;
        }

        private static long ModularMultiplicativeInverse(long a, long mod)
        {
            long b = a % mod;

            for (long x = 1; x < mod; x++)
            {
                if ((b * x) % mod == 1)
                {
                    return x;
                }
            }
            return 1;
        }
    }
}