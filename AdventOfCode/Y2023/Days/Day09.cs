using AdventOfCode.Models;

namespace AdventOfCode.Y2023.Days
{
    public class Day09 : Day
    {
        private List<List<List<int>>> _histories;

        public Day09(int year, int day, bool test) : base(year, day, test)
        {
            _histories = new();
            Inputs.ForEach(i => _histories.Add(new List<List<int>>() { i.Split(" ").Select(n => int.Parse(n)).ToList() }));

            foreach (var history in _histories)
            {
                List<List<int>> sequences = new() { history.First() };

                while (!sequences.Last().TrueForAll(d => d == 0))
                {
                    var lastSequence = sequences.Last();

                    List<int> newSequence = new List<int>();

                    for (int i = 0; i < lastSequence.Count - 1; i++)
                        newSequence.Add(lastSequence[i + 1] - lastSequence[i]);

                    sequences.Add(newSequence);
                }

                sequences.Last().Insert(0, 0);
                sequences.Last().Add(0);

                for (int i = sequences.Count - 2; i >= 0; i--)
                {
                    sequences[i].Insert(0, sequences[i].First() - sequences[i + 1].First());
                    sequences[i].Add(sequences[i].Last() + sequences[i + 1].Last());
                }
            }
        }

        public override string RunPart1() => _histories.Sum(h => h.First().Last()).ToString();

        public override string RunPart2() => _histories.Sum(h => h.First().First()).ToString();
    }
}