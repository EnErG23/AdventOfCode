using AdventOfCode.Models;

namespace AdventOfCode.Y2020.Days
{
    public class Day24 : Day
    {
        private List<List<bool>>? tiles = new List<List<bool>>();

        public Day24(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
        {
            long result = 0;

            List<bool> startRow = new List<bool>();
            tiles.Add(startRow);

            bool startTile = false;
            startRow.Add(startTile);

            foreach (var input in Inputs)
            {
                var y = 0;
                var x = 0;

                var prevDir = 'a';
                var shifted = false;

                foreach (var dir in input)
                {
                    switch (dir)
                    {
                        case 'n':
                            y--;
                            shifted = !shifted;
                            break;
                        case 'e':
                            if (shifted || prevDir == 'e' || prevDir == 'w' || prevDir == 'a')
                                x++;
                            break;
                        case 's':
                            y++;
                            shifted = !shifted;
                            break;
                        default:
                            if (!shifted || prevDir == 'e' || prevDir == 'w' || prevDir == 'a')
                                x--;
                            break;
                    }

                    prevDir = dir;
                }

                var startTileY = ((tiles.Count - 1) / 2);
                var startTileX = ((tiles.ElementAt(startTileY).Count - 1) / 2);

                // Enlarge grid height if necessary
                if (Math.Abs(y) > startTileY)
                {
                    var newTiles = new List<List<bool>>();
                    var newRow = new List<bool>();

                    for (int i = 0; i < tiles.ElementAt(0).Count; i++)
                    {
                        newRow.Add(false);
                    }

                    for (int i = 0; i < (Math.Abs(y) - startTileY); i++)
                    {
                        newTiles.Add(newRow.ToList());
                    }
                    newTiles.AddRange(tiles);
                    for (int i = 0; i < (Math.Abs(y) - startTileY); i++)
                    {
                        newTiles.Add(newRow.ToList());
                    }

                    tiles = newTiles;
                }

                // Enlarge grid width if necessary
                if (Math.Abs(x) > startTileX)
                {
                    var newTiles = new List<List<bool>>();

                    foreach (var row in tiles)
                    {
                        var newRow = new List<bool>();

                        for (int i = 0; i < (Math.Abs(x) - startTileX); i++)
                        {
                            newRow.Add(false);
                        }

                        newRow.AddRange(row);

                        for (int i = 0; i < (Math.Abs(x) - startTileX); i++)
                        {
                            newRow.Add(false);
                        }

                        newTiles.Add(newRow);
                    }

                    tiles = newTiles;
                }

                // Calculate tile coords
                startTileY = ((tiles.Count - 1) / 2);
                startTileX = ((tiles.ElementAt(startTileY).Count - 1) / 2);
                var destY = startTileY + y;
                var destX = startTileX + x;

                // Flip tile
                tiles[destY][destX] = !tiles[destY][destX];
            }

            //WriteTiles(tiles);
            //Console.WriteLine();

            foreach (var row in tiles)
                foreach (var tile in row)
                    if (tile)
                        result++;

			return result.ToString();
        }

        public override string RunPart2()
        {
            long result = 0;

            for (int i = 0; i < 100; i++)
            {
                // Any black tile with zero or more than 2 black tiles immediately adjacent to it is flipped to white.
                // Any white tile with exactly 2 black tiles immediately adjacent to it is flipped to black.
            }

			return result.ToString();
        }

        static void WriteTiles(List<List<bool>> tiles)
        {
            foreach (var row in tiles)
            {
                var line = "";

                foreach (var tile in row)
                {
                    if (tile)
                        line += "B";
                    else
                        line += "W";
                }

                Console.WriteLine(line);
            }
        }
    }
}