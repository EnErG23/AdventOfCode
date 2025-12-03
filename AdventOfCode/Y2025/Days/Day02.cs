using AdventOfCode.Models;

namespace AdventOfCode.Y2025.Days
{
    public class Day02 : Day
    {
        public Day02(int year, int day, bool test) : base(year, day, test)
            => Inputs = Inputs[0].Trim().Split(",").ToList();

        public override string RunPart1()
        {
            long result = 0;

            foreach (var input in Inputs)
            {
                for (var id = long.Parse(input.Split("-")[0]); id <= long.Parse(input.Split("-")[1]); id++)
                {
                    var idString = id.ToString();

                    if (idString.Length % 2 == 0)
                    {
                        var id1 = idString[..(idString.Length / 2)];
                        var id2 = idString[(idString.Length / 2)..];

                        if (id1 == id2)
                            result += id;
                    }
                }
            }

            return result.ToString();
        }

        public override string RunPart2()
        {
            long result = 0;

            foreach (var input in Inputs)
            {
                for (var id = long.Parse(input.Split("-")[0]); id <= long.Parse(input.Split("-")[1]); id++)
                {
                    var idString = id.ToString();

                    for (int i = 1; i <= idString.Length / 2; i++)
                    {
                        var id1 = idString[..i];
                        var counter = i;

                        var isInvalid = true;

                        while (counter < idString.Length)
                        {
                            try
                            {
                                if (idString.Substring(counter, i) != id1)
                                {
                                    isInvalid = false;
                                    break;
                                }
                            }
                            catch
                            {
                                isInvalid = false;
                                break;
                            }

                            counter += i;
                        }

                        if (isInvalid)
                        {                         
                            result += id;
                            break;
                        }
                    }
                }
            }

            return result.ToString();
        }
    }
}