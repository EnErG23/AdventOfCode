using AdventOfCode.Models;

namespace AdventOfCode.Y2024.Days
{
    public class Day08 : Day
    {
        private List<Location> _antennas = new();

        public Day08(int year, int day, bool test) : base(year, day, test)
        {
            for (int r = 0; r < Inputs.Count(); r++)
                for (int c = 0; c < Inputs[r].Count(); c++)
                    if (Inputs[r][c] != '.')
                        _antennas.Add(new Location(r, c, Inputs[r][c]));
        }

        public override string RunPart1()
        {
            List<Location> antinodes = new();
            List<Location> checkedAntennas = new();

            foreach (var antenna in _antennas)
            {
                foreach (var secondAntenna in _antennas.Where(a => a.Value == antenna.Value && a != antenna && !checkedAntennas.Contains(a)))
                {
                    var difference = (secondAntenna.Row - antenna.Row, secondAntenna.Column - antenna.Column);
                    var antinode1 = new Location(antenna.Row - difference.Item1, antenna.Column - difference.Item2, '#');
                    var antinode2 = new Location(secondAntenna.Row + difference.Item1, secondAntenna.Column + difference.Item2, '#');

                    if (antinode1.Row > -1 && antinode1.Row < Inputs.Count() && antinode1.Column > -1 && antinode1.Column < Inputs[0].Count() && !antinodes.Contains(antinode1))
                        antinodes.Add(antinode1);

                    if (antinode2.Row > -1 && antinode2.Row < Inputs.Count() && antinode2.Column > -1 && antinode2.Column < Inputs[0].Count() && !antinodes.Contains(antinode2))
                        antinodes.Add(antinode2);
                }

                checkedAntennas.Add(antenna);
            }

            return antinodes.Select(a => (a.Row, a.Column)).Distinct().Count().ToString();
        }

        public override string RunPart2()
        {
            List<Location> antinodes = new();
            List<Location> checkedAntennas = new();

            foreach (var antenna in _antennas)
            {
                foreach (var secondAntenna in _antennas.Where(a => a.Value == antenna.Value && a != antenna && !checkedAntennas.Contains(a)))
                {
                    antinodes.Add(antenna);
                    antinodes.Add(secondAntenna);

                    var difference = (secondAntenna.Row - antenna.Row, secondAntenna.Column - antenna.Column);

                    var multiplier = 1;
                    while (true)
                    {
                        var antinode1 = new Location(antenna.Row - (difference.Item1 * multiplier), antenna.Column - (difference.Item2 * multiplier), '#');

                        if (antinode1.Row > -1 && antinode1.Column > -1 && antinode1.Column < Inputs[0].Count() && !antinodes.Contains(antinode1))
                            antinodes.Add(antinode1);
                        else
                            break;

                        multiplier++;
                    }

                    multiplier = 1;
                    while (true)
                    {
                        var antinode2 = new Location(secondAntenna.Row + (difference.Item1 * multiplier), secondAntenna.Column + (difference.Item2 * multiplier), '#');

                        if (antinode2.Row < Inputs.Count() && antinode2.Column > -1 && antinode2.Column < Inputs[0].Count() && !antinodes.Contains(antinode2))
                            antinodes.Add(antinode2);
                        else
                            break;

                        multiplier++;
                    }
                }

                checkedAntennas.Add(antenna);
            }

            return antinodes.Select(a => (a.Row, a.Column)).Distinct().Count().ToString();
        }
    }
}