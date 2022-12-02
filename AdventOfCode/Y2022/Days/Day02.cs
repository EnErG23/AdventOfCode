using AdventOfCode.Models;

namespace AdventOfCode.Y2022.Days
{
    public class Day02 : Day
    {
        public Day02(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
        {
            int score = 0;

            foreach (var input in Inputs)
            {
                var game = input.Replace("A", "R").Replace("B", "P").Replace("C", "S").Replace("X", "R").Replace("Y", "P").Replace("Z", "S");

                switch(game.Split(" ")[0])
                {
                    case "R":
                        switch (game.Split(" ")[1])
                        {
                            case "R":
                                score += 4;
                                break;
                            case "P":
                                score += 8;
                                break;
                            case "S":
                                score += 3;
                                break;
                        }
                        break;
                    case "P":
                        switch (game.Split(" ")[1])
                        {
                            case "R":
                                score += 1;
                                break;
                            case "P":
                                score += 5;
                                break;
                            case "S":
                                score += 9;
                                break;
                        }
                        break;
                    case "S":
                        switch (game.Split(" ")[1])
                        {
                            case "R":
                                score += 7;
                                break;
                            case "P":
                                score += 2;
                                break;
                            case "S":
                                score += 6;
                                break;
                        }
                        break;
                }
            }

            return score.ToString();
        }

        public override string RunPart2()
        {
            int score = 0;

            foreach (var input in Inputs)
            {
                var game = input.Replace("A", "R").Replace("B", "P").Replace("C", "S");

                switch (game.Split(" ")[0])
                {
                    case "R":
                        switch (game.Split(" ")[1])
                        {
                            case "X":
                                score += 3;
                                break;
                            case "Y":
                                score += 4;
                                break;
                            case "Z":
                                score += 8;
                                break;
                        }
                        break;
                    case "P":
                        switch (game.Split(" ")[1])
                        {
                            case "X":
                                score += 1;
                                break;
                            case "Y":
                                score += 5;
                                break;
                            case "Z":
                                score += 9;
                                break;
                        }
                        break;
                    case "S":
                        switch (game.Split(" ")[1])
                        {
                            case "X":
                                score += 2;
                                break;
                            case "Y":
                                score += 6;
                                break;
                            case "Z":
                                score += 7;
                                break;
                        }
                        break;
                }
            }

            return score.ToString();
        }
    }
}