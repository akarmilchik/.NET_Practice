using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SalesStatistics.Core.Queries;
using SalesStatistics.Core.Services;
using SalesStatistics.DAL;
using SalesStatistics.DAL.Models;
using SalesStatistics.Mapper;
using System;
using System.IO;
using System.Reflection;
using System.Text.Json.Serialization;

namespace SalesStatistics
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();

            services.AddLocalization(opts =>
            {
                opts.ResourcesPath = "Resources";
            });

            services.AddMvc()
                .AddFluentValidation(f => f.RegisterValidatorsFromAssemblyContaining<Startup>())
                .AddXmlDataContractSerializerFormatters()
                .AddJsonOptions(opts => opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()))
                .AddRazorRuntimeCompilation();

            services.AddScoped<IProductsService, ProductsService>();
            services.AddScoped<IOrdersService, OrdersService>();
            services.AddScoped<IClientsService, ClientsService>();

            services.AddDbContext<DataContext>(o =>
            {
                o.UseSqlServer(Configuration.GetConnectionString("BaseConnection"))
                    .EnableSensitiveDataLogging();
            });

            services.AddDefaultIdentity<User>().AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<DataContext>();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 3;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });

            services.Configure<AntiforgeryOptions>(opts =>
            {
                opts.FormFieldName = "StoreSecretInput";
                opts.HeaderName = "X-CSRF-TOKEN";
                opts.SuppressXFrameOptionsHeader = false;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            services.AddSwaggerGen(c => {
                var file = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var path = Path.Combine(AppContext.BaseDirectory, file);
                c.IncludeXmlComments(path);
            });

            services.AddAutoMapper(typeof(MappingProfile));

            services.Scan(scan => scan
                .FromAssemblyOf<BaseQuery>()
                .AddClasses(c => c.AssignableTo(typeof(ISortingProvider<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            var supportedLocales = new[] { "en-US", "ru-RU", "be-BY" };

            var localizationOptions = new RequestLocalizationOptions().
                           SetDefaultCulture(supportedLocales[0])
                           .AddSupportedCultures(supportedLocales)
                           .AddSupportedUICultures(supportedLocales);

            app.UseRequestLocalization(localizationOptions);

            app.UseStaticFiles();

            app.UseSwagger();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SalesStatistics API v1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllers();
            });
        }
    }
}
