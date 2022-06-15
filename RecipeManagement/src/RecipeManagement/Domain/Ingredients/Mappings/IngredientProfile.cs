namespace RecipeManagement.Domain.Ingredients.Mappings;

using RecipeManagement.Domain.Ingredients.Dtos;
using AutoMapper;
using RecipeManagement.Domain.Ingredients;

public class IngredientProfile : Profile
{
    public IngredientProfile()
    {
        //createmap<to this, from this>
        CreateMap<Ingredient, IngredientDto>()
            .ReverseMap();
        CreateMap<IngredientForCreationDto, Ingredient>();
        CreateMap<IngredientForUpdateDto, Ingredient>()
            .ReverseMap();
    }
}