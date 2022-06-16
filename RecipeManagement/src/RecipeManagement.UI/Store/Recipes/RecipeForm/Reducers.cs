using Fluxor;

namespace RecipeManagement.UI.Store.Recipes.RecipeForm;

public static class RecipeFormReducers
{
    [ReducerMethod(typeof(RecipeFormInitializeAction))]
    public static RecipeFormState OnInitialize(RecipeFormState state) => RecipeFormState.Empty;

    [ReducerMethod(typeof(RecipeFormLoadDataAction))]
    public static RecipeFormState OnLoad(RecipeFormState state) => state with {IsLoading = true};

    [ReducerMethod]
    public static RecipeFormState OnFinishLoad(RecipeFormState state, RecipeFormSetDataAction action) =>
        state with {IsLoading = false, Model = action.Model};

    [ReducerMethod(typeof(RecipeFormLoadDataFailedAction))]
    public static RecipeFormState OnLoadFailed(RecipeFormState state) => state with {IsLoading = false};

    [ReducerMethod(typeof(RecipeFormSubmitAction))]
    public static RecipeFormState OnSubmit(RecipeFormState state) => state with {IsSubmitting = true};

    [ReducerMethod(typeof(RecipeFormSubmitSucceededAction))]
    public static RecipeFormState OnSubmitSucceeded(RecipeFormState state) => state with {IsSubmitting = false};

    [ReducerMethod(typeof(RecipeFormSubmitFailedAction))]
    public static RecipeFormState OnSubmitFailed(RecipeFormState state) => state with {IsSubmitting = false};
}