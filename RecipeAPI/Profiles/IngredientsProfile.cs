using AutoMapper;
using RecipeAPI.Entities;
using RecipeAPI.Models;

namespace RecipeAPI.Profiles
{
    public class IngredientsProfile : Profile
    {
        public IngredientsProfile()
        {
            CreateMap<Ingredient, IngredientWithoutRecipesDto>(); // Mapping from Ingredient to IngredientWithoutRecipesDto
            CreateMap<Ingredient, IngredientWithoutRecipesDto>();
            CreateMap<Ingredient, IngredientDto>()
                   .ForMember(dest => dest.Recipes, opt => opt.MapFrom(src => src.RecipeIngredients.Select(ri => ri.Recipe)));

            CreateMap<IngredientDto, Ingredient>();
            CreateMap<IngredientForCreationDto, Ingredient>();
            CreateMap<IngredientForUpdateDto, Ingredient>();
            CreateMap<Ingredient, IngredientForUpdateDto>();




        }

    }
}
