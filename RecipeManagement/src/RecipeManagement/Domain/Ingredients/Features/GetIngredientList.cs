namespace RecipeManagement.Domain.Ingredients.Features;

using RecipeManagement.Domain.Ingredients.Dtos;
using RecipeManagement.Domain.Ingredients.Services;
using RecipeManagement.Wrappers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Sieve.Models;
using Sieve.Services;

public static class GetIngredientList
{
    public class IngredientListQuery : IRequest<PagedList<IngredientDto>>
    {
        public readonly IngredientParametersDto QueryParameters;

        public IngredientListQuery(IngredientParametersDto queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }

    public class Handler : IRequestHandler<IngredientListQuery, PagedList<IngredientDto>>
    {
        private readonly IIngredientRepository _ingredientRepository;
        private readonly SieveProcessor _sieveProcessor;
        private readonly IMapper _mapper;

        public Handler(IIngredientRepository ingredientRepository, IMapper mapper, SieveProcessor sieveProcessor)
        {
            _mapper = mapper;
            _ingredientRepository = ingredientRepository;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<PagedList<IngredientDto>> Handle(IngredientListQuery request, CancellationToken cancellationToken)
        {
            var collection = _ingredientRepository.Query();

            var sieveModel = new SieveModel
            {
                Sorts = request.QueryParameters.SortOrder ?? "Id",
                Filters = request.QueryParameters.Filters
            };

            var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
            var dtoCollection = appliedCollection
                .ProjectTo<IngredientDto>(_mapper.ConfigurationProvider);

            return await PagedList<IngredientDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);
        }
    }
}