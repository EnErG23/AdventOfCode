using AdventOfCode.Models;

namespace AdventOfCode.Y2022.Days
{
    public class Day09 : Day
    {
        private List<(int, int)> _tailVisitedPositions;

        public Day09(int year, int day, bool test) : base(year, day, test)
        {
            _tailVisitedPositions = new List<(int, int)>() { (0, 0) };
        }

        public override string RunPart1()
        {
            _tailVisitedPositions = new List<(int, int)>();

            var headPos = (0, 0);
            var tailPos = (0, 0);

            _tailVisitedPositions.Add(tailPos);

            foreach (var input in Inputs)
            {
                var move = input.Split(" ");

                for (int i = 0; i < int.Parse(move[1]); i++)
                {
                    switch (move[0])
                    {
                        case "L":
                            headPos.Item1--;
                            break;
                        case "R":
                            headPos.Item1++;
                            break;
                        case "U":
                            headPos.Item2++;
                            break;
                        case "D":
                            headPos.Item2--;
                            break;
                    }

                    tailPos = MoveTail(headPos, tailPos);
                    _tailVisitedPositions.Add(tailPos);
                }
            }

            return _tailVisitedPositions.Distinct().Count().ToString();
        }

        public override string RunPart2()
        {
            _tailVisitedPositions = new List<(int, int)>();

            List<(int, int)> ropePositions = new List<(int, int)>();

            for (int i = 0; i < 10; i++)
                ropePositions.Add((0, 0));

            foreach (var input in Inputs)
            {
                //Console.WriteLine($"== {input} ==");

                var move = input.Split(" ");

                for (int i = 0; i < int.Parse(move[1]); i++)
                {
                    switch (move[0])
                    {
                        case "L":
                            ropePositions[0] = (ropePositions[0].Item1 - 1, ropePositions[0].Item2);
                            break;
                        case "R":
                            ropePositions[0] = (ropePositions[0].Item1 + 1, ropePositions[0].Item2);
                            break;
                        case "U":
                            ropePositions[0] = (ropePositions[0].Item1, ropePositions[0].Item2 + 1);
                            break;
                        case "D":
                            ropePositions[0] = (ropePositions[0].Item1, ropePositions[0].Item2 - 1);
                            break;
                    }

                    //Console.WriteLine($"H: ({ropePositions[0].Item1}, {ropePositions[0].Item2})");

                    for (int j = 1; j < 10; j++)
                    {
                        ropePositions[j] = MoveTail(ropePositions[j - 1], ropePositions[j]);
                        //Console.WriteLine($"{j}: ({ropePositions[j].Item1}, {ropePositions[j].Item2})");
                    }

                    //Console.WriteLine("---------");

                    _tailVisitedPositions.Add(ropePositions[9]);
                }

                //Console.WriteLine($"H: ({ropePositions[0].Item1}, {ropePositions[0].Item2})");

                //for (int j = 1; j < 10; j++)
                //    Console.WriteLine($"{j}: ({ropePositions[j].Item1}, {ropePositions[j].Item2})");

                //Console.WriteLine("---------");

                //Console.WriteLine();
                //Console.Read();
            }

            //foreach (var p in _tailVisitedPositions.Distinct().ToList())
            //{
            //    Console.WriteLine($"({p.Item1}, {p.Item2})");
            //    Console.Read();
            //}

            return _tailVisitedPositions.Distinct().Count().ToString();
        }

        public (int, int) MoveTail((int, int) headPos, (int, int) tailPos)
        {
            (int, int) tailNewPos = tailPos;

            int diffH = Math.Abs(headPos.Item1 - tailPos.Item1);
            int diffV = Math.Abs(headPos.Item2 - tailPos.Item2);

            if (diffH > 1)
            {
                if (diffV > 1)
                    tailNewPos.Item2 += headPos.Item2 - tailPos.Item2 + (headPos.Item2 > tailPos.Item2 ? -1 : 1);
                else if (diffV > 0)
                    tailNewPos.Item2 += headPos.Item2 - tailPos.Item2;

                tailNewPos.Item1 += headPos.Item1 - tailPos.Item1 + (headPos.Item1 > tailPos.Item1 ? -1 : 1);
            }
            else if (diffV > 1)
            {
                if (diffH > 1)
                    tailNewPos.Item1 += headPos.Item1 - tailPos.Item1 + (headPos.Item1 > tailPos.Item1 ? -1 : 1);
                else if (diffH > 0)
                    tailNewPos.Item1 += headPos.Item1 - tailPos.Item1;

                tailNewPos.Item2 += headPos.Item2 - tailPos.Item2 + (headPos.Item2 > tailPos.Item2 ? -1 : 1);
            }

            return tailNewPos;
        }

        //public string RunPart2EzFixAttempt()
        //{
        //    var headPos = (0, 0);
        //    var tailPos = (0, 0);

        //    _tailVisitedPositions.Add(tailPos);

        //    foreach (var input in Inputs)
        //    {
        //        var move = input.Split(" ");

        //        for (int i = 0; i < int.Parse(move[1]); i++)
        //        {
        //            switch (move[0])
        //            {
        //                case "L":
        //                    headPos.Item1--;
        //                    break;
        //                case "R":
        //                    headPos.Item1++;
        //                    break;
        //                case "U":
        //                    headPos.Item2++;
        //                    break;
        //                case "D":
        //                    headPos.Item2--;
        //                    break;
        //            }

        //            tailPos = MoveTail(10, headPos, tailPos);
        //            _tailVisitedPositions.Add(tailPos);
        //        }
        //    }

        //    return _tailVisitedPositions.Distinct().Count().ToString();
        //}

        //public (int, int) MoveTailEZAttempt(int length, (int, int) headPos, (int, int) tailPos)
        //{
        //    (int, int) tailNewPos = tailPos;

        //    int diffH = Math.Abs(headPos.Item1 - tailPos.Item1);
        //    int diffV = Math.Abs(headPos.Item2 - tailPos.Item2);

        //    if (diffH > length - 1)
        //    {
        //        if (diffV > 0)
        //            tailNewPos.Item2 += headPos.Item2 - tailPos.Item2;

        //        tailNewPos.Item1 += headPos.Item1 - tailPos.Item1 + (headPos.Item1 > tailPos.Item1 ? -length : length);
        //    }
        //    else if (diffV > length - 1)
        //    {
        //        if (diffH > 0)
        //            tailNewPos.Item1 += headPos.Item1 - tailPos.Item1;

        //        tailNewPos.Item2 += headPos.Item2 - tailPos.Item2 + (headPos.Item2 > tailPos.Item2 ? -length : length);
        //    }

        //    return tailNewPos;
        //}
    }
}