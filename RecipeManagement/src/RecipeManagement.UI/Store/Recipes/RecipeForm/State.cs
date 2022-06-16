using Fluxor;
using FormModel = RecipeManagement.UI.Components.Recipes.RecipeForm.RecipeFormModel;

namespace RecipeManagement.UI.Store.Recipes.RecipeForm;

[FeatureState]
public record RecipeFormState(FormModel Model, ICollection<string> Errors, bool IsLoading, bool IsSubmitting)
{
    public static RecipeFormState Empty => new();
    private RecipeFormState() : this(new FormModel(), new List<string>(), false, false) {}
}