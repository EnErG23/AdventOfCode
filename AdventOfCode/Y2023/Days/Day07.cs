using AdventOfCode.Models;
using System.ComponentModel;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;

namespace AdventOfCode.Y2023.Days
{
    public class Day07 : Day
    {
        private List<Hand> _hands;
        private int _rank1 = 1;
        private int _rank2 = 1;

        public Day07(int year, int day, bool test) : base(year, day, test) => _hands = Inputs.Select(i => new Hand(i.Split(" ")[0], int.Parse(i.Split(" ")[1]))).ToList();

        public override string RunPart1() => _hands.OrderBy(h => h.Type).ThenBy(h => h.Value).Sum(h => h.Bid * _rank1++).ToString();

        public override string RunPart2() => _hands.OrderBy(h => h.Type2).ThenBy(h => h.Value2).Sum(h => h.Bid * _rank2++).ToString();
    }

    public class Hand
    {
        private string _cardsString;
        private List<char> Cards => _cardsString.ToList();
        public int Bid { get; set; }
        public int Type
        {
            get
            {
                switch (Cards.Distinct().Count())
                {
                    case 1: // Five of a kind
                        return 6;
                    case 2: // Four of a kind || Full house
                        return Cards.GroupBy(c => c).Max(g => g.Count()) + 1;
                    case 3: // Three of a kind || Two pair
                        return Cards.GroupBy(c => c).Max(g => g.Count());
                    case 4: // One Pair
                        return 1;
                    default: // High Card
                        return 0;
                }
            }
        }
        public int Type2
        {
            get
            {
                string newCardsString = _cardsString;

                if (newCardsString.Contains('J') && newCardsString != "JJJJJ")
                {
                    var cardGroup = Cards.Where(c => c != 'J').GroupBy(c => c);
                    var maxCount = cardGroup.Max(g => g.Count());

                    newCardsString = newCardsString.Replace("J", cardGroup.First(x => x.Count() == maxCount).Key.ToString());
                }

                var newCards = newCardsString.ToList();

                switch (newCards.Distinct().Count())
                {
                    case 1: // Five of a kind
                        return 6;
                    case 2: // Four of a kind || Full house
                        return newCards.GroupBy(c => c).Max(g => g.Count()) + 1;
                    case 3: // Three of a kind || Two pair
                        return newCards.GroupBy(c => c).Max(g => g.Count());
                    case 4: // One Pair
                        return 1;
                    default: // High Card
                        return 0;
                }
            }
        }
        public string Value
        {
            get
            {
                var newString = char.IsDigit(Cards[0]) ? _cardsString : $"{_cardsString}";

                foreach (var label in new List<(string, string)> { ("A", "E"), ("K", "D"), ("Q", "C"), ("J", "B"), ("T", "A") })
                    newString = newString.Replace(label.Item1, label.Item2);

                return newString;
            }
        }
        public string Value2
        {
            get
            {
                var newString = char.IsDigit(Cards[0]) ? _cardsString : $"{_cardsString}";

                foreach (var label in new List<(string, string)> { ("A", "E"), ("K", "D"), ("Q", "C"), ("J", "1"), ("T", "A") })
                    newString = newString.Replace(label.Item1, label.Item2);

                return newString;
            }
        }

        public Hand(string cardsString, int bid)
        {
            _cardsString = cardsString;
            Bid = bid;
        }
    }
}