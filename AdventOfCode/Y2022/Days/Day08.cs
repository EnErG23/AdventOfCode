using AdventOfCode.Models;

namespace AdventOfCode.Y2022.Days
{
    public class Day08 : Day
    {
        private readonly List<List<int>> _forest;

        public Day08(int year, int day, bool test) : base(year, day, test)
        {
            _forest = new List<List<int>>();
            Inputs.ForEach(i => _forest.Add(i.ToList().Select(t => int.Parse(t.ToString())).ToList()));
        }

        public override string RunPart1()
        {
            long visibleTrees = (2 * _forest[0].Count()) + (2 * _forest.Count()) - 4;

            for (int r = 1; r < _forest.Count() - 1; r++)
            {
                for (int c = 1; c < _forest.First().Count() - 1; c++)
                {
                    var t = _forest[r][c];

                    //left
                    if (_forest[r].GetRange(0, c).Max() < t)
                    {
                        Console.WriteLine("Visible");
                        visibleTrees++;
                        continue;
                    }


                    //right
                    if (_forest[r].GetRange(c + 1, _forest[r].Count() - c - 1).Max() < t)
                    {
                        Console.WriteLine("Visible");
                        visibleTrees++;
                        continue;
                    }

                    //top
                    if (_forest.GetRange(0, r).Select(tr => tr[c]).Max() < t)
                    {
                        Console.WriteLine("Visible");
                        visibleTrees++;
                        continue;
                    }

                    //bottom
                    if (_forest.GetRange(r + 1, _forest.Count() - r - 1).Select(tr => tr[c]).Max() < t)
                    {
                        Console.WriteLine("Visible");
                        visibleTrees++;
                        continue;
                    }
                }
            }

            return visibleTrees.ToString();
        }

        public override string RunPart2()
        {
            long scenicScore = 0;

            for (int r = 1; r < _forest.Count() - 1; r++)
            {
                for (int c = 1; c < _forest.First().Count() - 1; c++)
                {
                    int t = _forest[r][c];
                    int ls = 0;
                    int rs = 0;
                    int ts = 0;
                    int bs = 0;

                    //left
                    for (int i = c - 1; i > -1; i--)
                    {
                        ls++;

                        if (_forest[r][i] >= t)
                            break;
                    }

                    //right
                    for (int i = c + 1; i < _forest[r].Count(); i++)
                    {
                        rs++;

                        if (_forest[r][i] >= t)
                            break;
                    }

                    //top
                    for (int i = r - 1; i > -1; i--)
                    {
                        ts++;

                        if (_forest[i][c] >= t)
                            break;
                    }

                    //bottom
                    for (int i = r + 1; i < _forest.Count(); i++)
                    {                        
                        bs++;

                        if (_forest[i][c] >= t)
                            break;
                    }

                    scenicScore = (ls * rs * ts * bs) > scenicScore ? (ls * rs * ts * bs) : scenicScore;
                }
            }

            return scenicScore.ToString();
        }
    }
}