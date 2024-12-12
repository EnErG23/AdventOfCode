using AdventOfCode.Models;

namespace AdventOfCode.Y2024.Days
{
    public class Day12 : Day
    {
        private List<Location> _plants = new();
        private List<Region> _regions = new();
        private List<Location> _visited = new();

        public Day12(int year, int day, bool test) : base(year, day, test)
        {
            for (int r = 0; r < Inputs.Count; r++)
                for (int c = 0; c < Inputs[r].Count(); c++)
                    _plants.Add(new Location(r, c, Inputs[r][c]));
        }

        public override string RunPart1()
        {
            foreach (var plant in _plants)
                if (!_regions.SelectMany(r => r.Plants).ToList().Exists(v => v.Row == plant.Row && v.Column == plant.Column))
                {
                    _visited = new();
                    var region = new Region();
                    Flood(plant, region);
                    _regions.Add(region);
                }

            return _regions.Sum(r => r.Area * r.Perimeter).ToString();
        }

        public override string RunPart2()
        {
            if (_regions.Count == 0)
                RunPart1();

            decimal result = 0;

            foreach (var region in _regions)
            {
                decimal tempFences = 4;

                //top
                Location prevFence = region.Fences.Where(f => f.Value == '0').OrderBy(f => f.Row).ThenBy(f => f.Column).First();
                foreach (var fence in region.Fences.Where(f => f.Value == '0').OrderBy(f => f.Row).ThenBy(f => f.Column))
                {
                    if (fence.Column > prevFence.Column + 1 || fence.Row != prevFence.Row)
                        tempFences++;

                    prevFence = fence;
                }

                //down
                prevFence = region.Fences.Where(f => f.Value == '2').OrderBy(f => f.Row).ThenBy(f => f.Column).First();
                foreach (var fence in region.Fences.Where(f => f.Value == '2').OrderBy(f => f.Row).ThenBy(f => f.Column))
                {
                    if (fence.Column > prevFence.Column + 1 || fence.Row != prevFence.Row)
                        tempFences++;

                    prevFence = fence;
                }

                //right
                prevFence = region.Fences.Where(f => f.Value == '1').OrderBy(f => f.Column).ThenBy(f => f.Row).First();
                foreach (var fence in region.Fences.Where(f => f.Value == '1').OrderBy(f => f.Column).ThenBy(f => f.Row))
                {
                    if (fence.Row > prevFence.Row + 1 || fence.Column != prevFence.Column)
                        tempFences++;

                    prevFence = fence;
                }

                //left
                prevFence = region.Fences.Where(f => f.Value == '3').OrderBy(f => f.Column).ThenBy(f => f.Row).First();
                foreach (var fence in region.Fences.Where(f => f.Value == '3').OrderBy(f => f.Column).ThenBy(f => f.Row))
                {
                    if (fence.Row > prevFence.Row + 1 || fence.Column != prevFence.Column)
                        tempFences++;

                    prevFence = fence;
                }

                result += region.Area * tempFences;
            }

            return result.ToString();
        }

        private void Flood(Location plant, Region region)
        {
            region.Plants.Add(plant);
            _visited.Add(plant);

            if (plant.Value == region.Plants.First().Value)
                region.Area++;

            List<(int, int)> directions = new() { (-1, 0), (0, 1), (1, 0), (0, -1) }; // 0 1 2 3

            int counter = 48;

            foreach (var direction in directions)
            {
                if (!_plants.Exists(p => p.Row == plant.Row + direction.Item1 && p.Column == plant.Column + direction.Item2 && p.Value == plant.Value))
                {
                    region.Fences.Add(new Location(plant.Row, plant.Column, (char)(counter)));
                    region.Perimeter++;
                }
                else if (!_visited.Exists(v => v.Row == plant.Row + direction.Item1 && v.Column == plant.Column + direction.Item2))
                    Flood(_plants.First(p => p.Row == plant.Row + direction.Item1 && p.Column == plant.Column + direction.Item2), region);

                counter++;
            }
        }
    }

    class Region
    {
        public List<Location> Plants = new();
        public decimal Area = 0;
        public List<Location> Fences = new();
        public decimal Perimeter = 0;

        public Region() { }
    }
}