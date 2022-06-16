 using RecipeManagement.Contracts.Recipes;

namespace RecipeManagement.Domain.Recipes;

using RecipeManagement.Domain.Recipes.Mappings;
using RecipeManagement.Domain.Recipes.Validators;
using RecipeManagement.Domain.Recipes.DomainEvents;
using AutoMapper;
using FluentValidation;
using Sieve.Attributes;


public class Recipe : BaseEntity
{
    [Sieve(CanFilter = true, CanSort = true)]
    public virtual string Title { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public virtual string Directions { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public virtual string RecipeSourceLink { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public virtual string Description { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public virtual string ImageLink { get; private set; }


    public static Recipe Create(RecipeForCreationDto recipeForCreationDto)
    {
        new RecipeForCreationDtoValidator().ValidateAndThrow(recipeForCreationDto);
        var mapper = new Mapper(new MapperConfiguration(cfg => {
            cfg.AddProfile<RecipeProfile>();
        }));
        var newRecipe = mapper.Map<Recipe>(recipeForCreationDto);
        newRecipe.QueueDomainEvent(new RecipeCreated(){ Recipe = newRecipe });
        
        return newRecipe;
    }

    public void Update(RecipeForUpdateDto recipeForUpdateDto)
    {
        new RecipeForUpdateDtoValidator().ValidateAndThrow(recipeForUpdateDto);
        var mapper = new Mapper(new MapperConfiguration(cfg => {
            cfg.AddProfile<RecipeProfile>();
        }));
        mapper.Map(recipeForUpdateDto, this);
        QueueDomainEvent(new RecipeUpdated(){ Id = Id });
    }
    
    protected Recipe() { } // For EF + Mocking
}