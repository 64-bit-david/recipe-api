using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using RecipeAPI.Entities;
using RecipeAPI.Models;
using RecipeAPI.Services;

namespace RecipeAPI.Controllers
{
    [ApiController]
    [Route("api/recipes")]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<RecipesController> _logger;


        public RecipesController(IRecipeRepository recipeRepository, IMapper mapper, ILogger<RecipesController> logger)
        {
            _recipeRepository = recipeRepository ?? throw new ArgumentNullException(nameof(recipeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecipeWithoutIngredientsDto>>> GetRecipes()
        {
            var recipeEntities = await _recipeRepository.GetRecipesAsync();
            return Ok(_mapper.Map<IEnumerable<RecipeWithoutIngredientsDto>>(recipeEntities));
        }

        [HttpGet("{recipeid}", Name = "GetRecipe")]
        public async Task<IActionResult> GetRecipe(int recipeid)
        {
            var recipeEntity = await _recipeRepository.GetRecipeAsync(recipeid);

            if (recipeEntity == null)
            {
                return NotFound();
            }

            var recipeDto = new RecipeDto
            {
                Id = recipeEntity.Id,
                Name = recipeEntity.Name,
                Description = recipeEntity.Description,
                Ingredients = recipeEntity.RecipeIngredients.Select(ri => new IngredientForRecipeDto
                {
                    Id = ri.Ingredient.Id,
                    Name = ri.Ingredient.Name,
                    Description = ri.Ingredient.Description,
                    Quantity = ri.Quantity,
                    UnitOfMeasurement = ri.UnitOfMeasurement
                }).ToList()
            };


            return Ok(recipeDto);
        }



        //Still need create/upate/delete

        [HttpPost]
        public async Task<ActionResult<RecipeForCreationDto>> AddRecipe(RecipeForCreationDto recipeDto)
        {
            var recipeEntity = _mapper.Map<Entities.Recipe>(recipeDto);

            _recipeRepository.CreateRecipe(recipeEntity);

            await _recipeRepository.SaveChangesAsync();

            var recipeDtoToReturn = _mapper.Map<RecipeWithoutIngredientsDto>(recipeEntity);

            return CreatedAtRoute("GetRecipe", new { recipeid = recipeDtoToReturn.Id }, recipeDtoToReturn);

        }

        [HttpPut ("{recipeid}")]
        public async Task<ActionResult>UpdateRecipe(int recipeid, RecipeForUpdateDto recipeToUpdateDto)
        {
            var recipeEntity = await _recipeRepository.GetRecipeAsync(recipeid);

            _mapper.Map(recipeToUpdateDto, recipeEntity);

            await _recipeRepository.SaveChangesAsync();

            return NoContent();
        }


        [HttpPost("{recipeId}/ingredients")]
        public async Task<ActionResult> AddIngredientToRecipe(int recipeId, [FromBody] RecipeIngredientForCreationDto recipeIngredientForCreationDto)
        {
            // Check if the recipe exists
            var recipe = await _recipeRepository.GetRecipeAsync(recipeId);
            if (recipe == null)
            {
                return NotFound("Recipe not found");
            }

            // Set the RecipeId on the entity
            var recipeIngredientEntity = _mapper.Map<Entities.RecipeIngredient>(recipeIngredientForCreationDto);
            recipeIngredientEntity.RecipeId = recipeId; // Set the RecipeId
                                                        // Set the IngredientId if it's available in the DTO
            recipeIngredientEntity.IngredientId = recipeIngredientForCreationDto.IngredientId;

            _recipeRepository.AddRecipeIngredient(recipeIngredientEntity);

            await _recipeRepository.SaveChangesAsync();

            return NoContent();
        }


        [HttpDelete ("{recipeId}/ingredients/{ingredientId}")]
        public async Task<IActionResult> RemoveIngredientFromRecipe(int recipeId, int ingredientId)
        {
            var recipe = await _recipeRepository.GetRecipeAsync(recipeId);
            if(recipe == null)
            {
                return NotFound();
            }

            var ingredientToRemove = recipe.RecipeIngredients.FirstOrDefault(ri => ri.IngredientId == ingredientId);

            if(ingredientToRemove == null)
            {
                return NotFound();
            }

            _recipeRepository.RemoveRecipeIngredient(recipeId, ingredientId);

            await _recipeRepository.SaveChangesAsync();

            return NoContent();


        }



    }
}
