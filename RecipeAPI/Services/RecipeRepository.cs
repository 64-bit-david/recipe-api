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
    }
}
