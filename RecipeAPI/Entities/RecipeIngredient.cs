using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeAPI.Entities
{
    //Represents a junction entity between recipe and ingredient
    public class RecipeIngredient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [ForeignKey("RecipeId")]
        public int RecipeId { get; set; }

        [ForeignKey("IngredientId")]
        public int IngredientId { get; set; }

        // Navigation property for the many-to-many relationship
        [InverseProperty("RecipeIngredients")]
        public Recipe Recipe { get; set; }

        [InverseProperty("RecipeIngredients")]
        public Ingredient Ingredient { get; set; }

        public decimal Quantity { get; set; }

        public string UnitOfMeasurement { get; set; }

    }
}
