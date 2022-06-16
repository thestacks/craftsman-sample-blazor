using FormModel = RecipeManagement.UI.Components.Ingredients.IngredientForm.IngredientFormModel;

namespace RecipeManagement.UI.Store.Ingredients.IngredientForm;

public record IngredientFormInitializeAction;
public record IngredientFormLoadDataAction(Guid IngredientId);
public record IngredientFormSetDataAction(FormModel Model);
public record IngredientFormLoadDataFailedAction;
public record IngredientFormSubmitAction(FormModel Model);
public record IngredientFormSubmitSucceededAction;
public record IngredientFormSubmitFailedAction;