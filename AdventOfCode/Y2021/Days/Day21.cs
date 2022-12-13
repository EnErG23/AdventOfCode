using AdventOfCode.Models;
using System.Text;

namespace AdventOfCode.Y2021.Days
{
    public class Day21 : Day
    {
        private long _p1Wins = 0;
        private long _p2Wins = 0;

        private Dictionary<int, int> _diceResultOccurrences;

        public Day21(int year, int day, bool test) : base(year, day, test)
            => _diceResultOccurrences = new() { { 3, 1 }, { 4, 3 }, { 5, 6 }, { 6, 7 }, { 7, 6 }, { 8, 3 }, { 9, 1 } };

        public override string RunPart1()
        {
            int p1Pos = int.Parse(Inputs[0].Last().ToString());
            int p2Pos = int.Parse(Inputs[1].Last().ToString());

            bool isP1Turn = true;

            int p1Score = 0;
            int p2Score = 0;

            int detDie = 1;
            int dieRolls = 0;

            while (true)
            {
                for (int i = 0; i < 3; i++)
                {
                    dieRolls++;

                    if (isP1Turn)
                        p1Pos = (p1Pos + detDie++) % 10;
                    else
                        p2Pos = (p2Pos + detDie++) % 10;

                    if (detDie > 100)
                        detDie = 1;
                }

                if (isP1Turn)
                    p1Score += p1Pos == 0 ? 10 : p1Pos;
                else
                    p2Score += p2Pos == 0 ? 10 : p2Pos;

                if (p1Score >= 1000 || p2Score >= 1000)
                    return (Math.Min(p1Score, p2Score) * dieRolls).ToString();

                isP1Turn = !isP1Turn;
            }
        }

        public override string RunPart2()
        {
            int p1Pos = int.Parse(Inputs[0].Last().ToString());
            int p2Pos = int.Parse(Inputs[1].Last().ToString());

            PlayQuantumGame(true, 0, 0, p1Pos, p2Pos, 1);

            return Math.Max(_p1Wins, _p2Wins).ToString();
        }

        private void PlayQuantumGame(bool isP1Turn, int p1Score, int p2Score, int p1Pos, int p2Pos, long occurrences)
        {
            foreach (int diceResult in _diceResultOccurrences.Keys)
                if (isP1Turn)
                {
                    int p1NewPos = (p1Pos + diceResult) % 10;
                    int p1NewScore = p1Score + (p1NewPos == 0 ? 10 : p1NewPos);
                    long totalOccurrences = occurrences * _diceResultOccurrences[diceResult];

                    if (p1NewScore >= 21)
                        _p1Wins += totalOccurrences;
                    else
                        PlayQuantumGame(false, p1NewScore, p2Score, p1NewPos, p2Pos, totalOccurrences);
                }
                else
                {
                    int p2NewPos = (p2Pos + diceResult) % 10;
                    int p2NewScore = p2Score + (p2NewPos == 0 ? 10 : p2NewPos);
                    long totalOccurrences = occurrences * _diceResultOccurrences[diceResult];

                    if (p2NewScore >= 21)
                        _p2Wins += totalOccurrences;
                    else
                        PlayQuantumGame(true, p1Score, p2NewScore, p1Pos, p2NewPos, totalOccurrences);
                }
        }
    }
}