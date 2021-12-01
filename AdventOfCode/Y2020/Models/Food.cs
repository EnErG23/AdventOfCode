using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Y2020.Models
{
    public class Food
    {
        public List<string> Ingredients { get; set; }
        public List<string> Allergens { get; set; }
    }

    public class Ingredient
    {
        public string Name { get; set; }
        public List<string> Allergens { get; set; }
    }
}
