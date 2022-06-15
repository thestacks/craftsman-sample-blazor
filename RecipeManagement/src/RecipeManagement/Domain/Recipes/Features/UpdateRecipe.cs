namespace RecipeManagement.Domain.Recipes.Features;

using RecipeManagement.Domain.Recipes;
using RecipeManagement.Domain.Recipes.Dtos;
using SharedKernel.Exceptions;
using RecipeManagement.Domain.Recipes.Validators;
using RecipeManagement.Domain.Recipes.Services;
using RecipeManagement.Services;
using AutoMapper;
using MediatR;

public static class UpdateRecipe
{
    public class UpdateRecipeCommand : IRequest<bool>
    {
        public readonly Guid Id;
        public readonly RecipeForUpdateDto RecipeToUpdate;

        public UpdateRecipeCommand(Guid recipe, RecipeForUpdateDto newRecipeData)
        {
            Id = recipe;
            RecipeToUpdate = newRecipeData;
        }
    }

    public class Handler : IRequestHandler<UpdateRecipeCommand, bool>
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IRecipeRepository recipeRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _recipeRepository = recipeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateRecipeCommand request, CancellationToken cancellationToken)
        {
            var recipeToUpdate = await _recipeRepository.GetById(request.Id, cancellationToken: cancellationToken);

            recipeToUpdate.Update(request.RecipeToUpdate);
            await _unitOfWork.CommitChanges(cancellationToken);

            return true;
        }
    }
}