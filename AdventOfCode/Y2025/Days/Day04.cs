using AdventOfCode.Models;

namespace AdventOfCode.Y2025.Days
{
    public class Day04 : Day
    {
        private readonly List<Location> _locations;
        private readonly List<(int Row, int Column)> _adjecentLocations = new()
        {
            (-1,-1),
            (-1,0),
            (-1,1),
            (0,-1),
            (0,1),
            (1,-1),
            (1,0),
            (1,1),
        };

        public Day04(int year, int day, bool test) : base(year, day, test)
        {
            _locations = new();

            for (int row = 0; row < Inputs.Count; row++)
                for (int column = 0; column < Inputs[row].Length; column++)
                    if (Inputs[row][column] == '@')
                        _locations.Add(new Location(row, column, '@'));
        }

        public override string RunPart1()
            => RemoveRolls(_locations.ToList()).ToString();

        public override string RunPart2()
        {
            long result = 0;
            var locations = _locations.ToList();

            while (true)
            {
                var removedRolls = RemoveRolls(locations);

                if (removedRolls == 0)
                    break;

                result += removedRolls;
            }

            return result.ToString();
        }

        private long RemoveRolls(List<Location> locations)
        {
            var removableRolls = new List<Location>();

            foreach (var location in locations)
            {
                var rolls = 0;

                foreach (var adjecentLocation in _adjecentLocations)
                {
                    if (locations.Exists(l =>
                        l.Row == location.Row + adjecentLocation.Row &&
                        l.Column == location.Column + adjecentLocation.Column))
                    {
                        rolls++;
                    }
                }

                if (rolls < 4)
                    removableRolls.Add(location);
            }

            locations.RemoveAll(l => removableRolls.Contains(l));

            return removableRolls.Count;
        }
    }
}