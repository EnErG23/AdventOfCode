using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Y2021.Models
{
    public class Cave
    {
        public string Name { get; set; }
        public bool IsBig { get; set; }
        public List<Cave> ConnectedCaves { get; set; }

        public Cave(string name)
        {
            Name = name;
            IsBig = char.IsUpper(name[0]);
            ConnectedCaves = new();
        }
    }
}
