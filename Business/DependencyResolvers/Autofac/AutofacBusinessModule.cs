using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Http;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    { // Autofac dependency injection module
        protected override void Load(ContainerBuilder builder)
        { // Ioc container
            // Business injection
            builder.RegisterType<HistoryManager>().As<IHistoryService>().SingleInstance();

            // DataAccess injection
            builder.RegisterType<EfHistoryDal>().As<IHistoryDal>().SingleInstance();

            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly(); // In the running app
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces() // find the implemented interfaces
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector() // call AspectInterceptorSelector for them
                }).SingleInstance();
        }
    }
}