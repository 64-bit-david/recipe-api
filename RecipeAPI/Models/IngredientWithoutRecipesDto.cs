using System.ComponentModel.DataAnnotations;

namespace RecipeAPI.Models
{
    public class IngredientWithoutRecipesDto
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}
