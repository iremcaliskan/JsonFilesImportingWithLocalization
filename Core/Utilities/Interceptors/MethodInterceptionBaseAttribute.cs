using Castle.DynamicProxy;
using System;

namespace Core.Utilities.Interceptors
{
    // This Attribute can be added to classes and methods, can be multiple also, can be inherited too
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public abstract class MethodInterceptionBaseAttribute : Attribute, IInterceptor
    { // IInterceptor comes from Autofac
        public int Priority { get; set; } // Priority, Which attribute runs first? 

        public virtual void Intercept(IInvocation invocation) // IInvocation comes from Autofac
        {
            // Implementable method, overridable
        }
    }
}