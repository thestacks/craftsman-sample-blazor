using RecipeManagement.Contracts.Ingredients;

namespace RecipeManagement.SharedTestHelpers.Fakes.Ingredient;

using RecipeManagement.Domain.Ingredients;

public class FakeIngredient
{
    public static Ingredient Generate(IngredientForCreationDto ingredientForCreationDto)
    {
        return Ingredient.Create(ingredientForCreationDto);
    }

    public static Ingredient Generate()
    {
        return Ingredient.Create(new FakeIngredientForCreationDto().Generate());
    }
}