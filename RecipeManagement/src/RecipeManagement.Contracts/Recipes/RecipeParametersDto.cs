namespace RecipeManagement.Contracts.Recipes
{
    public class RecipeParametersDto : BasePaginationParameters
    {
        public string Filters { get; set; } = string.Empty;
        public string SortOrder { get; set; } = string.Empty;
    }
}