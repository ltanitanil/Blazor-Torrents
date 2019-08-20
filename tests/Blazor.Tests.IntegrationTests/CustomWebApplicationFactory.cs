using Blazor.Server.DataAccessLayer.Context;
using Blazor.Server.WebApi;
using Blazor.Tests.IntegrationTests.Blazor.Server.WebApi.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Blazor.Tests.IntegrationTests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var provider = services.AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                services.AddEntityFrameworkInMemoryDatabase().AddDbContext<TorrentsContext>((options) =>
                    {
                        options.UseInMemoryDatabase("InMemoryDbForTesting");
                        options.UseInternalServiceProvider(provider);
                    });

                using var scope = services.BuildServiceProvider().CreateScope();

                var db = scope.ServiceProvider.GetRequiredService<TorrentsContext>();

                if (db.Database.EnsureCreated())
                    Utilities.InitializeDbForTests(db).Wait(); // Seed the database with test data.
            });
        }
    }
}
