using AdventOfCode.Models;
using System.Diagnostics;
using AdventOfCode.Y2020.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Y2020.Days
{
    public class Day21 : Day
    {
        public static List<Food> foods = new List<Food>();
        public static List<Ingredient> ingredients = new List<Ingredient>();

        public Day21(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
        {
            long result = 0;

            foreach (var input in Inputs)
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

			return result.ToString();
        }

        public override string RunPart2()
        {            
            return string.Join(",", ingredients.Where(i => i.Allergens.Count() > 0).OrderBy(i => i.Allergens.First()).Select(i => i.Name));
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