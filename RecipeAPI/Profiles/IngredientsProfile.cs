using AutoMapper;

namespace RecipeAPI.Profiles
{
    public class IngredientsProfile : Profile
    {
        public IngredientsProfile()
        {
            CreateMap<Entities.Ingredient, Models.IngredientDto>();
        }
    }
}
