using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Y2020.Models
{
    public class Restriction
    {
        public string Name { get; set; }
        public List<int> PossiblePositions { get; set; }
        public int Min1 { get; set; }
        public int Max1 { get; set; }
        public int Min2 { get; set; }
        public int Max2 { get; set; }
    }
}
