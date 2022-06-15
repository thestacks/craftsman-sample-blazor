namespace RecipeManagement.Domain.Recipes.Features;

using RecipeManagement.Domain.Recipes.Dtos;
using RecipeManagement.Domain.Recipes.Services;
using AutoMapper;
using MediatR;

public static class GetRecipe
{
    public class RecipeQuery : IRequest<RecipeDto>
    {
        public readonly Guid Id;

        public RecipeQuery(Guid id)
        {
            Id = id;
        }
    }

    public class Handler : IRequestHandler<RecipeQuery, RecipeDto>
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IMapper _mapper;

        public Handler(IRecipeRepository recipeRepository, IMapper mapper)
        {
            _mapper = mapper;
            _recipeRepository = recipeRepository;
        }

        public async Task<RecipeDto> Handle(RecipeQuery request, CancellationToken cancellationToken)
        {
            var result = await _recipeRepository.GetById(request.Id, cancellationToken: cancellationToken);
            return _mapper.Map<RecipeDto>(result);
        }
    }
}