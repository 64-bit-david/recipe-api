using RecipeAPI.Entities;
using RecipeAPI.Models;

namespace RecipeAPI.Services
{
    public interface IRecipeRepository
    {
        Task<IEnumerable<Recipe>> GetRecipesAsync();

        Task<Recipe?>GetRecipeAsync(int id);

        Task<IEnumerable<Ingredient>> GetIngredientsAsync();

        Task<Ingredient?> GetIngredientAsync(int id);

        void CreateIngredient(Ingredient ingredient);
        void UpdateIngredient(Ingredient ingredient);



        Task<bool> IngredientExists(int id);

        Task<bool> SaveChangesAsync();

        void DeleteIngredientAsync(Ingredient ingredient);



    }
}
