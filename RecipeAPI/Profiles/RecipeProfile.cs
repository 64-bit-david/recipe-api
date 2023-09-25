using AutoMapper;

namespace RecipeAPI.Profiles
{
    public class RecipeProfile : Profile
    {
        public RecipeProfile()
        {
            CreateMap<Entities.Recipe, Models.RecipeWithoutIngredientsDto>();

            CreateMap<Models.RecipeForCreationDto, Entities.Recipe>();

            CreateMap<Entities.Recipe, Models.RecipeForUpdateDto>();
            CreateMap<Models.RecipeForUpdateDto, Entities.Recipe>();


            CreateMap<Entities.Recipe, Models.RecipeDto>()
                .ForMember(dest => dest.Ingredients, opt => opt.MapFrom(src => src.RecipeIngredients.Select(ri => ri.Ingredient)));

        }
    }
}
