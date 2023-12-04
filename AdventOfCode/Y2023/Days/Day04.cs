using AdventOfCode.Models;

namespace AdventOfCode.Y2023.Days
{
    public class Day04 : Day
    {
        private List<Game> _games;
        private double _totalScore => _games.Sum(g => g.Score);
        private int _totalCards
        {
            get
            {
                _games.ForEach(game => _games.Where(g => g.ID > game.ID && g.ID <= game.ID + game.Matches).ToList().ForEach(g => g.Cards += game.Matches > 0 ? game.Cards : 0));

                return _games.Sum(g => g.Cards);
            }
        }

        public Day04(int year, int day, bool test) : base(year, day, test)
        {
            _games = new List<Game>();

            foreach (var game in Inputs)
            {
                var id = int.Parse(game.Split(":")[0].Replace("   ", " ").Replace("  ", " ").Split(" ")[1]);
                var winningNumbers = game.Split(":")[1].Split("|")[0].Trim().Replace("  ", " ").Split(" ").Select(n => int.Parse(n)).ToList();
                var gameNumbers = game.Split(":")[1].Split("|")[1].Trim().Replace("  ", " ").Split(" ").Select(n => int.Parse(n)).ToList();

                _games.Add(new Game(id, winningNumbers, gameNumbers));
            }
        }

        public override string RunPart1() => _totalScore.ToString();

        public override string RunPart2() => _totalCards.ToString();
    }

    public class Game
    {
        public int ID { get; set; }
        public int Cards { get; set; }
        public List<int> WinningNumbers { get; set; }
        public List<int> Numbers { get; set; }
        public int Matches => Numbers.Count(n => WinningNumbers.Contains(n));
        public double Score => Matches > 0 ? Math.Pow(2, Convert.ToDouble(Numbers.Count(n => WinningNumbers.Contains(n)) - 1)) : 0;

        public Game(int iD, List<int> winningNumbers, List<int> numbers)
        {
            ID = iD;
            Cards = 1;
            WinningNumbers = winningNumbers;
            Numbers = numbers;
        }
    }
}