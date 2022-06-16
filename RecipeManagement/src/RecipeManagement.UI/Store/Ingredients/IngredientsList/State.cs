using Fluxor;
using RecipeManagement.Contracts.Ingredients;

namespace RecipeManagement.UI.Store.Ingredients.IngredientsList;

[FeatureState]
public record IngredientsListState(IEnumerable<IngredientDto> DataSet, int TotalItems, bool IsLoading)
{
    public static IngredientsListState Empty => new();
    private IngredientsListState() : this(new List<IngredientDto>(), 0, false) {}
}