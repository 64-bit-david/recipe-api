using Microsoft.EntityFrameworkCore;
using RecipeAPI.Entities;

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

      
    }
}
