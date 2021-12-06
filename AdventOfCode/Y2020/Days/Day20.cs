using AdventOfCode.Helpers;
using System.Diagnostics;
using AdventOfCode.Y2020.Models;

namespace AdventOfCode.Y2020.Days
{
    public static class Day20
    {
        static int day = 20;
        static List<string>? inputs;
        public static List<Tile> tiles = new List<Tile>();
        public static char[,] image;

        public static string? Answer1 { get; set; }
        public static string? Answer2 { get; set; }

        public static void Run(int part, bool test)
        {
            Stopwatch sw = Stopwatch.StartNew();

            inputs = InputManager.GetInputAsStrings(day, test);

            string part1 = "";
            string part2 = "";

            if (part == 1)
                part1 = Part1();
            else if (part == 2)
                part2 = Part2();
            else
            {
                part1 = Part1();
                part2 = Part2();
            }

            sw.Stop();
			var ms = sw.Elapsed.TotalMilliseconds;

            Console.WriteLine($"Day {day} ({ms}ms):");
            if (part1 != "") Console.WriteLine($"    {part1}");
            if (part2 != "") Console.WriteLine($"    {part2}");
        }

        static string Part1()
        {
            Stopwatch sw = Stopwatch.StartNew();

            long result = 0;

            #region Solution

            CreateTiles();

            FindCornerTiles();

            result = tiles.Where(t => t.IsCorner).Select(t => t.ID).Aggregate(1, (long x, long y) => x * y);

            #endregion

            sw.Stop();
			var ms = sw.Elapsed.TotalMilliseconds;

            if (result > 0) Answer1 = result.ToString();
            return $"Part 1 ({ms}ms): {result} ";
        }

        static string Part2()
        {
            Stopwatch sw = Stopwatch.StartNew();

            long result = 0;

            #region Solution

            BuildImage();

            FindMonsters();

            var n = Math.Sqrt(image.Length);

            for (int x = 0; x < n; x++)
            {
                for (int y = 0; y < n; y++)
                {
                    result += image[x, y] == '#' ? 1 : 0;
                }
            }

            #endregion

            sw.Stop();
			var ms = sw.Elapsed.TotalMilliseconds;

            if (result > 0) Answer2 = result.ToString();
            return $"Part 2 ({ms}ms): {result} ";
        }

        //PART 1
        static void CreateTiles()
        {
            Tile tile = new Tile { Pixels = new char[10, 10], Edges = new List<Edge>(), IsCorner = false };
            Edge leftEdge = new Edge { Pixels = "", HasMatch = false };
            Edge rightEdge = new Edge { Pixels = "", HasMatch = false };

            var i = 0;

            foreach (var input in inputs)
            {
                if (input.Contains(":"))
                {
                    tile = new Tile { Pixels = new char[10, 10], Edges = new List<Edge>(), IsCorner = false };
                    leftEdge = new Edge { Pixels = "", HasMatch = false };
                    rightEdge = new Edge { Pixels = "", HasMatch = false };

                    i = 0;

                    tile.ID = Convert.ToInt32(input.Replace("Tile ", "").Replace(":", ""));
                }
                else if (input == "")
                {
                    tile.Edges.Add(leftEdge);
                    tile.Edges.Add(rightEdge);

                    tiles.Add(tile);
                }
                else
                {
                    var pixels = input.ToCharArray();

                    for (int p = 0; p < pixels.Length; p++)
                    {
                        tile.Pixels[i, p] = pixels[p];
                    }

                    if (i == 0 || i == input.Length - 1)
                        tile.Edges.Add(new Edge { Pixels = input, HasMatch = false });

                    leftEdge.Pixels += input.Substring(0, 1);
                    rightEdge.Pixels += input.Substring(input.Length - 1);

                    i++;
                }
            }

            tile.Edges.Add(leftEdge);
            tile.Edges.Add(rightEdge);
            tiles.Add(tile);
        }

        static void FindCornerTiles()
        {
            foreach (var tile in tiles)
            {
                int matchingEdges = 0;

                foreach (var edge in tile.Edges)
                {
                    foreach (var checkTile in tiles.Where(t => t.ID != tile.ID))
                    {
                        foreach (var checkEdge in checkTile.Edges)
                        {
                            if (edge.Pixels == checkEdge.Pixels || Reverse(edge.Pixels) == checkEdge.Pixels)
                            {
                                edge.HasMatch = true;
                                matchingEdges++;
                                break;
                            }
                        }
                        if (matchingEdges > 2) break;
                    }
                    if (matchingEdges > 2) break;
                }

                if (matchingEdges < 3)
                {
                    tile.IsCorner = true;
                }

                if (tiles.Count(t => t.IsCorner) > 3) break;
            }
        }

