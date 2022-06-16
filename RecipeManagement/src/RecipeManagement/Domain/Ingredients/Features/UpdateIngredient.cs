using RecipeManagement.Contracts.Ingredients;

namespace RecipeManagement.Domain.Ingredients.Features;

using RecipeManagement.Domain.Ingredients.Services;
using RecipeManagement.Services;
using AutoMapper;
using MediatR;

public static class UpdateIngredient
{
    public class UpdateIngredientCommand : IRequest<bool>
    {
        public readonly Guid Id;
        public readonly IngredientForUpdateDto IngredientToUpdate;

        public UpdateIngredientCommand(Guid ingredient, IngredientForUpdateDto newIngredientData)
        {
            Id = ingredient;
            IngredientToUpdate = newIngredientData;
        }
    }

    public class Handler : IRequestHandler<UpdateIngredientCommand, bool>
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

        public async Task<bool> Handle(UpdateIngredientCommand request, CancellationToken cancellationToken)
        {
            var ingredientToUpdate = await _ingredientRepository.GetById(request.Id, cancellationToken: cancellationToken);

            ingredientToUpdate.Update(request.IngredientToUpdate);
            await _unitOfWork.CommitChanges(cancellationToken);

            return true;
        }
    }
}