namespace RecipeManagement.Domain.Recipes.Mappings;

using RecipeManagement.Domain.Recipes.Dtos;
using AutoMapper;
using RecipeManagement.Domain.Recipes;

public class RecipeProfile : Profile
{
    public RecipeProfile()
    {
        //createmap<to this, from this>
        CreateMap<Recipe, RecipeDto>()
            .ReverseMap();
        CreateMap<RecipeForCreationDto, Recipe>();
        CreateMap<RecipeForUpdateDto, Recipe>()
            .ReverseMap();
    }
}