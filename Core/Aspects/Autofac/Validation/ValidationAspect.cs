using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Linq;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception
    {
        private readonly Type _validatorType; //Fo validation process, need a validator type

        public ValidationAspect(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            { // If the given validator is not a IValidator or AbstractValidator throw an error
                throw new Exception("It is not a validator class!");
            }
            _validatorType = validatorType; // Set the validator
        }

        // Execute before method and validate parameters of method
        protected override void OnBefore(IInvocation invocation)
        { // OnBefore overrided
            var validator = (IValidator)Activator.CreateInstance(_validatorType); // Reflection, new the type at runtime
            var entityType = _validatorType.BaseType.GetGenericArguments()[0]; // Go to Validator's Base type and take the first class' type 
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);
            // Find the Methods parameter type(s) where the base's generic type is equal and take them into a list

            foreach (var entity in entities)
            { // Validate type(s) one by one
                ValidationTool.Validate(validator, entity);
            }
        }
    }
}