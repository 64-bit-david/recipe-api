using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeAPI.Entities
{
    public class Recipe
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //generates new id automatically
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public  string  Name { get; set; }

        [MaxLength(200)]
        public string Description{ get; set; }

        public ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();

        public Recipe(string name)
        {
            Name = name;
        }


    }
}