        //PART 2
        static void BuildImage()
        {
            var firstTile = tiles.First(t => t.IsCorner);

            var n = Convert.ToInt32(Math.Sqrt(tiles.Count()));
            Tile[,] imageTiles = new Tile[n, n];

            if (firstTile.Edges[0].HasMatch)
                if (firstTile.Edges[2].HasMatch)
                    firstTile = RotateTile(firstTile, 2);
                else
                    firstTile = RotateTile(firstTile, 1);
            else if (firstTile.Edges[2].HasMatch)
                firstTile = RotateTile(firstTile, 3);

            imageTiles[0, 0] = firstTile;
            tiles.Remove(firstTile);

            for (int y = 1; y < n; y++)
            {
                var previousTile = imageTiles[0, y - 1];
                var previousEdge = "";

                for (int c = 0; c < 10; c++)
                {
                    previousEdge += previousTile.Pixels[c, 9];
                }

                var toRemove = new Tile();

                foreach (var tile in tiles)
                {
                    for (int i = 0; i < tile.Edges.Count(); i++)
                    {
                        if (previousEdge == tile.Edges[i].Pixels)
                        {
                            switch (i)
                            {
                                case 1:
                                    imageTiles[0, y] = RotateTile(tile, 1);
                                    break;
                                case 2:
                                    imageTiles[0, y] = tile;
                                    break;
                                case 3:
                                    imageTiles[0, y] = FlipTile(tile);
                                    break;
                                default:
                                    imageTiles[0, y] = RotateTile(FlipTile(tile), 3);
                                    break;
                            }
                            toRemove = tile;
                            break;
                        }
                        else if (previousEdge == Reverse(tile.Edges[i].Pixels))
                        {
                            switch (i)
                            {
                                case 1:
                                    imageTiles[0, y] = RotateTile(FlipTile(tile), 1);
                                    break;
                                case 2:
                                    imageTiles[0, y] = RotateTile(FlipTile(tile), 2);
                                    break;
                                case 3:
                                    imageTiles[0, y] = RotateTile(tile, 2);
                                    break;
                                default:
                                    imageTiles[0, y] = RotateTile(tile, 3);
                                    break;
                            }
                            toRemove = tile;
                            break;
                        }
                    }
                    if (toRemove.Pixels != null) break;
                }

                tiles.Remove(toRemove);
            }

            for (int x = 1; x < n; x++)
            {
                for (int y = 0; y < n; y++)
                {
                    var previousTile = imageTiles[x - 1, y];
                    var previousEdge = "";

                    for (int c = 0; c < 10; c++)
                    {
                        previousEdge += previousTile.Pixels[9, c];
                    }

                    var toRemove = new Tile();

                    foreach (var tile in tiles)
                    {
                        for (int i = 0; i < tile.Edges.Count(); i++)
                        {
                            if (previousEdge == tile.Edges[i].Pixels)
                            {
                                switch (i)
                                {
                                    case 1:
                                        imageTiles[x, y] = RotateTile(FlipTile(tile), 2);
                                        break;
                                    case 2:
                                        imageTiles[x, y] = RotateTile(FlipTile(tile), 3);
                                        break;
                                    case 3:
                                        imageTiles[x, y] = RotateTile(tile, 3);
                                        break;
                                    default:
                                        imageTiles[x, y] = tile;
                                        break;
                                }
                                toRemove = tile;
                                break;
                            }
                            else if (previousEdge == Reverse(tile.Edges[i].Pixels))
                            {
                                switch (i)
                                {
                                    case 1:
                                        imageTiles[x, y] = RotateTile(tile, 2);
                                        break;
                                    case 2:
                                        imageTiles[x, y] = RotateTile(tile, 1);
                                        break;
                                    case 3:
                                        imageTiles[x, y] = RotateTile(FlipTile(tile), 1);
                                        break;
                                    default:
                                        imageTiles[x, y] = FlipTile(tile);
                                        break;
                                }
                                toRemove = tile;
                                break;
                            }
                        }
                        if (toRemove.Pixels != null) break;
                    }

                    tiles.Remove(toRemove);
                }
            }

            // TRIM TILES
            for (int x = 0; x < n; x++)
            {
                for (int y = 0; y < n; y++)
                {
                    imageTiles[x, y] = TrimTile(imageTiles[x, y]);
                }
            }

            // CREATE SINGLE IMAGE
            image = new char[n * 8, n * 8];
            for (int x = 0; x < n; x++)
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int y = 0; y < n; y++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            image[x * 8 + i, y * 8 + j] = imageTiles[x, y] != null ? imageTiles[x, y].Pixels[i, j] : ' ';
                        }
                    }
                }
            }
        }

        static Tile FlipTile(Tile tile)
        {
            tile.Pixels = FlipMatrix(tile.Pixels);

            return tile;
        }

        static char[,] FlipMatrix(char[,] matrix)
        {
            var n = Convert.ToInt32(Math.Sqrt(matrix.Length));

            char[,] result = new char[n, n];

            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    result[i, j] = matrix[i, n - 1 - j];
                }
            }

            return result;
        }

        static Tile RotateTile(Tile tile, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                tile.Pixels = RotateMatrix(tile.Pixels);
            }

            return tile;
        }

        static char[,] RotateMatrix(char[,] matrix)
        {
            var n = Convert.ToInt32(Math.Sqrt(matrix.Length));

            char[,] result = new char[n, n];

            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    result[i, j] = matrix[n - 1 - j, i];
                }
            }

            return result;
        }

        static Tile TrimTile(Tile tile)
        {
            char[,] newPixels = new char[8, 8];

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    newPixels[i, j] = tile.Pixels[i + 1, j + 1];
                }
            }

            tile.Pixels = newPixels;

            return tile;
        }

        static void FindMonsters()
        {
            var monstersFound = 0;

            var attempt = 0;
            var n = Math.Sqrt(image.Length);

            while (true)
            {
                for (int x = 0; x < n - 2; x++)
                {
                    for (int y = 18; y < n - 1; y++)
                    {
                        if (image[x, y] == '#')
                        {
                            monstersFound += CheckForMonster(x, y) ? 1 : 0;
                        }
                    }
                }

                if (monstersFound > 0) break;

                attempt++;
                image = RotateMatrix(image);
                if (attempt == 4)
                    image = FlipMatrix(image);
            }
        }

        static bool CheckForMonster(int x, int y)
        {
            var result = true;

            var monsterPattern = new List<List<int>> { new List<int> { 18 }, new List<int> { 0, 5, 6, 11, 12, 17, 18, 19 }, new List<int> { 1, 4, 7, 10, 13, 16 } };

            for (int i = 1; i < 3; i++)
                for (int j = 0; j < monsterPattern[i].Count(); j++)
                    if (image[x + i, y - 18 + monsterPattern[i][j]] != '#') return false;

            // MONSTER FOUND

            for (int i = 0; i < 3; i++)
                for (int j = 0; j < monsterPattern[i].Count(); j++)
                    image[x + i, y - 18 + monsterPattern[i][j]] = '0';

            return result;
        }

        // AUX
        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        // Visualize
        public static void Visualize(int part, bool test)
        {
            Run(0, test);

            inputs = InputManager.GetInputAsStrings(day, test);

            Console.Clear();

            if (part == 1)
                VisualizePart1();
            else if (part == 2)
                VisualizePart2();
            else
            {
                VisualizePart1();
                VisualizePart2();
            }

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Cyan;
        }

        static void VisualizePart1()
        {
        }

        static void VisualizePart2()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            for (int i = 3; i > 0; i--)
            {
                Console.Clear();
                Console.WriteLine($"Visualization for 2020.20.2");
                Console.WriteLine($"Starting in {i}");
                Thread.Sleep(1000);
            }

            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.DarkBlue;

            VisualizeImage();
        }

        static void VisualizeImage()
        {
            var n = Convert.ToInt32(Math.Sqrt(image.Length));

            for (int x = 0; x < n; x++)
            {
                Console.BackgroundColor = ConsoleColor.Blue;

                for (int y = 0; y < n; y++)
                {
                    if (image[x, y] == '0')
                        Console.ForegroundColor = ConsoleColor.White;
                    else
                        Console.ForegroundColor = ConsoleColor.DarkBlue;

                    Console.Write(image[x, y]);
                }

                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine();
                Thread.Sleep(100);
            }
        }
    }
}