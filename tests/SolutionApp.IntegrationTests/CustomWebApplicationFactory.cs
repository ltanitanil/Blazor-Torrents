using Blazor.Server.DataAccessLayer.Data;
using Blazor.Server.WebApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SolutionApp.IntegrationTests.Blazor.Server.WebApi.Helpers;

namespace SolutionApp.IntegrationTests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var provider = services.AddEntityFrameworkInMemoryDatabase()
                                       .AddEntityFrameworkProxies()
                                       .BuildServiceProvider();

                services.AddDbContext<CatalogContext>((options) =>
                    {
                        options.UseInMemoryDatabase("InMemoryDbForTesting");
                        options.UseLazyLoadingProxies();
                        options.UseInternalServiceProvider(provider);
                    });

                using var scope = services.BuildServiceProvider().CreateScope();

                var db = scope.ServiceProvider.GetRequiredService<CatalogContext>();

                if (db.Database.EnsureCreated())
                    Utilities.InitializeDbForTests(db).Wait(); // Seed the database with test data.
            });
        }
    }
}
