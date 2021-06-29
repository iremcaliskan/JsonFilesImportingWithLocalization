using Castle.DynamicProxy;
using System;

namespace Core.Utilities.Interceptors
{
    public class MethodInterception : MethodInterceptionBaseAttribute
    {
        // Empty methods:
        protected virtual void OnBefore(IInvocation invocation) { }
        protected virtual void OnAfter(IInvocation invocation) { }
        protected virtual void OnException(IInvocation invocation, Exception e) { }
        protected virtual void OnSuccess(IInvocation invocation) { }

        public override void Intercept(IInvocation invocation)
        {
            var isSuccess = true;
            OnBefore(invocation); // Before the method
            try
            {
                invocation.Proceed();
            }
            catch (Exception e)
            {
                isSuccess = false;
                OnException(invocation, e); // When an error occurs in the method
                throw;
            }
            finally
            { // final block is like else, it runs anyway, if an error occurs or not 
                if (isSuccess)
                {
                    OnSuccess(invocation); // When the method succeded
                }
            }
            OnAfter(invocation); // After the method
        }
    }
}