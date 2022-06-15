namespace RecipeManagement.Domain.Ingredients.Dtos
{
    using System.Collections.Generic;
    using System;

    public abstract class IngredientForManipulationDto 
    {
        public int? RecipeId { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public double? Amount { get; set; }
    }
}