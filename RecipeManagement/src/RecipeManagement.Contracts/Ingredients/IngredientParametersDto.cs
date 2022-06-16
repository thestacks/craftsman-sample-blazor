namespace RecipeManagement.Contracts.Ingredients
{
    public class IngredientParametersDto : BasePaginationParameters
    {
        public string Filters { get; set; } = string.Empty;
        public string SortOrder { get; set; } = string.Empty;
    }
}