using System.Net;
using Flurl;
using Fluxor;
using RecipeManagement.Contracts.Recipes;
using RecipeManagement.Contracts.Routing;
using RecipeManagement.UI.Services.Meta;
using FormModel = RecipeManagement.UI.Components.Recipes.RecipeForm.RecipeFormModel;

namespace RecipeManagement.UI.Store.Recipes.RecipeForm;

public class RecipeFormEffects
{
    private readonly IBackendConnectorService _backendConnectorService;

    public RecipeFormEffects(IBackendConnectorService backendConnectorService)
    {
        _backendConnectorService = backendConnectorService;
    }

    [EffectMethod]
    public async Task Load(RecipeFormLoadDataAction action, IDispatcher dispatcher)
    {
        try
        {
            var uri = BackendRoutes.Recipes.AppendPathSegment(action.RecipeId);
            var response = await _backendConnectorService.SendAsync<RecipeDto>(HttpMethod.Get, uri);
            if (response == null)
                throw new HttpRequestException();

            var model = new FormModel
            {
                Id = response.Id,
                Title = response.Title,
                Directions = response.Directions,
                RecipeSourceLink = response.RecipeSourceLink,
                Description = response.Description,
                ImageLink = response.ImageLink
            };
            dispatcher.Dispatch(new RecipeFormSetDataAction(model));
        }
        catch
        {
            dispatcher.Dispatch(new RecipeFormLoadDataFailedAction());
        }
    }

    [EffectMethod]
    public async Task Submit(RecipeFormSubmitAction action, IDispatcher dispatcher)
    {
        try
        {
            var isEdit = action.Model.Id.HasValue;
            var requestBody = PrepareSubmitRequestModel(isEdit, action.Model);
            var uri = BackendRoutes.Recipes;
            if (isEdit)
                uri = uri.AppendPathSegment(action.Model.Id!.Value);
            
            var responseStatusCode =
                await _backendConnectorService.SendAndGetStatusCodeAsync(isEdit ? HttpMethod.Put : HttpMethod.Post, uri,
                    requestBody);
            if (!IsValidResponseStatusCode(isEdit, responseStatusCode))
                throw new HttpRequestException();
            
            dispatcher.Dispatch(new RecipeFormSubmitSucceededAction());
        }
        catch
        {
            dispatcher.Dispatch(new RecipeFormSubmitFailedAction());
        }
    }

    private RecipeForManipulationDto PrepareSubmitRequestModel(bool isEdit, FormModel model) =>
        isEdit ? new RecipeForUpdateDto
        {
            Title = model.Title,
            Directions = model.Directions,
            RecipeSourceLink = model.RecipeSourceLink,
            Description = model.Description,
            ImageLink = model.ImageLink
        }: new RecipeForCreationDto
        {
            Title = model.Title,
            Directions = model.Directions,
            RecipeSourceLink = model.RecipeSourceLink,
            Description = model.Description,
            ImageLink = model.ImageLink
        };

    private static bool IsValidResponseStatusCode(bool isEdit, HttpStatusCode? statusCode)
    {
        if (statusCode == null)
            return false;

        if (isEdit && statusCode.Value == HttpStatusCode.NoContent)
            return true;

        return !isEdit && statusCode.Value == HttpStatusCode.Created;
    }
}