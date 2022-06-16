using System.Net;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using RecipeManagement.Contracts;
using RecipeManagement.UI.Extensions;
using RecipeManagement.UI.Models;
using RecipeManagement.UI.Services.Meta;

namespace RecipeManagement.UI.Services;

public class HttpClientBackendConnectorService : IBackendConnectorService
{
    private readonly HttpClient _httpClient;

    public HttpClientBackendConnectorService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public string GetBaseUrl() => _httpClient.BaseAddress!.ToString();

    public Task<T?> SendAsync<T>(HttpMethod method, string uri) => SendAsync<T>(method, uri, null);

    public async Task<T?> SendAsync<T>(HttpMethod method, string uri, object? body)
    {
        try
        {
            var response = await SendAsync(method, uri, body);
            if (!response.IsSuccessStatusCode)
                return default;
            
            return await response.Content.ReadJsonAsync<T?>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occured: {ex.Message}");
            return default;
        }
    }

    public Task<HttpStatusCode?> SendAndGetStatusCodeAsync(HttpMethod method, string uri) =>
        SendAndGetStatusCodeAsync(method, uri, null);

    public async Task<HttpStatusCode?> SendAndGetStatusCodeAsync(HttpMethod method, string uri, object? body)
    {
        try
        {
            var response = await SendAsync(method, uri, body);
            return response?.StatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occured: {ex.Message}");
            return null;
        }
    }

    public async Task<PagedList<T>> SendQueryAsync<T>(string uri)
    {
        try
        {
            var response = await SendAsync(HttpMethod.Get, uri, null);
            
            var paginationDetails =
                JsonSerializer.Deserialize<PaginationHeaderDto>(response.Headers
                    .Single(h => h.Key == "x-pagination").Value.Single(), HttpClientExtensions.Options);
            var items = await response.Content.ReadJsonAsync<List<T>>();
            
            return new PagedList<T>(items, paginationDetails!.TotalCount, paginationDetails.PageNumber,
                paginationDetails.PageSize);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }
    
    private async Task<HttpResponseMessage?> SendAsync(HttpMethod method, string uri, object? body)
    {
        try
        {
            var requestMessage = new HttpRequestMessage
            {
                Method = method,
                RequestUri = BuildUri(uri)
            };
            if (body != null)
                requestMessage.Content = new StringContent(JsonSerializer.Serialize(body, HttpClientExtensions.Options), Encoding.UTF8,
                    MediaTypeNames.Application.Json);

            var response = await _httpClient.SendAsync(requestMessage);
            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occured: {ex.Message}");
            return null;
        }
    }

    private Uri BuildUri(string path) => new(_httpClient.BaseAddress!, path);
}