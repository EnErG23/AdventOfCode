using AdventOfCode.Models;

namespace AdventOfCode.Y2023.Days
{
    public class Day04 : Day
    {
        public Day04(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
        {
            double score = 0;

            foreach (var game in Inputs)
            {
                var winningNumbers = game.Split(":")[1].Split("|")[0].Trim().Replace("  ", " ").Split(" ").Select(n => int.Parse(n)).ToList();
                var gameNumbers = game.Split(":")[1].Split("|")[1].Trim().Replace("  ", " ").Split(" ").Select(n => int.Parse(n)).ToList();

                if ((gameNumbers.Count(g => winningNumbers.Contains(g)) > 0))
                    score += Math.Pow(2, Convert.ToDouble(gameNumbers.Count(g => winningNumbers.Contains(g)) - 1));
            }

            return score.ToString();
        }

        public override string RunPart2()
        {
            var games = new List<Game> { };

            foreach (var game in Inputs)
            {
                var id = int.Parse(game.Split(":")[0].Replace("   "," ").Replace("  ", " ").Split(" ")[1]);
                var winningNumbers = game.Split(":")[1].Split("|")[0].Trim().Replace("  ", " ").Split(" ").Select(n => int.Parse(n)).ToList();
                var gameNumbers = game.Split(":")[1].Split("|")[1].Trim().Replace("  ", " ").Split(" ").Select(n => int.Parse(n)).ToList();

                games.Add(new Game(id, winningNumbers, gameNumbers));
            }

            int maxID = games.Max(g => g.ID);

            for (int i = 1; i <= maxID; i++)
            {
                var game = games.First(g => g.ID == i);

                if (game.Matches > 0)
                {
                    var cards = games.Count(g => g.ID == i);

                    for (int c = 0; c < cards; c++)
                        for (int j = 1; j <= game.Matches; j++)
                            if (game.ID + j <= maxID)
                                games.Add(games.First(g => g.ID == game.ID + j));
                }
            }

            return games.Count().ToString();
        }
    }

    public class Game
    {
        public int ID { get; set; }
        public List<int> WinningNumbers { get; set; }
        public List<int> Numbers { get; set; }
        public int Matches
        {
            get
            {
                return Numbers.Count(n => WinningNumbers.Contains(n));
            }

        }
        public double Score
        {
            get
            {
                return Matches > 0 ? Math.Pow(2, Matches - 1) : 0;
            }
        }

        public Game(int iD, List<int> winningNumbers, List<int> numbers)
        {
            ID = iD;
            WinningNumbers = winningNumbers;
            Numbers = numbers;
        }
    }
}