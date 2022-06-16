using RecipeManagement.Contracts.Ingredients;

namespace RecipeManagement.Domain.Ingredients.Validators;

public class IngredientForUpdateDtoValidator: IngredientForManipulationDtoValidator<IngredientForUpdateDto>
{
    public IngredientForUpdateDtoValidator()
    {
        // add fluent validation rules that should only be run on update operations here
        //https://fluentvalidation.net/
    }
}