using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.DependencyResolvers.Autofac;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using System.Resources;

namespace JsonParsingWithLocalization
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        // Set default IoC as Autofac
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args) // Server configuration
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureServices(serviceCollection =>
             {
                 serviceCollection.AddSingleton(new ResourceManager("JsonParsingWithLocalization.Resources.Controllers.HistoriesController",
                                                typeof(Startup).GetTypeInfo().Assembly));
             })
            .ConfigureContainer<ContainerBuilder>(builder =>
            {
                builder.RegisterModule(new AutofacBusinessModule());                
                builder.RegisterModule(new AutofacAutoMapperModule());
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
    }
}