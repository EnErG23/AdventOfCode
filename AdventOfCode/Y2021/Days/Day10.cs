using AdventOfCode.Models;

namespace AdventOfCode.Y2021.Days
{
    public class Day10 : Day
    {
        private List<string>? completionStrings;

        public Day10(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
        {
            return RemoveCorruptedAndCreateCompletionStrings().ToString();
        }

        public override string RunPart2()
        {
            if (completionStrings == null)
                RemoveCorruptedAndCreateCompletionStrings();

            List<long> results = new();

            foreach (var completionString in completionStrings)
            {
                long score = 0;

                foreach (var c in completionString)
                {
                    score *= 5;

                    if (c == ')')
                        score += 1;
                    else if (c == ']')
                        score += 2;
                    else if (c == '}')
                        score += 3;
                    else if (c == '>')
                        score += 4;
                }

                results.Add(score);
            }

            return results.OrderBy(r => r).ToList()[results.Count / 2].ToString();
        }

        public long RemoveCorruptedAndCreateCompletionStrings()
        {
            long result = 0;
            completionStrings = new();

            foreach (var input in Inputs)
            {
                List<char> expectedChars = new();
                char corruptedChar = ' ';

                foreach (char c in input)
                {
                    switch (c)
                    {
                        case '(':
                            expectedChars.Add(')');
                            break;
                        case '[':
                            expectedChars.Add(']');
                            break;
                        case '{':
                            expectedChars.Add('}');
                            break;
                        case '<':
                            expectedChars.Add('>');
                            break;
                        default:
                            if (expectedChars.Last() == c)
                                expectedChars.RemoveAt(expectedChars.Count - 1);
                            else
                                corruptedChar = c;
                            break;
                    }

                    if (corruptedChar != ' ')
                    {
                        if (corruptedChar == ')')
                            result += 3;
                        else if (corruptedChar == ']')
                            result += 57;
                        else if (corruptedChar == '}')
                            result += 1197;
                        else if (corruptedChar == '>')
                            result += 25137;

                        break;
                    }
                }

                expectedChars.Reverse();

                if (corruptedChar == ' ')
                    completionStrings.Add(String.Join("", expectedChars));
            }

            return result;
        }
    }
}