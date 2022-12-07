using AdventOfCode.Models;
using System.Text;

namespace AdventOfCode.Y2021.Days
{
    public class Day21 : Day
    {
        private long _p1Wins = 0;
        private long _p2Wins = 0;

        public Day21(int year, int day, bool test) : base(year, day, test) { }

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
                    {
                        p1Pos = (p1Pos + detDie++) % 10;
                    }
                    else
                    {
                        p2Pos = (p2Pos + detDie++) % 10;
                    }

                    if (detDie > 100)
                        detDie = 1;
                }

                if (isP1Turn)
                    p1Score += p1Pos == 0 ? 10 : p1Pos;
                else
                    p2Score += p2Pos == 0 ? 10 : p2Pos;

                if (p1Score >= 1000 || p2Score >= 1000)
                {
                    return (Math.Min(p1Score, p2Score) * dieRolls).ToString();
                }

                isP1Turn = !isP1Turn;
            }
        }

        public override string RunPart2()
        {
            return "undefined";

            int p1Pos = int.Parse(Inputs[0].Last().ToString());
            int p2Pos = int.Parse(Inputs[1].Last().ToString());

            PlayGame(true, 0, 0, p1Pos, p2Pos);

            return Math.Max(_p1Wins, _p2Wins).ToString();
        }

        private void PlayGame(bool isP1Turn, int p1Score, int p2Score, int p1Pos, int p2Pos)
        {
            Console.WriteLine($"{_p1Wins} vs {_p2Wins}");
            //Console.WriteLine($"{(isP1Turn ? 1 : 2)} ({turn}): {p1Pos} ({p1Score}) vs {p2Pos} ({p2Score})");

            if (isP1Turn)
            {
                for (int i = 1; i < 4; i++)
                {
                    for (int j = 1; j < 4; j++)
                    {
                        for (int k = 1; k < 4; k++)
                        {
                            int p1NewPos = (p1Pos + i + j + k) % 10;
                            int p1NewScore = p1Score + (p1NewPos == 0 ? 10 : p1NewPos);

                            if (p1NewScore >= 21)
                            {
                                _p1Wins++;
                                return;
                            }

                            PlayGame(false, p1NewScore, p2Score, p1NewPos, p2Pos);
                        }
                    }
                }
            }
            else
            {
                for (int i = 1; i < 4; i++)
                {
                    for (int j = 1; j < 4; j++)
                    {
                        for (int k = 1; k < 4; k++)
                        {
                            int p2NewPos = (p2Pos + i + j + k) % 10;
                            int p2NewScore = p2Score + (p2NewPos == 0 ? 10 : p2NewPos);

                            if (p2NewScore >= 21)
                            {
                                _p2Wins++;
                                break;
                            }

                            PlayGame(true, p1Score, p2NewScore, p1Pos, p2NewPos);
                        }
                    }
                }
            }
        }
    }
}