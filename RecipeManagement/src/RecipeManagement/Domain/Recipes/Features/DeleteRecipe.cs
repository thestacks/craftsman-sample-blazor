namespace RecipeManagement.Domain.Recipes.Features;

using RecipeManagement.Domain.Recipes.Services;
using RecipeManagement.Services;
using MediatR;

public static class DeleteRecipe
{
    public class DeleteRecipeCommand : IRequest<bool>
    {
        public readonly Guid Id;

        public DeleteRecipeCommand(Guid recipe)
        {
            Id = recipe;
        }
    }

    public class Handler : IRequestHandler<DeleteRecipeCommand, bool>
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IRecipeRepository recipeRepository, IUnitOfWork unitOfWork)
        {
            _recipeRepository = recipeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteRecipeCommand request, CancellationToken cancellationToken)
        {
            var recordToDelete = await _recipeRepository.GetById(request.Id, cancellationToken: cancellationToken);

            _recipeRepository.Remove(recordToDelete);
            await _unitOfWork.CommitChanges(cancellationToken);
            return true;
        }
    }
}