using System.Runtime.InteropServices;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RecipeAPI.Models;

namespace RecipeAPI.Controllers
{
    [Route("api/ingredients")]
    [ApiController]
    public class IngredientsController : Controller
    {
        private readonly ILogger<IngredientsController> _logger;
        private readonly RecipeDataStore _recipeDataStore;


        public IngredientsController(ILogger<IngredientsController> logger, RecipeDataStore recipeDataStore)
        {
            _recipeDataStore = recipeDataStore ?? throw new ArgumentNullException(nameof(recipeDataStore));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public ActionResult<IEnumerable<IngredientDto>> GetIngredients()
        {
            return Ok(_recipeDataStore.Ingredients);
        }


        [HttpGet("{ingredientId}", Name ="GetIngredient")]
        public ActionResult<IngredientDto>GetIngredient(int ingredientId)
        {
            try
            {
                var ingredient = _recipeDataStore.Ingredients.FirstOrDefault(i => i.Id == ingredientId);
                if (ingredient == null)
                {
                    _logger.LogInformation($"Exception while getting ingredient with id of {ingredientId}.");
                    return NotFound();
                }

                return Ok(ingredient);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(
                    $"Exception while getting ingredient with id of {ingredientId}");
                return StatusCode(500, "An error happened while handling your request");
            }
        }

        [HttpPost]
        public ActionResult<IngredientDto> CreateIngredient(IngredientForCreationDto ingredient)
        {
            var maxIngredientId = _recipeDataStore.Ingredients.Max(i => i.Id);

            var newIngredientId = maxIngredientId++;

            var IngredientToPost = new IngredientDto()
            {
                Id = newIngredientId,
                Name = ingredient.Name,
                Description = ingredient.Description
            };

            _recipeDataStore.Ingredients.Add(IngredientToPost);

            return CreatedAtRoute("GetIngredient", new
            {
                Id=newIngredientId,
            },
            IngredientToPost);

        }

        [HttpPut("{ingredientid}")]
        public ActionResult<IngredientDto> UpdateIngredient(int ingredientId, IngredientForUpdateDto ingredient)
        {
            var existingIngredient = _recipeDataStore.Ingredients.FirstOrDefault(i => i.Id == ingredientId);
            if (existingIngredient == null) 
            {
                return NotFound();
            }

            var updatedIngredient = new IngredientDto()
            {
                Id = ingredient.Id,
                Name = ingredient.Name,
                Description = ingredient.Description
            };

            existingIngredient.Name = ingredient.Name;
            
            existingIngredient.Description = ingredient.Description;

            return NoContent();
            
        }



        [HttpPatch("{ingredientid}")]
        public ActionResult PartiallyUpdateIngredient(
            int ingredientId, JsonPatchDocument<IngredientForUpdateDto> patchDocument)
        {
            var ingredientFromStore = _recipeDataStore.Ingredients.FirstOrDefault(i => i.Id == ingredientId);
            if (ingredientFromStore == null)
            {
                return NotFound();
            }

            
            var ingredientToPatch = new IngredientForUpdateDto()
            {
                Name = ingredientFromStore.Name,
                Description = ingredientFromStore.Description
            };

            
            //pass modelstate to check that the patch is valid
            patchDocument.ApplyTo(ingredientToPatch, ModelState);
            

            if(!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            //adds another validation layer to check ingredientToPatch
            //after running patchDoc. ensures model is valid after patch op.
            if (!TryValidateModel(ingredientToPatch))
            {
                return BadRequest(ModelState);
            }

            ingredientFromStore.Name = ingredientToPatch.Name;
            ingredientFromStore.Description = ingredientToPatch.Description;

            return NoContent();
        }

        [HttpDelete("{ingredientId}")]
        public ActionResult DeleteIngredient(int ingredientId) 
        {
            var ingredientToDelete = _recipeDataStore.Ingredients.FirstOrDefault(i=> i.Id == ingredientId);
            if(ingredientToDelete == null)
            {
                return NotFound();
            }

            _recipeDataStore.Ingredients.Remove(ingredientToDelete);

            return NoContent();


        }


    }
}
