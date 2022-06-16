using Fluxor;

namespace RecipeManagement.UI.Store.Recipes.RecipesList;

public static class RecipesListReducers
{
    [ReducerMethod(typeof(StoreInitializedAction))]
    public static RecipesListState OnInitialize(RecipesListState state) => RecipesListState.Empty;
    
    [ReducerMethod(typeof(RecipesListLoadAction))]
    public static RecipesListState OnLoad(RecipesListState state) => state with {IsLoading = true};

    [ReducerMethod]
    public static RecipesListState OnSetData(RecipesListState state, RecipesListSetDataAction action) => state with
    {
        IsLoading = false,
        DataSet = action.Data,
        TotalItems = action.Data.TotalCount
    };

    [ReducerMethod(typeof(RecipesListDeleteAction))]
    public static RecipesListState OnDelete(RecipesListState state) => state with {IsLoading = true};

    [ReducerMethod(typeof(RecipesListDeleteSucceededAction))]
    public static RecipesListState OnDeleteSucceeded(RecipesListState state) => state with {IsLoading = false};

    [ReducerMethod(typeof(RecipesListDeleteFailedAction))]
    public static RecipesListState OnDeleteFailed(RecipesListState state) => state with {IsLoading = false};
}