using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using Blazor.Server.WebApi.Filters;
using Blazor.Server.WebApi.Interfaces;
using Blazor.Server.WebApi.Services;
using Blazor.Server.WebApi.Settings;
using Blazor.DataAccessLayer.Data;
using Blazor.BusinessLayer.Interfaces;
using Blazor.DataAccessLayer.Data.Repositories;

namespace Blazor.Server.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<CatalogContext>(c =>
                {
                    c.UseSqlServer(connection);
                    c.UseLazyLoadingProxies();
                });
            services.Configure<CacheOptionsSettings>(Configuration.GetSection("CacheSettings"));

            services.AddAutoMapper(typeof(Startup));

            services.AddScoped<ITorrentsRepository, TorrentsRepository>();

            services.AddScoped<ITorrentsViewModelService, CachedTorrentsViewModelService>();
            services.AddScoped<TorrentsViewModelService>();

            services.AddMvc(options => options.Filters.Add<ApiExceptionFilterAttribute>());

            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBlazorDebugging();
            }

            app.UseClientSideBlazorFiles<Client.Startup>();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapFallbackToClientSideBlazor<Client.Startup>("index.html");
            });
        }
    }
}
