using System.Reflection;
using System.Text.Json;
using Blazored.LocalStorage;
using Fluxor;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;
using RecipeManagement.UI;
using RecipeManagement.UI.Services;
using RecipeManagement.UI.Services.Meta;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(_ => new HttpClient {BaseAddress = new Uri("https://localhost:5005")});
builder.Services.AddScoped<IBackendConnectorService, HttpClientBackendConnectorService>();
builder.Services.AddBlazoredLocalStorage(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});
builder.Services
    .AddScoped<DialogService>()
    .AddScoped<NotificationService>()
    .AddScoped<TooltipService>()
    .AddScoped<ContextMenuService>();
builder.Services.AddFluxor(options =>
{
    options.ScanAssemblies(Assembly.GetAssembly(typeof(Program)));
    options.UseRouting();
    options.UseReduxDevTools();
});
await builder.Build().RunAsync();