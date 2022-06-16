using RecipeManagement.Contracts.Recipes;

namespace RecipeManagement.Domain.Recipes.Validators;

public class RecipeForUpdateDtoValidator: RecipeForManipulationDtoValidator<RecipeForUpdateDto>
{
    public RecipeForUpdateDtoValidator()
    {
        // add fluent validation rules that should only be run on update operations here
        //https://fluentvalidation.net/
    }
}