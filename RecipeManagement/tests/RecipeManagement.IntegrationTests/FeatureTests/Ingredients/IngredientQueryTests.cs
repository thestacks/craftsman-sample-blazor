namespace RecipeManagement.IntegrationTests.FeatureTests.Ingredients;

using RecipeManagement.SharedTestHelpers.Fakes.Ingredient;
using RecipeManagement.Domain.Ingredients.Features;
using FluentAssertions;
using NUnit.Framework;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using static TestFixture;

public class IngredientQueryTests : TestBase
{
    [Test]
    public async Task can_get_existing_ingredient_with_accurate_props()
    {
        // Arrange
        var fakeIngredientOne = FakeIngredient.Generate(new FakeIngredientForCreationDto().Generate());
        await InsertAsync(fakeIngredientOne);

        // Act
        var query = new GetIngredient.IngredientQuery(fakeIngredientOne.Id);
        var ingredients = await SendAsync(query);

        // Assert
        ingredients.Should().BeEquivalentTo(fakeIngredientOne, options =>
            options.ExcludingMissingMembers());
    }

    [Test]
    public async Task get_ingredient_throws_notfound_exception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var query = new GetIngredient.IngredientQuery(badId);
        Func<Task> act = () => SendAsync(query);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}