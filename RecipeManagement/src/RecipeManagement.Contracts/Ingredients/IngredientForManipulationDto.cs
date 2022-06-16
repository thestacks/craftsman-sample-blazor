namespace RecipeManagement.Contracts.Ingredients
{
    public abstract class IngredientForManipulationDto 
    {
        public int? RecipeId { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public double? Amount { get; set; }
    }
}