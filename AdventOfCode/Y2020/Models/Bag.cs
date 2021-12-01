using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Y2020.Models
{
    public class Bag
    {
        public string Name { get; set; }
        public List<string> ChildBags { get; set; }
    }
}
