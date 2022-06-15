namespace RecipeManagement.IntegrationTests.FeatureTests.Ingredients;

using RecipeManagement.Domain.Ingredients.Dtos;
using RecipeManagement.SharedTestHelpers.Fakes.Ingredient;
using SharedKernel.Exceptions;
using RecipeManagement.Domain.Ingredients.Features;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using static TestFixture;

public class IngredientListQueryTests : TestBase
{
    
    [Test]
    public async Task can_get_ingredient_list()
    {
        // Arrange
        var fakeIngredientOne = FakeIngredient.Generate(new FakeIngredientForCreationDto().Generate());
        var fakeIngredientTwo = FakeIngredient.Generate(new FakeIngredientForCreationDto().Generate());
        var queryParameters = new IngredientParametersDto();

        await InsertAsync(fakeIngredientOne, fakeIngredientTwo);

        // Act
        var query = new GetIngredientList.IngredientListQuery(queryParameters);
        var ingredients = await SendAsync(query);

        // Assert
        ingredients.Count.Should().BeGreaterThanOrEqualTo(2);
    }
    
    [Test]
    public async Task can_get_ingredient_list_with_expected_page_size_and_number()
    {
        //Arrange
        var fakeIngredientOne = FakeIngredient.Generate(new FakeIngredientForCreationDto().Generate());
        var fakeIngredientTwo = FakeIngredient.Generate(new FakeIngredientForCreationDto().Generate());
        var fakeIngredientThree = FakeIngredient.Generate(new FakeIngredientForCreationDto().Generate());
        var queryParameters = new IngredientParametersDto() { PageSize = 1, PageNumber = 2 };

        await InsertAsync(fakeIngredientOne, fakeIngredientTwo, fakeIngredientThree);

        //Act
        var query = new GetIngredientList.IngredientListQuery(queryParameters);
        var ingredients = await SendAsync(query);

        // Assert
        ingredients.Should().HaveCount(1);
    }
    
    [Test]
    public async Task can_filter_ingredient_list_using_RecipeId()
    {
        //Arrange
        var fakeIngredientOne = FakeIngredient.Generate(new FakeIngredientForCreationDto()
            .RuleFor(i => i.RecipeId, _ => 1)
            .Generate());
        var fakeIngredientTwo = FakeIngredient.Generate(new FakeIngredientForCreationDto()
            .RuleFor(i => i.RecipeId, _ => 2)
            .Generate());
        var queryParameters = new IngredientParametersDto() { Filters = $"RecipeId == {fakeIngredientTwo.RecipeId}" };

        await InsertAsync(fakeIngredientOne, fakeIngredientTwo);

        //Act
        var query = new GetIngredientList.IngredientListQuery(queryParameters);
        var ingredients = await SendAsync(query);

        // Assert
        ingredients.Should().HaveCount(1);
        ingredients
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeIngredientTwo, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_filter_ingredient_list_using_Name()
    {
        //Arrange
        var fakeIngredientOne = FakeIngredient.Generate(new FakeIngredientForCreationDto()
            .RuleFor(i => i.Name, _ => "alpha")
            .Generate());
        var fakeIngredientTwo = FakeIngredient.Generate(new FakeIngredientForCreationDto()
            .RuleFor(i => i.Name, _ => "bravo")
            .Generate());
        var queryParameters = new IngredientParametersDto() { Filters = $"Name == {fakeIngredientTwo.Name}" };

        await InsertAsync(fakeIngredientOne, fakeIngredientTwo);

        //Act
        var query = new GetIngredientList.IngredientListQuery(queryParameters);
        var ingredients = await SendAsync(query);

        // Assert
        ingredients.Should().HaveCount(1);
        ingredients
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeIngredientTwo, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_filter_ingredient_list_using_Unit()
    {
        //Arrange
        var fakeIngredientOne = FakeIngredient.Generate(new FakeIngredientForCreationDto()
            .RuleFor(i => i.Unit, _ => "alpha")
            .Generate());
        var fakeIngredientTwo = FakeIngredient.Generate(new FakeIngredientForCreationDto()
            .RuleFor(i => i.Unit, _ => "bravo")
            .Generate());
        var queryParameters = new IngredientParametersDto() { Filters = $"Unit == {fakeIngredientTwo.Unit}" };

        await InsertAsync(fakeIngredientOne, fakeIngredientTwo);

        //Act
        var query = new GetIngredientList.IngredientListQuery(queryParameters);
        var ingredients = await SendAsync(query);

        // Assert
        ingredients.Should().HaveCount(1);
        ingredients
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeIngredientTwo, options =>
                options.ExcludingMissingMembers());
    }

}