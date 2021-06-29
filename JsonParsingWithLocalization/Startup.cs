using Core.DependencyResolvers;
using Core.Extensions;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Globalization;

namespace JsonParsingWithLocalization
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLocalization(opts => { opts.ResourcesPath = "Resources"; });

            services.AddMvc()
                    // Add support for finding localized views, based on file name suffix, e.g. Index.tr.cshtml
                    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                    // Add support for localizing strings in data annotations (e.g. validation messages) by the IStringLocalizer abstractions
                    .AddDataAnnotationsLocalization();

            services.Configure<RequestLocalizationOptions>(
                opt =>
                {
                    var supportedCultures = new List<CultureInfo>()
                    {
                        new CultureInfo("en"),
                        new CultureInfo("tr"),
                        new CultureInfo("it")
                    };
                    opt.DefaultRequestCulture = new RequestCulture("en");
                    // Formatting numbers, dates, etc.
                    opt.SupportedCultures = supportedCultures;
                    // UI strings that we have localized.
                    opt.SupportedUICultures = supportedCultures;
                });

            services.AddControllersWithViews();

            // Every request is percieved a threat by the system come to API. To allow them:
            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin", 
                    builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            // The AddDependendyResolvers structure was established for the CoreModule added here and the modules to be added in the future too
            services.AddDependencyResolvers(new ICoreModule[] { // params can be used too
                new CoreModule()
            });
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
                app.UseExceptionHandler("/Histories/Error");
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseRouting();

            app.UseAuthorization();

            app.UseRequestLocalization(app.ApplicationServices.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Histories}/{action=Index}/{id?}");
            });
        }
    }
}