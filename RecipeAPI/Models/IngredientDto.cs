﻿using System.ComponentModel.DataAnnotations;
using RecipeAPI.Entities;

namespace RecipeAPI.Models
{
    public class IngredientDto
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }


        public ICollection<RecipeWithoutIngredientsDto> Recipes { get; set; } = new List<RecipeWithoutIngredientsDto>();

        
    }
}
