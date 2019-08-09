using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Blazor.Infrastructure.Data;
using Tests.Shared;

namespace SolutionApp.IntegrationTests.Helpers
{
    public class Utilities
    {
        public static async Task InitializeDbForTests(CatalogContext context)
        {
            await context.Forums.AddRangeAsync(InitialEntities.Forums);
            await context.Torrents.AddRangeAsync(InitialEntities.Torrents);
            await context.Files.AddRangeAsync(InitialEntities.Files);

            await context.SaveChangesAsync();
        }
    }
}
