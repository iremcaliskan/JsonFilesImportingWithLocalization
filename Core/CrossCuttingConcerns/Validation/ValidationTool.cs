using FluentValidation;

namespace Core.CrossCuttingConcerns.Validation
{
    public static class ValidationTool
    {
        //  Generic validation tool for any object
        public static void Validate(IValidator validator, object entity)
        {
            var context = new ValidationContext<object>(entity); // Create an validation context for given entity type
            var result = validator.Validate(context); // Validate the given entity with given Validator

            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}