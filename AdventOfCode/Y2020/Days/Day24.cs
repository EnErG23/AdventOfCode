using AdventOfCode.Models;

namespace AdventOfCode.Y2020.Days
{
    public class Day24 : Day
    {
        private List<List<bool>>? tiles;

        public Day24(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
        {
            long result = 0;

            tiles = new();
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

                    for (int i = 0; i < tiles[0].Count; i++)
                        newRow.Add(false);

                    for (int i = 0; i < (Math.Abs(y) - startTileY); i++)
                        newTiles.Add(newRow.ToList());

                    newTiles.AddRange(tiles);

                    for (int i = 0; i < (Math.Abs(y) - startTileY); i++)
                        newTiles.Add(newRow.ToList());

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
                            newRow.Add(false);

                        newRow.AddRange(row);

                        for (int i = 0; i < (Math.Abs(x) - startTileX); i++)
                            newRow.Add(false);

                        newTiles.Add(newRow);
                    }

                    tiles = newTiles;
                }

                // Calculate tile coords
                startTileY = ((tiles.Count - 1) / 2);
                startTileX = ((tiles[startTileY].Count - 1) / 2);
                var destY = startTileY + y;
                var destX = startTileX + x;

                // Flip tile
                tiles[destY][destX] = !tiles[destY][destX];
            }

            foreach (var row in tiles)
                foreach (var tile in row)
                    if (tile)
                        result++;

            return result.ToString();
        }

        public override string RunPart2()
        {
            if (tiles is null)
                RunPart1();

            Console.Clear();
            WriteTiles(tiles);
            Console.WriteLine();

            long result = 0;

            for (int d = 0; d < 100; d++)
            {
                // Enlarge grid to also check neighbouring whites
                List<List<bool>> newTiles = new();
                List<bool> newRow = new();

                for (int i = 0; i < tiles[0].Count + 2; i++)
                    newRow.Add(false);

                newTiles.Add(newRow.ToList());

                for (int i = 0; i < tiles.Count; i++)
                {
                    newRow = new();

                    newRow.Add(false);
                    newRow.AddRange(tiles[i]);
                    newRow.Add(false);

                    newTiles.Add(newRow.ToList());
                }

                newRow = new();

                for (int i = 0; i < newTiles[0].Count; i++)
                    newRow.Add(false);

                newTiles.Add(newRow.ToList());

                tiles = newTiles.ToList();

                // Get tiles to flip
                List<(int, int)> tilesToFlip = new();

                for (int r = 0; r < tiles.Count; r++)
                {
                    for (int c = 0; c < tiles[0].Count; c++)
                    {
                        var adjacentBlack = 0;

                        // TODO: RE-WRITE FOR SHIFTED ROWS (hexagonals vs squares)
                        for (int i = -1; i <= 1; i++)
                        {
                            for (int j = -1; j <= 1; j++)
                                try
                                {
                                    if (tiles[r + i][c + j])
                                        adjacentBlack++;
                                }
                                catch { }
                        }

                        if (tiles[r][c])
                        {
                            adjacentBlack--;

                            if (adjacentBlack == 0 || adjacentBlack > 2)
                                tilesToFlip.Add((r, c));
                        }
                        else if (adjacentBlack == 2)
                            tilesToFlip.Add((r, c));
                    }
                }

                // Flip tiles
                foreach (var tileToFlip in tilesToFlip)
                    tiles[tileToFlip.Item1][tileToFlip.Item2] = !tiles[tileToFlip.Item1][tileToFlip.Item2];

                //Console.Clear();                

                var blackTiles = 0;

                foreach (var row in tiles)
                    foreach (var tile in row)
                        if (tile)
                            blackTiles++;

                Console.WriteLine($"Day {d + 1}: {blackTiles}");
                Console.WriteLine();
                WriteTiles(tiles);
                Console.WriteLine();
            }

            foreach (var row in tiles)
                foreach (var tile in row)
                    if (tile)
                        result++;

            return result.ToString();
        }

        static void WriteTiles(List<List<bool>> tiles)
        {
            Console.ForegroundColor = ConsoleColor.White;

            foreach (var row in tiles)
            {
                foreach (var tile in row)
                {
                    if (tile)
                        Console.Write(" ");
                    else
                        Console.Write("█");
                }
                Console.WriteLine();
            }
        }
    }
}