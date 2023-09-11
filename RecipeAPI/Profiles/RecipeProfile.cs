using AutoMapper;

namespace RecipeAPI.Profiles
{
    public class RecipeProfile : Profile
    {
        public RecipeProfile()
        {
            CreateMap<Entities.Recipe, Models.RecipeWithoutIngredientsDto>();


            CreateMap<Entities.Recipe, Models.RecipeDto>()
                .ForMember(dest => dest.Ingredients, opt => opt.MapFrom(src => src.RecipeIngredients.Select(ri => ri.Ingredient)));



        }
    }
}
