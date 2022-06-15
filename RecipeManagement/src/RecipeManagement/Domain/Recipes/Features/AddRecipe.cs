namespace RecipeManagement.Domain.Recipes.Features;

using RecipeManagement.Domain.Recipes.Services;
using RecipeManagement.Domain.Recipes;
using RecipeManagement.Domain.Recipes.Dtos;
using RecipeManagement.Services;
using AutoMapper;
using MediatR;

public static class AddRecipe
{
    public class AddRecipeCommand : IRequest<RecipeDto>
    {
        public readonly RecipeForCreationDto RecipeToAdd;

        public AddRecipeCommand(RecipeForCreationDto recipeToAdd)
        {
            RecipeToAdd = recipeToAdd;
        }
    }

    public class Handler : IRequestHandler<AddRecipeCommand, RecipeDto>
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

        public async Task<RecipeDto> Handle(AddRecipeCommand request, CancellationToken cancellationToken)
        {
            var recipe = Recipe.Create(request.RecipeToAdd);
            await _recipeRepository.Add(recipe, cancellationToken);

            await _unitOfWork.CommitChanges(cancellationToken);

            var recipeAdded = await _recipeRepository.GetById(recipe.Id, cancellationToken: cancellationToken);
            return _mapper.Map<RecipeDto>(recipeAdded);
        }
    }
}