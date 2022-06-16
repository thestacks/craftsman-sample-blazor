using Radzen;
using RecipeManagement.Contracts;
using RecipeManagement.Contracts.Ingredients;

namespace RecipeManagement.UI.Store.Ingredients.IngredientsList;

public record IngredientsListInitializeAction;
public record IngredientsListLoadAction(LoadDataArgs Parameters);
public record IngredientsListSetDataAction(PagedList<IngredientDto> Data);
public record IngredientsListDeleteAction(Guid Id);

public record IngredientsListDeleteSucceededAction;

public record IngredientsListDeleteFailedAction;
