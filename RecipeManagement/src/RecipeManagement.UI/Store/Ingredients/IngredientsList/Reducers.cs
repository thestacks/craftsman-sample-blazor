using Fluxor;

namespace RecipeManagement.UI.Store.Ingredients.IngredientsList;

public static class IngredientsListReducers
{
    [ReducerMethod(typeof(StoreInitializedAction))]
    public static IngredientsListState OnInitialize(IngredientsListState state) => IngredientsListState.Empty;
    
    [ReducerMethod(typeof(IngredientsListLoadAction))]
    public static IngredientsListState OnLoad(IngredientsListState state) => state with {IsLoading = true};

    [ReducerMethod]
    public static IngredientsListState OnSetData(IngredientsListState state, IngredientsListSetDataAction action) => state with
    {
        IsLoading = false,
        DataSet = action.Data,
        TotalItems = action.Data.TotalCount
    };

    [ReducerMethod(typeof(IngredientsListDeleteAction))]
    public static IngredientsListState OnDelete(IngredientsListState state) => state with {IsLoading = true};

    [ReducerMethod(typeof(IngredientsListDeleteSucceededAction))]
    public static IngredientsListState OnDeleteSucceeded(IngredientsListState state) => state with {IsLoading = false};

    [ReducerMethod(typeof(IngredientsListDeleteFailedAction))]
    public static IngredientsListState OnDeleteFailed(IngredientsListState state) => state with {IsLoading = false};
}