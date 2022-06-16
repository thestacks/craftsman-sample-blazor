using System.Net;
using Flurl;
using Fluxor;
using RecipeManagement.Contracts;
using RecipeManagement.Contracts.Recipes;
using RecipeManagement.Contracts.Routing;
using RecipeManagement.UI.Extensions;
using RecipeManagement.UI.Services.Meta;

namespace RecipeManagement.UI.Store.Recipes.RecipesList;

public class RecipesListEffects
{
    private readonly IBackendConnectorService _backendConnectorService;

    public RecipesListEffects(IBackendConnectorService backendConnectorService)
    {
        _backendConnectorService = backendConnectorService;
    }

    [EffectMethod]
    public async Task Load(RecipesListLoadAction action, IDispatcher dispatcher)
    {
        try
        {
            var pagination = action.Parameters.GetPagination();
            var sortsField = action.Parameters.GetSortsField();
            var uri = BackendRoutes.Recipes
                .SetQueryParam(nameof(RecipeParametersDto.PageSize), pagination.pageSize)
                .SetQueryParam(nameof(RecipeParametersDto.PageNumber), pagination.page);
            if (!string.IsNullOrEmpty(sortsField))
                uri = uri.SetQueryParam(nameof(RecipeParametersDto.SortOrder), sortsField);
            Console.WriteLine(sortsField);

            var data = await _backendConnectorService.SendQueryAsync<RecipeDto>(uri);
            if (data == null)
                throw new HttpRequestException();

            dispatcher.Dispatch(new RecipesListSetDataAction(data));
        }
        catch
        {
            dispatcher.Dispatch(new RecipesListSetDataAction(new PagedList<RecipeDto>()));
        }
    }

    [EffectMethod]
    public async Task Delete(RecipesListDeleteAction action, IDispatcher dispatcher)
    {
        try
        {
            var uri = BackendRoutes.Recipes.AppendPathSegment(action.Id);
            var statusCode = await _backendConnectorService.SendAndGetStatusCodeAsync(HttpMethod.Delete, uri);
            if (statusCode != HttpStatusCode.NoContent)
                throw new HttpRequestException();
            
            dispatcher.Dispatch(new RecipesListDeleteSucceededAction());
        }
        catch
        {
            dispatcher.Dispatch(new RecipesListDeleteFailedAction());
        }
    }
}