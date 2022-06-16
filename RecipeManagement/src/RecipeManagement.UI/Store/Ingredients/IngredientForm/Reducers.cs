using Fluxor;

namespace RecipeManagement.UI.Store.Ingredients.IngredientForm;

public static class IngredientFormReducers
{
    [ReducerMethod(typeof(IngredientFormInitializeAction))]
    public static IngredientFormState OnInitialize(IngredientFormState state) => IngredientFormState.Empty;

    [ReducerMethod(typeof(IngredientFormLoadDataAction))]
    public static IngredientFormState OnLoad(IngredientFormState state) => state with {IsLoading = true};

    [ReducerMethod]
    public static IngredientFormState OnFinishLoad(IngredientFormState state, IngredientFormSetDataAction action) =>
        state with {IsLoading = false, Model = action.Model};

    [ReducerMethod(typeof(IngredientFormLoadDataFailedAction))]
    public static IngredientFormState OnLoadFailed(IngredientFormState state) => state with {IsLoading = false};

    [ReducerMethod(typeof(IngredientFormSubmitAction))]
    public static IngredientFormState OnSubmit(IngredientFormState state) => state with {IsSubmitting = true};

    [ReducerMethod(typeof(IngredientFormSubmitSucceededAction))]
    public static IngredientFormState OnSubmitSucceeded(IngredientFormState state) => state with {IsSubmitting = false};

    [ReducerMethod(typeof(IngredientFormSubmitFailedAction))]
    public static IngredientFormState OnSubmitFailed(IngredientFormState state) => state with {IsSubmitting = false};
}