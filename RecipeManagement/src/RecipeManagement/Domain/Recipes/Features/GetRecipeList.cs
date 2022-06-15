namespace RecipeManagement.Domain.Recipes.Features;

using RecipeManagement.Domain.Recipes.Dtos;
using RecipeManagement.Domain.Recipes.Services;
using RecipeManagement.Wrappers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Sieve.Models;
using Sieve.Services;

public static class GetRecipeList
{
    public class RecipeListQuery : IRequest<PagedList<RecipeDto>>
    {
        public readonly RecipeParametersDto QueryParameters;

        public RecipeListQuery(RecipeParametersDto queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }

    public class Handler : IRequestHandler<RecipeListQuery, PagedList<RecipeDto>>
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly SieveProcessor _sieveProcessor;
        private readonly IMapper _mapper;

        public Handler(IRecipeRepository recipeRepository, IMapper mapper, SieveProcessor sieveProcessor)
        {
            _mapper = mapper;
            _recipeRepository = recipeRepository;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<PagedList<RecipeDto>> Handle(RecipeListQuery request, CancellationToken cancellationToken)
        {
            var collection = _recipeRepository.Query();

            var sieveModel = new SieveModel
            {
                Sorts = request.QueryParameters.SortOrder ?? "Id",
                Filters = request.QueryParameters.Filters
            };

            var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
            var dtoCollection = appliedCollection
                .ProjectTo<RecipeDto>(_mapper.ConfigurationProvider);

            return await PagedList<RecipeDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);
        }
    }
}