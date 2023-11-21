using AdventOfCode.Models;

namespace AdventOfCode.Y2022.Days
{
    public class Day19 : Day
    {
        private readonly List<Blueprint> _blueprints;

        public Day19(int year, int day, bool test) : base(year, day, test)
        {
            _blueprints = new();

            foreach (string input in Inputs)
            {
                var words = input.Split(" ");

                List<Robot> robots = new() { new("ore", 0, 0, 0), };

                List<Robot> robotShop = new()
                {
                    new("ore", int.Parse(words[6]), 0, 0),
                    new("clay", int.Parse(words[12]), 0, 0),
                    new("obsidian", int.Parse(words[18]), int.Parse(words[21]), 0),
                    new("geode", int.Parse(words[27]), 0, int.Parse(words[30]))
                };

                _blueprints.Add(new(int.Parse(words[1].Replace(":", "")), robots, robotShop));
            }
        }

        public override string RunPart1()
        {
            foreach (Blueprint blueprint in _blueprints)
            {
                Console.WriteLine();
                Console.WriteLine($"== {blueprint.Id} ==");
                Console.WriteLine();

                foreach (Robot robot in blueprint.Robots)
                    Console.WriteLine($"{robot.Type} ({robot.OreCost},{robot.ClayCost},{robot.ObsidianCost})");

                Console.WriteLine();
                Console.WriteLine($"--------------------");
            }
            Console.WriteLine();

            return "";

            int minutes = 24;

            foreach (Blueprint blueprint in _blueprints)
                blueprint.MineGeodes(minutes);

            return _blueprints.Sum(b => b.QualityLevel).ToString();
        }

        public override string RunPart2()
        {
            return "undefined";
        }
    }

    public class Blueprint
    {
        public int Id;
        public List<Robot> Robots;
        private List<Robot> RobotShop;
        private int Ores = 0;
        private int Clay = 0;
        private int Obsidian = 0;
        private int Geodes = 0;

        public Blueprint(int id, List<Robot> robots, List<Robot> robotShop)
        {
            Id = id;
            Robots = robots;
            RobotShop = robotShop;
        }

        public int QualityLevel
        {
            get
            {
                return Id * QualityLevel;
            }
        }

        public void MineGeodes(int minutes)
        {
            Geodes += minutes;
        }
    }

    public class Robot
    {
        public string Type { get; set; }
        public int OreCost { get; set; }
        public int ClayCost { get; set; }
        public int ObsidianCost { get; set; }

        public Robot(string type, int oreCost, int clayCost, int obsidianCost)
        {
            Type = type;
            OreCost = oreCost;
            ClayCost = clayCost;
            ObsidianCost = obsidianCost;
        }
    }
}