namespace RecipeManagement.Domain.Ingredients.Features;

using RecipeManagement.Domain.Ingredients.Services;
using RecipeManagement.Services;
using MediatR;

public static class DeleteIngredient
{
    public class DeleteIngredientCommand : IRequest<bool>
    {
        public readonly Guid Id;

        public DeleteIngredientCommand(Guid ingredient)
        {
            Id = ingredient;
        }
    }

    public class Handler : IRequestHandler<DeleteIngredientCommand, bool>
    {
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IIngredientRepository ingredientRepository, IUnitOfWork unitOfWork)
        {
            _ingredientRepository = ingredientRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteIngredientCommand request, CancellationToken cancellationToken)
        {
            var recordToDelete = await _ingredientRepository.GetById(request.Id, cancellationToken: cancellationToken);

            _ingredientRepository.Remove(recordToDelete);
            await _unitOfWork.CommitChanges(cancellationToken);
            return true;
        }
    }
}