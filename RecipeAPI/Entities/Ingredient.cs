using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeAPI.Entities
{
    public class Ingredient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //generates new id automatically
        public  int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<RecipeIngredient> RecipeIngredients { get; set; }


        public Ingredient(string name)
        {
            Name = name;
        }
    }
}
