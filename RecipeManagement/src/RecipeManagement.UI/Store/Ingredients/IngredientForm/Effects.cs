using System.Net;
using Flurl;
using Fluxor;
using RecipeManagement.Contracts.Ingredients;
using RecipeManagement.Contracts.Routing;
using RecipeManagement.UI.Services.Meta;
using FormModel = RecipeManagement.UI.Components.Ingredients.IngredientForm.IngredientFormModel;

namespace RecipeManagement.UI.Store.Ingredients.IngredientForm;

public class IngredientFormEffects
{
    private readonly IBackendConnectorService _backendConnectorService;

    public IngredientFormEffects(IBackendConnectorService backendConnectorService)
    {
        _backendConnectorService = backendConnectorService;
    }

    [EffectMethod]
    public async Task Load(IngredientFormLoadDataAction action, IDispatcher dispatcher)
    {
        try
        {
            var uri = BackendRoutes.Ingredients.AppendPathSegment(action.IngredientId);
            var response = await _backendConnectorService.SendAsync<IngredientDto>(HttpMethod.Get, uri);
            if (response == null)
                throw new HttpRequestException();

            var model = new FormModel
            {
                Id = response.Id,
                RecipeId = response.RecipeId,
                Name = response.Name,
                Unit = response.Unit,
                Amount = response.Amount
            };
            dispatcher.Dispatch(new IngredientFormSetDataAction(model));
        }
        catch
        {
            dispatcher.Dispatch(new IngredientFormLoadDataFailedAction());
        }
    }

    [EffectMethod]
    public async Task Submit(IngredientFormSubmitAction action, IDispatcher dispatcher)
    {
        try
        {
            var isEdit = action.Model.Id.HasValue;
            var requestBody = PrepareSubmitRequestModel(isEdit, action.Model);
            var uri = BackendRoutes.Ingredients;
            if (isEdit)
                uri = uri.AppendPathSegment(action.Model.Id!.Value);
            
            var responseStatusCode =
                await _backendConnectorService.SendAndGetStatusCodeAsync(isEdit ? HttpMethod.Put : HttpMethod.Post, uri,
                    requestBody);
            if (!IsValidResponseStatusCode(isEdit, responseStatusCode))
                throw new HttpRequestException();
            
            dispatcher.Dispatch(new IngredientFormSubmitSucceededAction());
        }
        catch
        {
            dispatcher.Dispatch(new IngredientFormSubmitFailedAction());
        }
    }

    private IngredientForManipulationDto PrepareSubmitRequestModel(bool isEdit, FormModel model) =>
        isEdit ? new IngredientForUpdateDto
        {
            RecipeId = model.RecipeId,
            Name = model.Name,
            Unit = model.Unit,
            Amount = model.Amount
        }: new IngredientForCreationDto
        {
            RecipeId = model.RecipeId,
            Name = model.Name,
            Unit = model.Unit,
            Amount = model.Amount
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