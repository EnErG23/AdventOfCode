using AdventOfCode.Models;

namespace AdventOfCode.Y2022.Days
{
    public class Day17 : Day
    {
        private List<char> _jetPushes;
        private int _jetCount;

        public Day17(int year, int day, bool test) : base(year, day, test)
        {
            _jetPushes = Inputs[0].ToCharArray().ToList();
            _jetCount = -1;
        }

        public override string RunPart1()
        {
            int chamberWidth = 7;
            List<Shape> shapes = new();

            int shapeType = 0;

            for (int i = 0; i < 2022; i++)
            {
                shapeType += shapeType + 1 > 5 ? -4 : 1;

                DropNewShape(chamberWidth, shapes, shapeType);
            }

            return (Math.Abs(shapes.Min(s => s.Locations.Min(l => l.Row))) + 1).ToString();
        }

        public override string RunPart2()
        {
            return "undefined";
        }

        public void DropNewShape(int chamberWidth, List<Shape> shapes, int shapeType)
        {
            int height = shapes.Any() ? shapes.Min(s => s.Locations.Min(l => l.Row)) : 1;

            (int, int) spawnPoint = (height - 4, 2);
            Shape shape = new Shape(shapeType, spawnPoint);

            while (true)
            {
                //PrintShapes(shapes, chamberWidth);

                _jetCount = _jetCount == _jetPushes.Count() - 1 ? 0 : _jetCount + 1;

                //Console.WriteLine($"{_jetPushes[_jetCount]} & v");

                if (_jetPushes[_jetCount] == '<' && shape.CanMoveLeft(shapes))
                    shape.MoveLeft();
                else if (_jetPushes[_jetCount] == '>' && shape.CanMoveRight(shapes, chamberWidth))
                    shape.MoveRight();

                if (!shape.CanMoveDown(shapes))
                    break;

                shape.MoveDown();
            }

            shapes.Add(shape);

            //PrintShapes(shapes, chamberWidth);
            //Console.WriteLine("-----------------------------");
            //Console.ReadLine();
        }

        public void PrintShapes(List<Shape> shapes, int chamberWidth)
        {
            //int count = 0;

            //foreach (var shape in shapes)
            //{
            //    Console.WriteLine($"== Shape {++count} ==");

            //    foreach (var location in shape.Locations)
            //        Console.WriteLine($"({location.Row}, {location.Column})");

            //    Console.WriteLine();
            //}

            //Console.WriteLine(shapes.Min(s => s.Locations.Min(l => l.Row)));            

            for (int i = shapes.Min(s => s.Locations.Min(l => l.Row)); i < 1; i++)
            {
                Console.Write($"{Math.Abs(i)}: ");

                for (int j = 0; j < chamberWidth; j++)
                {
                    if (shapes.Any(s => s.Locations.Any(l => (l.Row, l.Column) == (i, j))))
                        Console.Write("#");
                    else
                        Console.Write(".");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }

    public class Shape
    {
        public List<Location> Locations { get; set; }

        public Shape(int type, (int, int) origin)
        {
            // origin is left bottom of shape;

            List<(int, int)> coords = new();

            switch (type)
            {
                // ####
                case 1:
                    coords = new() { (origin.Item1, origin.Item2), (origin.Item1, origin.Item2 + 1), (origin.Item1, origin.Item2 + 2), (origin.Item1, origin.Item2 + 3) };
                    break;
                // .#.
                // ###
                // .#.
                case 2:
                    coords = new() { (origin.Item1, origin.Item2 + 1), (origin.Item1 - 1, origin.Item2), (origin.Item1 - 1, origin.Item2 + 1), (origin.Item1 - 1, origin.Item2 + 2), (origin.Item1 - 2, origin.Item2 + 1) };
                    break;
                // ..#
                // ..#
                // ###
                case 3:
                    coords = new() { (origin.Item1, origin.Item2), (origin.Item1, origin.Item2 + 1), (origin.Item1, origin.Item2 + 2), (origin.Item1 - 1, origin.Item2 + 2), (origin.Item1 - 2, origin.Item2 + 2) };
                    break;
                // #
                // #
                // #
                // #
                case 4:
                    coords = new() { (origin.Item1, origin.Item2), (origin.Item1 - 1, origin.Item2), (origin.Item1 - 2, origin.Item2), (origin.Item1 - 3, origin.Item2) };
                    break;
                // ##
                // ##
                case 5:
                    coords = new() { (origin.Item1, origin.Item2), (origin.Item1, origin.Item2 + 1), (origin.Item1 - 1, origin.Item2), (origin.Item1 - 1, origin.Item2 + 1) };
                    break;
            }

            Locations = coords.Select(c => new Location(c.Item1, c.Item2, '#')).ToList();
        }

        public bool CanMoveLeft(List<Shape> shapes)
        {
            if (Locations.Min(l => l.Column) == 0)
                return false;

            var moveLeftLocations = Locations.Select(l => (l.Row, l.Column - 1)).ToList();

            foreach (var shape in shapes.OrderBy(s => s.Locations.Min(l => l.Row)))
                foreach (var locations in shape.Locations)
                    foreach (var loc in moveLeftLocations)
                        if (locations.Row == loc.Item1 && locations.Column == loc.Item2)
                            return false;

            return true;
        }

        public bool CanMoveRight(List<Shape> shapes, int chamberWidth)
        {
            if (Locations.Max(l => l.Column) == chamberWidth - 1)
                return false;

            var moveRightLocations = Locations.Select(l => (l.Row, l.Column + 1)).ToList();

            foreach (var shape in shapes.OrderBy(s => s.Locations.Min(l => l.Row)))
                foreach (var locations in shape.Locations)
                    foreach (var loc in moveRightLocations)
                        if (locations.Row == loc.Item1 && locations.Column == loc.Item2)
                            return false;

            return true;
        }

        public bool CanMoveDown(List<Shape> shapes)
        {
            if (Locations.Max(l => l.Row) > -1)
                return false;

            var moveDownLocations = Locations.Select(l => (l.Row + 1, l.Column)).ToList();

            foreach (var shape in shapes.OrderBy(s => s.Locations.Min(l => l.Row)))
                foreach (var locations in shape.Locations)
                    foreach (var loc in moveDownLocations)
                        if (locations.Row == loc.Item1 && locations.Column == loc.Item2)
                            return false;

            return true;
        }

        public void MoveLeft() => Locations.ForEach(l => l.Column--);
        public void MoveRight() => Locations.ForEach(l => l.Column++);
        public void MoveDown() => Locations.ForEach(l => l.Row++);
        public void MoveUp() => Locations.ForEach(l => l.Row--);
    }
}