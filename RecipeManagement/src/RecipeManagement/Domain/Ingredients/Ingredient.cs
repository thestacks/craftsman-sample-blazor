using RecipeManagement.Contracts.Ingredients;

namespace RecipeManagement.Domain.Ingredients;

using RecipeManagement.Domain.Ingredients.Mappings;
using RecipeManagement.Domain.Ingredients.Validators;
using RecipeManagement.Domain.Ingredients.DomainEvents;
using AutoMapper;
using FluentValidation;
using Sieve.Attributes;


public class Ingredient : BaseEntity
{
    [Sieve(CanFilter = true, CanSort = true)]
    public virtual int? RecipeId { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public virtual string Name { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public virtual string Unit { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public virtual double? Amount { get; private set; }


    public static Ingredient Create(IngredientForCreationDto ingredientForCreationDto)
    {
        new IngredientForCreationDtoValidator().ValidateAndThrow(ingredientForCreationDto);
        var mapper = new Mapper(new MapperConfiguration(cfg => {
            cfg.AddProfile<IngredientProfile>();
        }));
        var newIngredient = mapper.Map<Ingredient>(ingredientForCreationDto);
        newIngredient.QueueDomainEvent(new IngredientCreated(){ Ingredient = newIngredient });
        
        return newIngredient;
    }

    public void Update(IngredientForUpdateDto ingredientForUpdateDto)
    {
        new IngredientForUpdateDtoValidator().ValidateAndThrow(ingredientForUpdateDto);
        var mapper = new Mapper(new MapperConfiguration(cfg => {
            cfg.AddProfile<IngredientProfile>();
        }));
        mapper.Map(ingredientForUpdateDto, this);
        QueueDomainEvent(new IngredientUpdated(){ Id = Id });
    }
    
    protected Ingredient() { } // For EF + Mocking
}