using System.Runtime.InteropServices;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RecipeAPI.Entities;
using RecipeAPI.Models;
using RecipeAPI.Services;

namespace RecipeAPI.Controllers
{
    [Route("api/ingredients")]
    [ApiController]
    public class IngredientsController : Controller
    {
        private readonly ILogger<IngredientsController> _logger;
        private readonly IMapper _mapper;
        private readonly IRecipeRepository _recipeRepository;

        public IngredientsController(ILogger<IngredientsController> logger, 
            IMapper mapper, IRecipeRepository recipeRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _recipeRepository = recipeRepository ?? throw new ArgumentNullException(nameof(recipeRepository));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IngredientWithoutRecipesDto>>> GetIngredients()
        {
            var ingredientEntities =   await _recipeRepository.GetIngredientsAsync();
            return Ok(_mapper.Map<IEnumerable<IngredientWithoutRecipesDto>>(ingredientEntities));

        }


        [HttpGet("{ingredientId}", Name = "GetIngredient")]
        public async Task<ActionResult<IngredientDto>> GetIngredient(int ingredientId)
        {
            var ingredientEntity = await _recipeRepository.GetIngredientAsync(ingredientId);

            if(ingredientEntity == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IngredientDto>(ingredientEntity));


        }

        [HttpPost]
        public async Task<ActionResult<IngredientDto>> CreateIngredient(IngredientForCreationDto ingredient)
        {
            var ingredientEntity = _mapper.Map<Entities.Ingredient>(ingredient);

             _recipeRepository.CreateIngredient(ingredientEntity);

            await _recipeRepository.SaveChangesAsync();

            var newIngredientToReturn = _mapper.Map<IngredientWithoutRecipesDto>(ingredientEntity);


            return CreatedAtRoute("GetIngredient", new { ingredientId = newIngredientToReturn.Id }, newIngredientToReturn);
        }


        [HttpPut("{ingredientid}")]
        public async Task<ActionResult> UpdateIngredient(int ingredientId, IngredientForUpdateDto ingredient)
        {
            var ingredientEntity = await _recipeRepository.GetIngredientAsync(ingredientId);

            if (ingredientEntity == null)
            {
                return NotFound();
            }

            _mapper.Map(ingredient, ingredientEntity);
            //_recipeRepository.UpdateIngredient(ingredientToUpdateEntity);

            await _recipeRepository.SaveChangesAsync();

            //var updatedIngredientDto = _mapper.Map<IngredientForUpdateDto>(ingredientToUpdateEntity);

            return NoContent();

        }



        [HttpPatch("{ingredientid}")]
        public async Task<ActionResult> PartiallyUpdateIngredient(
            int ingredientId, JsonPatchDocument<IngredientForUpdateDto> patchDocument)
        {
            var ingredientEntity = await _recipeRepository.GetIngredientAsync(ingredientId);

            if (ingredientEntity == null)
            {
                return NotFound();
            }


            var ingredientToPatch = _mapper.Map<IngredientForUpdateDto>(ingredientEntity);

        


            //pass modelstate to check that the patch is valid
            patchDocument.ApplyTo(ingredientToPatch, ModelState);


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //adds another validation layer to check ingredientToPatch
            //after running patchDoc. ensures model is valid after patch op.
            if (!TryValidateModel(ingredientToPatch))
            {
                return BadRequest(ModelState);
            }

            //_mapper.Map<Ingredient>(ingredientToPatch);
            _mapper.Map(ingredientToPatch, ingredientEntity);

          
            await _recipeRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{ingredientId}")]
        public async Task<ActionResult>DeleteIngredient(int ingredientId)
        {
            var ingredientToDelete = await _recipeRepository.GetIngredientAsync(ingredientId);
            if (ingredientToDelete == null)
            {
                return NotFound();
            }

             _recipeRepository.DeleteIngredientAsync(ingredientToDelete);

            await _recipeRepository.SaveChangesAsync();

            return NoContent();


        }


    }
}
