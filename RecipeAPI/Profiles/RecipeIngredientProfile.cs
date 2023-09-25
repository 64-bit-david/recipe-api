using AutoMapper;

namespace RecipeAPI.Profiles
{
    public class RecipeIngredientProfile : Profile
    {
        public RecipeIngredientProfile() 
        {
            CreateMap<Entities.RecipeIngredient, Models.RecipeIngredientForCreationDto>();
            CreateMap<Models.RecipeIngredientForCreationDto, Entities.RecipeIngredient>();

        }
    }
}
