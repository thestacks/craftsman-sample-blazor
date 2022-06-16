using Radzen;
using RecipeManagement.Contracts;
using RecipeManagement.Contracts.Recipes;

namespace RecipeManagement.UI.Store.Recipes.RecipesList;

public record RecipesListInitializeAction;
public record RecipesListLoadAction(LoadDataArgs Parameters);
public record RecipesListSetDataAction(PagedList<RecipeDto> Data);
public record RecipesListDeleteAction(Guid Id);

public record RecipesListDeleteSucceededAction;

public record RecipesListDeleteFailedAction;
