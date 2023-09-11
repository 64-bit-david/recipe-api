using Microsoft.EntityFrameworkCore;
using RecipeAPI.Entities;

namespace RecipeAPI
{
    public class RecipeContext : DbContext
    {
        public DbSet<Recipe> Recipes { get; set; } = null!;

        public DbSet<Ingredient> Ingredients { get; set; } = null!;

        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }


        public RecipeContext(DbContextOptions<RecipeContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure your entity relationships and any other database-specific settings here
            modelBuilder.Entity<RecipeIngredient>()
                .HasKey(ri => new { ri.RecipeId, ri.IngredientId });

            modelBuilder.Entity<RecipeIngredient>()
                .HasOne(ri => ri.Recipe)
                .WithMany(r => r.RecipeIngredients)
                .HasForeignKey(ri => ri.RecipeId);

            modelBuilder.Entity<RecipeIngredient>()
                .HasOne(ri => ri.Ingredient)
                .WithMany(i => i.RecipeIngredients)
                .HasForeignKey(ri => ri.IngredientId);

            SeedData(modelBuilder);

        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Create and seed three recipes
            modelBuilder.Entity<Recipe>().HasData(
                new Recipe("Spaghetti Carbonara") { Id = 1, Description = "Classic Italian pasta dish" },
                new Recipe("Chicken Alfredo") { Id = 2, Description = "Creamy chicken pasta" },
                new Recipe("Vegetable Stir-Fry") { Id = 3, Description = "Healthy vegetable stir-fry" }
            );

            // Create and seed four ingredients
            modelBuilder.Entity<Ingredient>().HasData(
                new Ingredient("Pasta") { Id = 1, Description = "Spaghetti pasta" },
                new Ingredient("Eggs") { Id = 2, Description = "Fresh eggs" },
                new Ingredient("Bacon") { Id = 3, Description = "Smoked bacon strips" },
                new Ingredient("Parmesan Cheese") { Id = 4, Description = "Grated Parmesan cheese" },
                new Ingredient("Chicken") { Id = 5, Description = "Boneless chicken breast" },
                new Ingredient("Heavy Cream") { Id = 6, Description = "Heavy cream for sauce" },
                new Ingredient("Broccoli") { Id = 7, Description = "Fresh broccoli florets" },
                new Ingredient("Carrots") { Id = 8, Description = "Sliced carrots" }
            );

            // Create and seed RecipeIngredient relationships
            modelBuilder.Entity<RecipeIngredient>().HasData(
                // Spaghetti Carbonara ingredients
                new RecipeIngredient { RecipeId = 1, IngredientId = 1, Quantity = 200, UnitOfMeasurement = "grams" },
                new RecipeIngredient { RecipeId = 1, IngredientId = 2, Quantity = 2, UnitOfMeasurement = "large" },
                new RecipeIngredient { RecipeId = 1, IngredientId = 3, Quantity = 100, UnitOfMeasurement = "grams" },
                new RecipeIngredient { RecipeId = 1, IngredientId = 4, Quantity = 50, UnitOfMeasurement = "grams" },
                // Chicken Alfredo ingredients
                new RecipeIngredient { RecipeId = 2, IngredientId = 5, Quantity = 400, UnitOfMeasurement = "grams" },
                new RecipeIngredient { RecipeId = 2, IngredientId = 6, Quantity = 250, UnitOfMeasurement = "ml" },
                new RecipeIngredient { RecipeId = 2, IngredientId = 7, Quantity = 150, UnitOfMeasurement = "grams" },
                new RecipeIngredient { RecipeId = 2, IngredientId = 4, Quantity = 30, UnitOfMeasurement = "grams" },
                // Vegetable Stir-Fry ingredients
                new RecipeIngredient { RecipeId = 3, IngredientId = 7, Quantity = 200, UnitOfMeasurement = "grams" },
                new RecipeIngredient { RecipeId = 3, IngredientId = 8, Quantity = 150, UnitOfMeasurement = "grams" },
                new RecipeIngredient { RecipeId = 3, IngredientId = 6, Quantity = 100, UnitOfMeasurement = "ml" },
                new RecipeIngredient { RecipeId = 3, IngredientId = 5, Quantity = 300, UnitOfMeasurement = "grams" }
            );
        }

    }
}
