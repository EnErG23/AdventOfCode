using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Y2021.Models
{
    public class Board
    {
        public List<List<Number>> Rows { get; set; } = new List<List<Number>>();
        public bool IsWinner
        {
            get
            {
                // Check rows
                foreach (var r in Rows)
                    if (r.Where(x => x.Marked == false).Count() == 0)
                        return true;

                // Check cols
                for (int i = 0; i < Rows[0].Count; i++)
                    if (Rows.Select(r => r[i]).Where(x => x.Marked == false).Count() == 0)
                        return true;

                return false;
            }
        }
        public long UnmarkedSum
        {
            get
            {
                return Rows.Where(r => r.Any(n => !n.Marked)).Sum(r => r.Where(n => !n.Marked).Sum(n => n.Value));
            }
        }
    }

    public class Number
    {
        public int Value { get; set; } = 0;
        public bool Marked { get; set; } = false;
    }
}
