using Castle.DynamicProxy;
using System;
using System.Reflection;
using System.Linq;

namespace Core.Utilities.Interceptors
{
    public class AspectInterceptorSelector : IInterceptorSelector
    { // Aspect Interceptor selector
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            // Find class' attributes
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>
                (true).ToList();

            // Find method's attribute
            var methodAttributes = type.GetMethod(method.Name)
                .GetCustomAttributes<MethodInterceptionBaseAttribute>(true);

            // Put the class' attributes into a list
            classAttributes.AddRange(methodAttributes);

            // At runtime, sort class' attributes about Priority
            return (IInterceptor[])classAttributes.OrderBy(x => x.Priority).ToArray();
        }
    }
}