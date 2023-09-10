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
        [HttpGet]
        public ActionResult<IEnumerable<IngredientDto>> GetIngredients()
        {
            return Ok(RecipeDataStore.Current.Ingredients);
        }


        [HttpGet("{ingredientId}", Name ="GetIngredient")]
        public ActionResult<IngredientDto>GetIngredient(int ingredientId)
        {
            var ingredient = RecipeDataStore.Current.Ingredients.FirstOrDefault(i => i.Id == ingredientId);
            if (ingredient == null)
            {
                return NotFound();
            }

            return Ok(ingredient);
        }

        [HttpPost]
        public ActionResult<IngredientDto> CreateIngredient(IngredientForCreationDto ingredient)
        {
            var maxIngredientId = RecipeDataStore.Current.Ingredients.Max(i => i.Id);

            var newIngredientId = maxIngredientId++;

            var IngredientToPost = new IngredientDto()
            {
                Id = newIngredientId,
                Name = ingredient.Name,
                Description = ingredient.Description
            };

            RecipeDataStore.Current.Ingredients.Add(IngredientToPost);

            return CreatedAtRoute("GetIngredient", new
            {
                Id=newIngredientId,
            },
            IngredientToPost);

        }

        [HttpPut("{ingredientid}")]
        public ActionResult<IngredientDto> UpdateIngredient(int ingredientId, IngredientForUpdateDto ingredient)
        {
            var existingIngredient = RecipeDataStore.Current.Ingredients.FirstOrDefault(i => i.Id == ingredientId);
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
            var ingredientFromStore = RecipeDataStore.Current.Ingredients.FirstOrDefault(i => i.Id == ingredientId);
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
            var ingredientToDelete = RecipeDataStore.Current.Ingredients.FirstOrDefault(i=> i.Id == ingredientId);
            if(ingredientToDelete == null)
            {
                return NotFound();
            }

            RecipeDataStore.Current.Ingredients.Remove(ingredientToDelete);

            return NoContent();


        }


    }
}
