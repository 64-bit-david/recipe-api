namespace RecipeAPI.Models
{
    public class IngredientForRecipeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Quantity { get; set; }
        public string UnitOfMeasurement { get; set; }
    }
}
