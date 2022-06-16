using FormModel = RecipeManagement.UI.Components.Recipes.RecipeForm.RecipeFormModel;

namespace RecipeManagement.UI.Store.Recipes.RecipeForm;

public record RecipeFormInitializeAction;
public record RecipeFormLoadDataAction(Guid RecipeId);
public record RecipeFormSetDataAction(FormModel Model);
public record RecipeFormLoadDataFailedAction;
public record RecipeFormSubmitAction(FormModel Model);
public record RecipeFormSubmitSucceededAction;
public record RecipeFormSubmitFailedAction;