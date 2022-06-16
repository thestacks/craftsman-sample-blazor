
namespace RecipeManagement.FunctionalTests;

using RecipeManagement.Databases;
using RecipeManagement.Resources;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class TestingWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.UseEnvironment(LocalConfig.FunctionalTestingEnvName);

        builder.ConfigureServices(services =>
        {
            // Create a new service provider.
            var provider = services.BuildServiceProvider();

            // Add a database context (RecipesDbContext) using an in-memory database for testing.
            services.AddDbContext<RecipesDbContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDbForTesting");
                options.UseInternalServiceProvider(provider);
            });

            // Build the service provider.
            var sp = services.BuildServiceProvider();

            // Create a scope to obtain a reference to the database context (RecipesDbContext).
            using (var scope = sp.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<RecipesDbContext>();

                // Ensure the database is created.
                db.Database.EnsureCreated();
            }
        });
        
        return base.CreateHost(builder);
    }
}