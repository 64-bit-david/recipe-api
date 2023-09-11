using RecipeAPI.Entities;

namespace RecipeAPI.Services
{
    public interface IRecipeRepository
    {
        Task<IEnumerable<Recipe>> GetRecipesAsync();

        Task<Recipe?>GetRecipeAsync(int id);



    }
}
