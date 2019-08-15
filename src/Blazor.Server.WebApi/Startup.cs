using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using Blazor.Server.BusinessLayer.Services.TorrentsService;
using Blazor.Server.BusinessLayer.Settings;
using Blazor.Server.DataAccessLayer.Data.Context;
using Blazor.Server.WebApi.Filters;
using Blazor.Server.DataAccessLayer.Data.Repositories;

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
            services.AddDbContext<TorrentsContext>(c =>
                {
                    c.UseSqlServer(connection);
                    //c.UseLazyLoadingProxies();
                });

            services.Configure<CacheOptionsSettings>(Configuration.GetSection("CacheSettings"));

            services.AddAutoMapper(typeof(Startup));

            services.AddScoped<ITorrentsRepository, TorrentsRepository>();
            services.AddScoped<ITorrentsService, TorrentsServiceCacheDecorator>();
            services.AddScoped<TorrentsService>();

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

            app.UseClientSideBlazorFiles<Frontend.Client.Startup>();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapFallbackToClientSideBlazor<Frontend.Client.Startup>("index.html");
            });
        }
    }
}
