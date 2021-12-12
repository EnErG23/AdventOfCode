using AdventOfCode.Models;
using System.Diagnostics;

namespace AdventOfCode.Y2020.Days
{
    public class Day11 : Day
    {
        public Day11(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
        {
            List<List<char>> seats = Inputs.Select(i => i.ToList()).ToList();

            var change = true;
            var rule1 = false;

            while (change)
            {
                var newSeats = new List<List<char>>();

                change = false;
                rule1 = !rule1;

                for (int i = 0; i < seats.Count(); i++)
                {
                    var newRow = new List<char>();

                    for (int j = 0; j < seats[0].Count(); j++)
                    {
                        var seat = seats[i][j];

                        if (seat == '.')
                        {
                            newRow.Add('.');
                            continue;
                        }

                        var adjacentSeats = "";

                        try { adjacentSeats += seats[i - 1][j]; } catch { }         //U
                        try { adjacentSeats += seats[i - 1][j + 1]; } catch { }     //RU
                        try { adjacentSeats += seats[i][j + 1]; } catch { }         //R
                        try { adjacentSeats += seats[i + 1][j + 1]; } catch { }     //RD
                        try { adjacentSeats += seats[i + 1][j]; } catch { }         //D
                        try { adjacentSeats += seats[i + 1][j - 1]; } catch { }     //LD
                        try { adjacentSeats += seats[i][j - 1]; } catch { }         //L
                        try { adjacentSeats += seats[i - 1][j - 1]; } catch { }     //LU

                        if (rule1)
                            if (seat == 'L')
                                if (!adjacentSeats.Contains('#'))
                                {
                                    change = true;
                                    newRow.Add('#');
                                }
                                else
                                    newRow.Add('L');
                            else
                                newRow.Add('#');
                        else
                        {
                            if (seat == '#')
                            {
                                if (adjacentSeats.Count(a => a == '#') > 3)
                                {
                                    change = true;
                                    newRow.Add('L');
                                }
                                else
                                    newRow.Add('#');
                            }
                            else
                                newRow.Add('L');
                        }
                    }
                    newSeats.Add(newRow);
                }
                seats = newSeats;
            }

            return seats.Sum(r => r.Count(s => s == '#')).ToString();
        }

        public override string RunPart2()
        {
            List<List<char>> seats = Inputs.Select(i => i.ToList()).ToList();

            var change = true;
            var rule1 = false;

            while (change)
            {
                var newSeats = new List<List<char>>();

                change = false;
                rule1 = !rule1;

                for (int i = 0; i < seats.Count(); i++)
                {
                    var newRow = new List<char>();

                    for (int j = 0; j < seats[0].Count(); j++)
                    {
                        var seat = seats[i][j];

                        if (seat == '.')
                        {
                            newRow.Add('.');
                            continue;
                        }

                        var adjacentSeats = "";

                        //U
                        try
                        {
                            var noSeat = true;
                            var offset = 0;

                            while (noSeat)
                            {
                                offset++;
                                var checkSeat = seats[i - offset][j];
                                if (checkSeat == 'L' || checkSeat == '#')
                                {
                                    if (checkSeat == '#')
                                        adjacentSeats += checkSeat;

                                    noSeat = false;
                                }
                            }
                        }
                        catch { }

                        //RU
                        try
                        {
                            var noSeat = true;
                            var offset = 0;

                            while (noSeat)
                            {
                                offset++;
                                var checkSeat = seats[i - offset][j + offset];
                                if (checkSeat == 'L' || checkSeat == '#')
                                {
                                    if (checkSeat == '#')
                                        adjacentSeats += checkSeat;

                                    noSeat = false;
                                }
                            }
                        }
                        catch { }

                        //R
                        try
                        {
                            var noSeat = true;
                            var offset = 0;

                            while (noSeat)
                            {
                                offset++;
                                var checkSeat = seats[i][j + offset];
                                if (checkSeat == 'L' || checkSeat == '#')
                                {
                                    if (checkSeat == '#')
                                        adjacentSeats += checkSeat;

                                    noSeat = false;
                                }
                            }
                        }
                        catch { }

                        //RD
                        try
                        {
                            var noSeat = true;
                            var offset = 0;

                            while (noSeat)
                            {
                                offset++;
                                var checkSeat = seats[i + offset][j + offset];
                                if (checkSeat == 'L' || checkSeat == '#')
                                {
                                    if (checkSeat == '#')
                                        adjacentSeats += checkSeat;

                                    noSeat = false;
                                }
                            }
                        }
                        catch { }

                        //D
                        try
                        {
                            var noSeat = true;
                            var offset = 0;

                            while (noSeat)
                            {
                                offset++;
                                var checkSeat = seats[i + offset][j];
                                if (checkSeat == 'L' || checkSeat == '#')
                                {
                                    if (checkSeat == '#')
                                        adjacentSeats += checkSeat;

                                    noSeat = false;
                                }
                            }
                        }
                        catch { }

                        //LD
                        try
                        {
                            var noSeat = true;
                            var offset = 0;

                            while (noSeat)
                            {
                                offset++;
                                var checkSeat = seats[i + offset][j - offset];
                                if (checkSeat == 'L' || checkSeat == '#')
                                {
                                    if (checkSeat == '#')
                                        adjacentSeats += checkSeat;

                                    noSeat = false;
                                }
                            }
                        }
                        catch { }

                        //L
                        try
                        {
                            var noSeat = true;
                            var offset = 0;

                            while (noSeat)
                            {
                                offset++;
                                var checkSeat = seats[i][j - offset];
                                if (checkSeat == 'L' || checkSeat == '#')
                                {
                                    if (checkSeat == '#')
                                        adjacentSeats += checkSeat;

                                    noSeat = false;
                                }
                            }
                        }
                        catch { }

                        //LU
                        try
                        {
                            var noSeat = true;
                            var offset = 0;

                            while (noSeat)
                            {
                                offset++;
                                var checkSeat = seats[i - offset][j - offset];
                                if (checkSeat == 'L' || checkSeat == '#')
                                {
                                    if (checkSeat == '#')
                                        adjacentSeats += checkSeat;

                                    noSeat = false;
                                }
                            }
                        }
                        catch { }

                        if (rule1)
                            if (seat == 'L')
                            {
                                if (!adjacentSeats.Contains('#'))
                                {
                                    change = true;
                                    newRow.Add('#');
                                }
                                else
                                    newRow.Add('L');
                            }
                            else
                                newRow.Add('#');
                        else
                        {
                            if (seat == '#')
                            {
                                if (adjacentSeats.Count() > 4)
                                {
                                    change = true;
                                    newRow.Add('L');
                                }
                                else
                                    newRow.Add('#');
                            }
                            else
                                newRow.Add('L');
                        }
                    }
                    newSeats.Add(newRow);
                }
                seats = newSeats;
            }

            return seats.Sum(r => r.Count(s => s == '#')).ToString();
        }
    }
}