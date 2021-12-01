using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Y2020.Models
{
    public class Tile
    {
        public long ID { get; set; }
        public char[,] Pixels { get; set; }
        public List<Edge> Edges { get; set; }
        public bool IsCorner { get; set; }
    }

    public class Edge
    {
        public string Position { get; set; }
        public string Pixels { get; set; }
        public bool HasMatch { get; set; }
    }
}
