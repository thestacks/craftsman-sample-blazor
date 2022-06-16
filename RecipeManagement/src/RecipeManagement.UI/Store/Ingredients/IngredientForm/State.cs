using Fluxor;
using FormModel = RecipeManagement.UI.Components.Ingredients.IngredientForm.IngredientFormModel;

namespace RecipeManagement.UI.Store.Ingredients.IngredientForm;

[FeatureState]
public record IngredientFormState(FormModel Model, ICollection<string> Errors, bool IsLoading, bool IsSubmitting)
{
    public static IngredientFormState Empty => new();
    private IngredientFormState() : this(new FormModel(), new List<string>(), false, false) {}
}