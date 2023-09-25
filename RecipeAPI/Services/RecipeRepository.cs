using System.Collections;
using Microsoft.EntityFrameworkCore;
using RecipeAPI.Entities;
using RecipeAPI.Models;

namespace RecipeAPI.Services
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly RecipeContext _context;

        public RecipeRepository(RecipeContext context)
        {
             _context = context ?? throw new ArgumentNullException(nameof(context));
        }
     

        public async Task<IEnumerable<Recipe>> GetRecipesAsync()
        {
            return await _context.Recipes.OrderBy(r => r.Name).ToListAsync();
        }

        public async Task<Recipe?> GetRecipeAsync(int id)
        {
            return await _context.Recipes
                .Where(r => r.Id == id)
                .Include(r => r.RecipeIngredients) // Eagerly loa RecipeIngredients
                    .ThenInclude(ri => ri.Ingredient) // Eagerly load Ingredient for each RecipeIngredient
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Ingredient>> GetIngredientsAsync()
        {
            return await _context.Ingredients.OrderBy(r => r.Name).ToListAsync();
        }

        public async Task<Ingredient?>GetIngredientAsync(int id)
        {
            return await _context.Ingredients
                    .Where(i => i.Id == id)
                    .Include(i => i.RecipeIngredients)
                        .ThenInclude(ri => ri.Recipe)
                    .FirstOrDefaultAsync();
        }

        public void CreateIngredient(Ingredient ingredient)
        {
            _context.Ingredients.Add(ingredient);

        }

        public void CreateRecipe(Recipe recipe)
        {
            _context.Recipes.Add(recipe);
        }



        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

        public async Task<bool> IngredientExists(int id)
        {
            return await _context.Ingredients.AnyAsync(i => i.Id == id);  
        }

        public void UpdateIngredient(Ingredient ingredient)
        {
            _context.Ingredients.Update(ingredient);
        }

        public  void DeleteIngredientAsync(Ingredient ingredient)
        {
            _context.Ingredients.Remove(ingredient);
        }

        public void UpdateRecipe(Recipe recipe)
        {
            _context.Recipes.Update(recipe);
        }

        public async Task<IEnumerable<RecipeIngredient>> GetRecipeIngredientsAsync(int id)
        {
            var recipe = await GetRecipeAsync(id);

            return recipe.RecipeIngredients;
        }

        public void AddRecipeIngredient(RecipeIngredient recipeIngredient)
        {
            _context.RecipeIngredients.AddAsync(recipeIngredient);
        }

        public void RemoveRecipeIngredient(int recipeId, int ingredientId)
        {
            //find recipe
            var recipe = _context.Recipes
                 .Include(r => r.RecipeIngredients)
                 .FirstOrDefault(r => r.Id == recipeId);

            if (recipe == null)
            {
                throw new InvalidOperationException("Recipe not found");
            }

            var recipeIngredientToRemove = recipe.RecipeIngredients
                .FirstOrDefault(ri => ri.IngredientId == ingredientId);

            if (recipeIngredientToRemove == null)
            {
                throw new InvalidOperationException("Ingredient not found in the recipe");
            }

            // Remove the recipe ingredient from the collection
            recipe.RecipeIngredients.Remove(recipeIngredientToRemove);

        }
    }
}
