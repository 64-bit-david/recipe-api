using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RecipeAPI.Models;

namespace RecipeAPI.Controllers
{
    [ApiController]
    [Route("api/recipes")]
    public class RecipesController : ControllerBase
    {
        private readonly RecipeDataStore _recipeDataStore;

        public RecipesController(RecipeDataStore recipeDataStore)
        {
            _recipeDataStore = recipeDataStore ?? throw new ArgumentNullException(nameof(recipeDataStore));
        }

        [HttpGet]
        public ActionResult<IEnumerable<RecipeDto>> GetRecipes()
        {
            return Ok(_recipeDataStore.Recipes);
        }

        [HttpGet("{id}")]
        public ActionResult<RecipeDto> GetRecipe(int id)
        {
            var recipe = _recipeDataStore.Recipes.FirstOrDefault(r => r.Id == id);

            if(recipe == null)
            {
                return NotFound();
            }

            return Ok(recipe);   
        }


        
    }
}
