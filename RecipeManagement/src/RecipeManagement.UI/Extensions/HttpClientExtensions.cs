using System.Net.Http.Json;
using System.Text.Json;

namespace RecipeManagement.UI.Extensions;

public static class HttpClientExtensions
{
    public static JsonSerializerOptions Options => new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
    
    public static Task<T>
        ReadJsonAsync<T>(this HttpContent content, CancellationToken cancellationToken = default) =>
        content.ReadFromJsonAsync<T>(Options, cancellationToken);
}