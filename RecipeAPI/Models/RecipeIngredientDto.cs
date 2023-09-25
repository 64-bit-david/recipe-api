namespace RecipeAPI.Models
{
    public class RecipeIngredientDto
    {
        public int IngredientId { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }
        public decimal Quantity { get; set; }
        public string UnitOfMeasurement { get; set; }
    }
}
