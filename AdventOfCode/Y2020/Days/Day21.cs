using AdventOfCode.Helpers;
using System.Diagnostics;
using AdventOfCode.Y2020.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Y2020.Days
{
    public static class Day21
    {
        static readonly int day = 21;
        static List<string>? inputs;
        public static List<Food> foods = new List<Food>();
        public static List<Ingredient> ingredients = new List<Ingredient>();

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

        private static string Part1()
        {
            Stopwatch sw = Stopwatch.StartNew();

            long result = 0;

            #region Solution

            foreach (var input in inputs)
            {
                var ingredients = input.Substring(0, input.IndexOf("(") - 1).Split(' ').ToList();
                var allergens = input.Substring(input.IndexOf("(") + 1).Replace("contains ", "").Replace(",", "").Replace(")", "").Split(' ').ToList();

                Food food = new Food
                {
                    Ingredients = ingredients,
                    Allergens = allergens
                };

                foods.Add(food);
            }

            foreach (var food in foods)
            {
                foreach (var ingredient in food.Ingredients)
                {
                    if (ingredients.Count(a => a.Name == ingredient) > 0)
                    {
                        var existingIngredient = ingredients.Find(a => a.Name == ingredient);
                        foreach (var allergen in food.Allergens)
                            if (!existingIngredient.Allergens.Contains(allergen))
                                existingIngredient.Allergens.Add(allergen);
                    }
                    else
                        ingredients.Add(new Ingredient { Name = ingredient, Allergens = food.Allergens.ToList() });
                }
            }

            FindAllergens();

            result = foods.Select(f => f.Ingredients.Where(i => ingredients.Where(ig => ig.Allergens.Count() == 0).Select(ig => ig.Name).ToList().Contains(i))).Sum(i => i.Count());

            #endregion

            sw.Stop();
			var ms = sw.Elapsed.TotalMilliseconds;

            if (result > 0) Answer1 = result.ToString();
            return $"Part 1 ({ms}ms): {result} ";
        }

        private static string Part2()
        {
            string result = "";

            Stopwatch sw = Stopwatch.StartNew();

            #region Solution

            result = string.Join(",", ingredients.Where(i => i.Allergens.Count() > 0).OrderBy(i => i.Allergens.First()).Select(i => i.Name));

            #endregion

            sw.Stop();
			var ms = sw.Elapsed.TotalMilliseconds;

            if (result != "") Answer2 = result.ToString();
            return $"Part 2 ({ms}ms): {result} ";
        }

        static void FindAllergens()
        {
            foreach (var ingredient in ingredients)
            {
                var toRemoveAllergens = new List<string>();

                foreach (var allergen in ingredient.Allergens)
                {
                    foreach (var food in foods.Where(f => f.Allergens.Contains(allergen)))
                    {
                        if (!food.Ingredients.Contains(ingredient.Name))
                        {
                            toRemoveAllergens.Add(allergen);
                            break;
                        }
                    }
                }

                foreach (var toRemoveAllergen in toRemoveAllergens)
                {
                    ingredient.Allergens.Remove(toRemoveAllergen);
                }
            }

            while (ingredients.Where(i => i.Allergens.Count() > 1).Count() > 0)
                foreach (var allergen in ingredients.Where(i => i.Allergens.Count() == 1).Select(i => i.Allergens.First()))
                    foreach (var toChangeIngredient in ingredients.Where(i => i.Allergens.Count() > 1))
                        toChangeIngredient.Allergens.Remove(allergen);
        }
    }
}