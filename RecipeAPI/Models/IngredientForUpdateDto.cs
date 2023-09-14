using System.ComponentModel.DataAnnotations;

namespace RecipeAPI.Models
{
    public class IngredientForUpdateDto
    {

        //no id needed
        //add errormessage to required to provide a custom error message
        [Required(ErrorMessage = "You should provide a name value")]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(200)]
        [MinLength(10)]
        public string? Description { get; set; }


        public ICollection<RecipeWithoutIngredientsDto>? Recipes { get; set; }


    }
}
