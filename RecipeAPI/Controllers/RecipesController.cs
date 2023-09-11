using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecipe(int id)
        {
            var recipeEntity = await _recipeRepository.GetRecipeAsync(id);

            if (recipeEntity == null)
            {
                return NotFound();
            }

            foreach (var ingredient in recipeEntity.RecipeIngredients)
            {
                // You can log or print details about each ingredient here
                _logger.LogInformation($"Ingredient Id: {ingredient.Ingredient.Id}, Name: {ingredient.Ingredient.Name}");
            }
        


            return Ok(_mapper.Map<RecipeDto>(recipeEntity));
        }



    }
}
