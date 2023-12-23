using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Models
{
    public class Location
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public char Value { get; set; }

        public Location(int row, int column, char value)
        {
            Row = row;
            Column = column;
            Value = value;
        }

        public override string ToString() => $"({Row}, {Column})";
    }
}
