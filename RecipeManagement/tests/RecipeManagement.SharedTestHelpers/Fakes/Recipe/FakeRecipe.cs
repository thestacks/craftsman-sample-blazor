using RecipeManagement.Contracts.Recipes;

namespace RecipeManagement.SharedTestHelpers.Fakes.Recipe;

using RecipeManagement.Domain.Recipes;

public class FakeRecipe
{
    public static Recipe Generate(RecipeForCreationDto recipeForCreationDto)
    {
        return Recipe.Create(recipeForCreationDto);
    }

    public static Recipe Generate()
    {
        return Recipe.Create(new FakeRecipeForCreationDto().Generate());
    }
}