using RecipeManagement.Contracts.Ingredients;

namespace RecipeManagement.Domain.Ingredients.Features;

using RecipeManagement.Domain.Ingredients.Services;
using RecipeManagement.Domain.Ingredients;
using RecipeManagement.Services;
using AutoMapper;
using MediatR;

public static class AddIngredient
{
    public class AddIngredientCommand : IRequest<IngredientDto>
    {
        public readonly IngredientForCreationDto IngredientToAdd;

        public AddIngredientCommand(IngredientForCreationDto ingredientToAdd)
        {
            IngredientToAdd = ingredientToAdd;
        }
    }

    public class Handler : IRequestHandler<AddIngredientCommand, IngredientDto>
    {
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IIngredientRepository ingredientRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _ingredientRepository = ingredientRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IngredientDto> Handle(AddIngredientCommand request, CancellationToken cancellationToken)
        {
            var ingredient = Ingredient.Create(request.IngredientToAdd);
            await _ingredientRepository.Add(ingredient, cancellationToken);

            await _unitOfWork.CommitChanges(cancellationToken);

            var ingredientAdded = await _ingredientRepository.GetById(ingredient.Id, cancellationToken: cancellationToken);
            return _mapper.Map<IngredientDto>(ingredientAdded);
        }
    }
}