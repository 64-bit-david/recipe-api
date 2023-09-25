using RecipeAPI.Entities;
using RecipeAPI.Models;

namespace RecipeAPI.Services
{
    public interface IRecipeRepository
    {
        Task<IEnumerable<Recipe>> GetRecipesAsync();

        Task<Recipe?>GetRecipeAsync(int id);
        
        void CreateRecipe(Recipe recipe);

        void UpdateRecipe(Recipe recipe);






        Task<IEnumerable<Ingredient>> GetIngredientsAsync();

        Task<Ingredient?> GetIngredientAsync(int id);

        void CreateIngredient(Ingredient ingredient);
        void UpdateIngredient(Ingredient ingredient);

        Task<bool> IngredientExists(int id);

        Task<bool> SaveChangesAsync();

        void DeleteIngredientAsync(Ingredient ingredient);




        Task<IEnumerable<RecipeIngredient>>GetRecipeIngredientsAsync(int id);

        void AddRecipeIngredient(RecipeIngredient recipeIngredient);

        void RemoveRecipeIngredient(int recipeId, int ingreidentId);


    }
}
