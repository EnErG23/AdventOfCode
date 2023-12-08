using AdventOfCode.Models;

namespace AdventOfCode.Y2023.Days
{
    public class Day02 : Day
    {
        public Day02(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
        {
            int redCubes = 12;
            int greenCubes = 13;
            int blueCubes = 14;

            int gameSum = 0;

            foreach (var game in Inputs)
            {
                int gameID = int.Parse(game.Split(' ')[1].Replace(":", ""));
                string sets = game.Replace($"Game {gameID}: ", "");
                bool validGame = true;

                foreach (var set in sets.Split(";"))
                {
                    foreach (var setColor in set.Split(","))
                    {
                        int amount = int.Parse(setColor.Trim().Split(" ")[0]);
                        string color = setColor.Trim().Split(" ")[1];

                        switch (color)
                        {
                            case "red":
                                if (amount > redCubes)
                                    validGame = false;
                                break;
                            case "green":
                                if (amount > greenCubes)
                                    validGame = false;
                                break;
                            case "blue":
                                if (amount > blueCubes)
                                    validGame = false;
                                break;
                        }

                        if (!validGame)
                            break;
                    }

                    if (!validGame)
                        break;
                }

                gameSum += validGame ? gameID : 0;
            }

            return gameSum.ToString();
        }

        public override string RunPart2()
        {
            int gameSum = 0;

            foreach (var game in Inputs)
            {
                int minRedCubes = 0;
                int minGreenCubes = 0;
                int minBlueCubes = 0;

                int gameID = int.Parse(game.Split(' ')[1].Replace(":", ""));
                string sets = game.Replace($"Game {gameID}: ", "");

                foreach (var set in sets.Split(";"))
                {
                    int redCubes = 0;
                    int greenCubes = 0;
                    int blueCubes = 0;

                    foreach (var setColor in set.Split(","))
                    {
                        int amount = int.Parse(setColor.Trim().Split(" ")[0]);
                        string color = setColor.Trim().Split(" ")[1];

                        switch (color)
                        {
                            case "red":
                                redCubes = amount;
                                break;
                            case "green":
                                greenCubes = amount;
                                break;
                            case "blue":
                                blueCubes = amount;
                                break;
                        }
                    }

                    minRedCubes = Math.Max(minRedCubes, redCubes);
                    minGreenCubes = Math.Max(minGreenCubes, greenCubes);
                    minBlueCubes = Math.Max(minBlueCubes, blueCubes);
                }

                gameSum += (minRedCubes * minGreenCubes * minBlueCubes);
            }

            return gameSum.ToString();
        }
    }
}