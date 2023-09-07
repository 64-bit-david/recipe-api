using RecipeAPI.Models;
using System.Collections.Generic;

namespace RecipeAPI
{
    public class RecipeDataStore
    {
        public List<RecipeDto> Recipes { get; set; }
        public List<IngredientDto> Ingredients { get; set; }

        public static RecipeDataStore Current { get; } = new RecipeDataStore();


        public RecipeDataStore()
        {
          
            Ingredients = new List<IngredientDto>
            {
                new IngredientDto
                {
                    Id = 1,
                    Name = "Eggs",
                    Description = "Fresh, organic eggs for the best taste."
                },
                new IngredientDto
                {
                    Id = 2,
                    Name = "Salt",
                    Description = "A pinch of salt enhances the flavor."
                },
                new IngredientDto
                {
                    Id = 3,
                    Name = "Pepper",
                    Description = "Ground pepper adds a hint of spice."
                },
                new IngredientDto
                {
                    Id = 4,
                    Name = "Cheddar Cheese",
                    Description = "A delicious type of cheese."
                },
                new IngredientDto
                {
                    Id = 5,
                    Name = "Whole Wheat Bread",
                    Description = "Healthy bread option."
                },
                new IngredientDto
                {
                    Id = 6,
                    Name = "Worcestershire Sauce",
                    Description = "Adds a tangy flavor to dishes."
                },
                new IngredientDto
                {
                    Id = 7,
                    Name = "Pasta",
                    Description = "The main ingredient for spaghetti."
                },
                new IngredientDto
                {
                    Id = 8,
                    Name = "Ground Beef",
                    Description = "Lean ground beef for a rich sauce."
                },
                new IngredientDto
                {
                    Id = 9,
                    Name = "Tomato Sauce",
                    Description = "Savory tomato sauce with herbs and spices."
                }
            };

            Recipes = new List<RecipeDto>
            {
                new RecipeDto
                {
                    Id = 1,
                    Name = "Scrambled Eggs",
                    Description = "Delicious eggs for a nice brekkie",
                    Ingredients = new List<IngredientDto>
                    {
                        Ingredients[0], // Eggs
                        Ingredients[1], // Salt
                        Ingredients[2]  // Pepper
                    }
                },
                new RecipeDto
                {
                    Id = 2,
                    Name = "Cheese on Toast",
                    Description = "For students",
                    Ingredients = new List<IngredientDto>
                    {
                        Ingredients[3], // Cheddar Cheese
                        Ingredients[4], // Whole Wheat Bread
                        Ingredients[5]  // Worcestershire Sauce
                    }
                },
                new RecipeDto
                {
                    Id = 3,
                    Name = "Bolognese",
                    Description = "Spaghetti is great",
                    Ingredients = new List<IngredientDto>
                    {
                        Ingredients[6], // Pasta
                        Ingredients[7], // Ground Beef
                        Ingredients[8]  // Tomato Sauce
                    }
                }
            };

        }
    }
}
