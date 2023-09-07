using Microsoft.AspNetCore.Mvc;
using RecipeAPI.Models;

namespace RecipeAPI.Controllers
{
    [Route("api/ingredients")]
    [ApiController]
    public class IngredientsController : Controller
    {
        [HttpGet]
        public ActionResult<IEnumerable<IngredientDto>> GetIngredients()
        {
            return Ok(RecipeDataStore.Current.Ingredients);
        }


        [HttpGet("{ingredientId}")]
        public ActionResult<IngredientDto>GetIngredient(int ingredientId)
        {
            var ingredient = RecipeDataStore.Current.Ingredients.FirstOrDefault(i => i.Id == ingredientId);
            if (ingredient == null)
            {
                return NotFound();
            }

            return Ok(ingredient);
        }

    }
}
