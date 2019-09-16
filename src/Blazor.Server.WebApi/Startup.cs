using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using System.Text;
using Blazor.Server.BusinessLayer.Services.AccountService;
using Blazor.Server.BusinessLayer.Services.BlobContainerService;
using Blazor.Server.BusinessLayer.Services.JwtTokenService;
using Blazor.Server.BusinessLayer.Services.TorrentsService;
using Blazor.Server.BusinessLayer.Settings;
using Blazor.Server.DataAccessLayer.Context.Identity;
using Blazor.Server.DataAccessLayer.Context.Torrents;
using Blazor.Server.DataAccessLayer.Entities;
using Blazor.Server.WebApi.Filters;
using Blazor.Server.DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc.Formatters;



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
            services.AddDbContext<TorrentsContext>(c =>
                c.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<IdentityContext>(optionsAction: options =>
                options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));


            services.AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["JWTSettings:Issuer"],
                        ValidAudience = Configuration["JWTSettings:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWTSettings:SecurityKey"]))
                    });

            services.Configure<CacheOptionsSettings>(Configuration.GetSection("CacheSettings"));
            services.Configure<TokenManagerSettings>(Configuration.GetSection("JWTSettings"));
            services.Configure<BlobContainerSettings>(Configuration.GetSection("BlobContainerSettings"));

            services.AddAutoMapper(typeof(Startup));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IBlobContainerService, BlobContainerService>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ITorrentsService, TorrentsServiceCacheDecorator>();
            services.AddScoped<TorrentsService>();


            services.AddMvc(options =>
                {
                    options.Filters.Add<ApiExceptionFilterAttribute>();
                    options.OutputFormatters.RemoveType<StringOutputFormatter>();
                }
                );

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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapFallbackToClientSideBlazor<Frontend.Client.Startup>("index.html");
            });
        }
    }
}
