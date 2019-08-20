using System.Threading.Tasks;
using Blazor.Server.DataAccessLayer.Context;
using Blazor.Tests.Helpers;


namespace Blazor.Tests.IntegrationTests.Blazor.Server.WebApi.Helpers
{
    public class Utilities
    {
        public static async Task InitializeDbForTests(TorrentsContext context)
        {
            await context.Forums.AddRangeAsync(InitialEntities.Forums);
            await context.Torrents.AddRangeAsync(InitialEntities.Torrents);
            await context.Files.AddRangeAsync(InitialEntities.Files);

            await context.SaveChangesAsync();
        }
    }
}
