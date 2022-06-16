using RecipeManagement.Contracts.Ingredients;

namespace RecipeManagement.Domain.Ingredients.Features;

using RecipeManagement.Domain.Ingredients.Services;
using AutoMapper;
using MediatR;

public static class GetIngredient
{
    public class IngredientQuery : IRequest<IngredientDto>
    {
        public readonly Guid Id;

        public IngredientQuery(Guid id)
        {
            Id = id;
        }
    }

    public class Handler : IRequestHandler<IngredientQuery, IngredientDto>
    {
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IMapper _mapper;

        public Handler(IIngredientRepository ingredientRepository, IMapper mapper)
        {
            _mapper = mapper;
            _ingredientRepository = ingredientRepository;
        }

        public async Task<IngredientDto> Handle(IngredientQuery request, CancellationToken cancellationToken)
        {
            var result = await _ingredientRepository.GetById(request.Id, cancellationToken: cancellationToken);
            return _mapper.Map<IngredientDto>(result);
        }
    }
}