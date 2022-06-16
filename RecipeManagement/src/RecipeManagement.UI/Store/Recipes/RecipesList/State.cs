using Fluxor;
using RecipeManagement.Contracts.Recipes;

namespace RecipeManagement.UI.Store.Recipes.RecipesList;

[FeatureState]
public record RecipesListState(IEnumerable<RecipeDto> DataSet, int TotalItems, bool IsLoading)
{
    public static RecipesListState Empty => new();
    private RecipesListState() : this(new List<RecipeDto>(), 0, false) {}
}