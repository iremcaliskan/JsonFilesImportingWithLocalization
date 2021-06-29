using System;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Utilities.IoC
{ // Inversion of Control
    public static class ServiceTool
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public static IServiceCollection Create(IServiceCollection services)
        {
            // Take the services and build them, that provides Injections
            ServiceProvider = services.BuildServiceProvider();
            return services;
        }
    }
}