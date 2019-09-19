using Blazor.FileReader;
using Blazor.Frontend.BusinessLayer.Provider;
using Blazor.Frontend.BusinessLayer.Services.AuthService;
using Blazor.Frontend.BusinessLayer.Services.TorrentsService;
using Blazored.LocalStorage;
using Blazored.Modal;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;


namespace Blazor.Frontend.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddBlazoredLocalStorage();
            services.AddAuthorizationCore();
            services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITorrentsService, TorrentsService>();
            services.AddBlazoredModal();
            services.AddFileReaderService(options => options.UseWasmSharedBuffer = true);
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
