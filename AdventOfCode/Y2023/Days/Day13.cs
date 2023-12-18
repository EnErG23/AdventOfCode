using AdventOfCode.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace AdventOfCode.Y2023.Days
{
    public class Day13 : Day
    {
        private List<List<string>> _patterns;
        private List<(bool, int)> _usedMirrors;

        public Day13(int year, int day, bool test) : base(year, day, test)
        {
            _patterns = new();
            _usedMirrors = new();
            List<string> pattern = new();

            foreach (var input in Inputs)
                if (input != "")
                    pattern.Add(input);
                else
                {
                    _patterns.Add(pattern);
                    pattern = new();
                }

            _patterns.Add(pattern);
        }

        public override string RunPart1() => _patterns.Sum(p => GetNotes(p)).ToString();

        public override string RunPart2() => _patterns.Select((p, i) => GetNewNotes(p, i)).Sum().ToString();

        private int GetNotes(List<string> pattern)
        {
            //Check horizontal mirrors
            var mirrors = GetMirrors(pattern, false);

            foreach (var r in mirrors)
                if (ValidMirror(pattern, r, false))
                {
                    _usedMirrors.Add((false, r));
                    return (r + 1) * 100;
                }

            //Check vertical mirrors
            pattern = RotatePattern(pattern);
            mirrors = GetMirrors(pattern, false);

            foreach (var r in mirrors)
                if (ValidMirror(pattern, r, false))
                {
                    _usedMirrors.Add((true, r));
                    return r + 1;
                }

            return 0;
        }

        private int GetNewNotes(List<string> pattern, int i)
        {
            //Get used mirrors in part 1
            if (_usedMirrors.Count == 0)
                _patterns.ForEach(p => GetNotes(p));

            //Check horizontal mirrors
            var mirrors = GetMirrors(pattern, true);

            foreach (var r in mirrors)
                if (!(_usedMirrors[i] == (false, r)) && ValidMirror(pattern, r, true))
                    return (r + 1) * 100;

            //Check vertical mirrors
            pattern = RotatePattern(pattern);
            mirrors = GetMirrors(pattern, true);

            foreach (var r in mirrors)
                if (!(_usedMirrors[i] == (true, r)) && ValidMirror(pattern, r, true))
                    return r + 1;

            return 0;
        }

        private bool ValidMirror(List<string> pattern, int r, bool fixSmudge)
        {
            var rowsBelow = pattern.Count - r - 2;
            var rowsToTake = Math.Min(r, rowsBelow);

            var topRows = pattern.Skip(r > rowsBelow ? r - rowsToTake : 0).Take(rowsToTake).ToList();
            var bottomRows = pattern.Skip(r + 2).Take(rowsToTake).Reverse().ToList();

            bool allEqual = true;
            bool smudgeFixed = false;

            for (int i = 0; i < topRows.Count; i++)
            {
                var topRow = topRows[i];
                var bottomRow = bottomRows[i];

                if (!smudgeFixed && fixSmudge)
                {
                    int differences = 0;
                    int pos = 0;

                    for (int j = 0; j < topRow.Length; j++)
                    {
                        if (topRow[j] != bottomRow[j])
                        {
                            differences++;
                            pos = j;
                        }

                        if (differences > 1)
                            break;
                    }

                    if (differences == 1)
                    {
                        smudgeFixed = true;
                        continue;
                    }
                }

                if (topRow != bottomRow)
                {
                    allEqual = false;
                    break;
                }
            }

            return allEqual;
        }

        private List<int> GetMirrors(List<string> pattern, bool fixSmudge)
        {
            List<int> result = new List<int>();

            for (int r = 0; r < pattern.Count - 1; r++)
            {
                var firstRow = pattern[r];
                var secondRow = pattern[r + 1];

                if (fixSmudge)
                {
                    int differences = 0;
                    int pos = 0;

                    for (int j = 0; j < firstRow.Length; j++)
                    {
                        if (firstRow[j] != secondRow[j])
                        {
                            differences++;
                            pos = j;
                        }

                        if (differences > 1)
                            break;
                    }

                    if (differences == 1)
                    {
                        result.Add(r);
                        continue;
                    }
                }

                if (firstRow == secondRow)
                    result.Add(r);
            }

            return result;
        }

        private List<string> RotatePattern(List<string> pattern)
        {
            List<string> rotatedpattern = new();

            for (int i = 0; i < pattern[0].Count(); i++)
                rotatedpattern.Add(string.Join("", pattern.Select(m => m[i])));

            return rotatedpattern;
        }
    }
}