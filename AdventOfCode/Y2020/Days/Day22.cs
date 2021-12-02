using AdventOfCode.Helpers;

namespace AdventOfCode.Y2020.Days
{
    public static class Day22
    {
        static int day = 22;
        static List<string>? inputs;
        public static List<List<int>> decks = new List<List<int>>();
        public static int game = 1;

        public static string? Answer1 { get; set; }
        public static string? Answer2 { get; set; }

        public static void Run(int part, bool test)
        {
            inputs = InputManager.GetInputAsStrings(day, test);

            var start = DateTime.Now;

            string part1 = "";
            string part2 = "";

            if (part == 1)
                part1 = Part1();
            else if (part == 2)
                part2 = Part2();
            else
            {
                part1 = Part1();
                part2 = Part2();
            }

            var ms = Math.Round((DateTime.Now - start).TotalMilliseconds);

            Console.WriteLine($"Day {day} ({ms}ms):");
            if (part1 != "") Console.WriteLine($"    {part1}");
            if (part2 != "") Console.WriteLine($"    {part2}");
        }

        static string Part1()
        {
            long result = 0;

            var start = DateTime.Now;

            #region Solution

            List<int> deck = new List<int>();

            foreach (var input in inputs)
            {
                if (input.Contains(":"))
                    continue;
                else if (input == "")
                {
                    decks.Add(deck);
                    deck = new List<int>();
                }
                else
                    deck.Add(Convert.ToInt32(input));
            }
            decks.Add(deck);

            //WriteDecks();

            PlayGame();

            //WriteDecks();

            var winnersDeck = decks.Find(d => d.Count > 0);

            int i = winnersDeck.Count();

            foreach (var card in winnersDeck)
            {
                result += card * i;
                i--;
            }

            #endregion

            var ms = Math.Round((DateTime.Now - start).TotalMilliseconds);

            if (result > 0) Answer1 = result.ToString();
            return $"Part 1 ({ms}ms): {result} ";
        }

        static string Part2()
        {
            long result = 0;

            var start = DateTime.Now;

            #region Solution

            decks = new List<List<int>>();
            List<int> deck = new List<int>();

            foreach (var input in inputs)
            {
                if (input.Contains(":"))
                    continue;
                else if (input == "")
                {
                    decks.Add(deck);
                    deck = new List<int>();
                }
                else
                    deck.Add(Convert.ToInt32(input));
            }
            decks.Add(deck);

            var winner = PlayRecursiveGame(decks);

            var winnersDeck = decks[winner - 1];

            int i = winnersDeck.Count();

            foreach (var card in winnersDeck)
            {
                result += card * i;
                i--;
            }

            #endregion

            var ms = Math.Round((DateTime.Now - start).TotalMilliseconds);

            if (result > 0) Answer2 = result.ToString();
            return $"Part 2 ({ms}ms): {result} ";
        }

        static void WriteDecks()
        {
            var i = 0;
            foreach (var d in decks)
            {
                i++;
                Console.WriteLine($"Player {i}'s deck: {string.Join(", ", d)}");
            }
        }

        static void WriteDecks(List<List<int>> writeDecks)
        {
            var i = 0;
            foreach (var d in writeDecks)
            {
                i++;
                Console.WriteLine($"Deck {i}: {string.Join(", ", d)}");
            }
        }

        static void PlayGame()
        {
            while (decks.Where(d => d.Count() == 0).Count() == 0)
            {
                int card1 = decks[0][0];
                int card2 = decks[1][0];

                decks[0].Remove(card1);
                decks[1].Remove(card2);

                if (card1 > card2)
                {
                    decks[0].Add(card1);
                    decks[0].Add(card2);
                }
                else
                {
                    decks[1].Add(card2);
                    decks[1].Add(card1);
                }
            }
        }

        static int PlayRecursiveGame(List<List<int>> gameDecks)
        {
            var result = 0;

            var round = 1;
            var currentGame = game++;

            var player1Decks = new List<List<int>>();
            var player2Decks = new List<List<int>>();

            while (gameDecks.Where(d => d.Count() == 0).Count() == 0)
            {
                var currentRound = round++;

                int card1 = gameDecks[0][0];
                int card2 = gameDecks[1][0];

                player1Decks.Add(gameDecks[0].ToList());
                player2Decks.Add(gameDecks[1].ToList());

                // SAME DECKS RULE
                if (player1Decks.GetRange(0, currentRound - 1).Where(d => d.SequenceEqual(gameDecks[0])).Count() > 0 || player2Decks.GetRange(0, currentRound - 1).Where(d => d.SequenceEqual(gameDecks[1])).Count() > 0)
                {
                    result = 1;
                    break;
                }

                // SUB GAME RULE
                if (card1 < gameDecks[0].Count() && card2 < gameDecks[1].Count())
                {
                    var subDecks = new List<List<int>>() { gameDecks[0].GetRange(1, card1), gameDecks[1].GetRange(1, card2) };

                    result = PlayRecursiveGame(subDecks);
                }
                // REGULAR GAME RULE
                else
                {
                    if (card1 > card2)
                    {
                        result = 1;
                    }
                    else
                    {
                        result = 2;
                    }
                }

                gameDecks[0].Remove(card1);
                gameDecks[1].Remove(card2);

                if (result == 1)
                {
                    gameDecks[0].Add(card1);
                    gameDecks[0].Add(card2);
                }
                else
                {
                    gameDecks[1].Add(card2);
                    gameDecks[1].Add(card1);
                }
            }

            return result;
        }
    }
}