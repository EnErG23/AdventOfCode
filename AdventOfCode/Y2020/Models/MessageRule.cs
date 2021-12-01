using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Y2020.Models
{
    public class MessageRule
    {
        public string ID { get; set; }
        public List<List<string>> SubRules { get; set; }
        public string Match{ get; set; }
    }
}
