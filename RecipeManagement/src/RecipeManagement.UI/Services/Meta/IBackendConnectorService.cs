using System.Net;
using RecipeManagement.Contracts;

namespace RecipeManagement.UI.Services.Meta;

public interface IBackendConnectorService
{
    string GetBaseUrl();
    Task<HttpStatusCode?> SendAndGetStatusCodeAsync(HttpMethod method, string uri);
    Task<HttpStatusCode?> SendAndGetStatusCodeAsync(HttpMethod method, string uri, object? body);
    Task<T?> SendAsync<T>(HttpMethod method, string uri);
    Task<T?> SendAsync<T>(HttpMethod method, string uri, object? body);
    Task<PagedList<T>> SendQueryAsync<T>(string uri);
}