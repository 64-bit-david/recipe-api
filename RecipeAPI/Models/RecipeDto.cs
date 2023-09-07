namespace RecipeAPI.Models
{
    public class RecipeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }     = string.Empty;
        public string? Description { get; set; }

        public int NumberOfIngredients
        {
            get
            {
                return Ingredients.Count;
            }
        }

        //initialise new collections empty - avoid null ref issues
        public ICollection<IngredientDto> Ingredients { get; set; }
            = new List<IngredientDto>();

    }
}
