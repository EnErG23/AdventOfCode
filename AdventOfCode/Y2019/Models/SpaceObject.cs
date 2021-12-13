namespace AdventOfCode.Y2019.Models
{
    public class SpaceObject
    {
        public string? Name { get; set; }
        public SpaceObject? OrbitsAround { get; set; }
    }
}