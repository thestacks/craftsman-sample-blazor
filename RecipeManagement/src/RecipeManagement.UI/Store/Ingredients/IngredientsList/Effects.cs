using System.Net;
using Flurl;
using Fluxor;
using RecipeManagement.Contracts;
using RecipeManagement.Contracts.Ingredients;
using RecipeManagement.Contracts.Routing;
using RecipeManagement.UI.Extensions;
using RecipeManagement.UI.Services.Meta;

namespace RecipeManagement.UI.Store.Ingredients.IngredientsList;

public class IngredientsListEffects
{
    private readonly IBackendConnectorService _backendConnectorService;

    public IngredientsListEffects(IBackendConnectorService backendConnectorService)
    {
        _backendConnectorService = backendConnectorService;
    }

    [EffectMethod]
    public async Task Load(IngredientsListLoadAction action, IDispatcher dispatcher)
    {
        try
        {
            var pagination = action.Parameters.GetPagination();
            var sortsField = action.Parameters.GetSortsField();
            var uri = BackendRoutes.Ingredients
                .SetQueryParam(nameof(IngredientParametersDto.PageSize), pagination.pageSize)
                .SetQueryParam(nameof(IngredientParametersDto.PageNumber), pagination.page);
            if (!string.IsNullOrEmpty(sortsField))
                uri = uri.SetQueryParam(nameof(IngredientParametersDto.SortOrder), sortsField);
            Console.WriteLine(sortsField);

            var data = await _backendConnectorService.SendQueryAsync<IngredientDto>(uri);
            if (data == null)
                throw new HttpRequestException();

            dispatcher.Dispatch(new IngredientsListSetDataAction(data));
        }
        catch
        {
            dispatcher.Dispatch(new IngredientsListSetDataAction(new PagedList<IngredientDto>()));
        }
    }

    [EffectMethod]
    public async Task Delete(IngredientsListDeleteAction action, IDispatcher dispatcher)
    {
        try
        {
            var uri = BackendRoutes.Ingredients.AppendPathSegment(action.Id);
            var statusCode = await _backendConnectorService.SendAndGetStatusCodeAsync(HttpMethod.Delete, uri);
            if (statusCode != HttpStatusCode.NoContent)
                throw new HttpRequestException();
            
            dispatcher.Dispatch(new IngredientsListDeleteSucceededAction());
        }
        catch
        {
            dispatcher.Dispatch(new IngredientsListDeleteFailedAction());
        }
    }
}