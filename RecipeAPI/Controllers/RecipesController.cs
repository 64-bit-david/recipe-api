using Microsoft.AspNetCore.Mvc;
using RecipeAPI.Models;

namespace RecipeAPI.Controllers
{
    [ApiController]
    [Route("api/recipes")]
    public class RecipesController : ControllerBase
    {

        [HttpGet]
        public ActionResult<IEnumerable<RecipeDto>> GetRecipes()
        {
            return Ok(RecipeDataStore.Current.Recipes);
        }

        [HttpGet("{id}")]
        public ActionResult<RecipeDto> GetRecipe(int id)
        {
            var recipe = RecipeDataStore.Current.Recipes.FirstOrDefault(r => r.Id == id);

            if(recipe == null)
            {
                return NotFound();
            }

            return Ok(recipe);
            
        }
    }
}
