using Entities.Concrete;
using FluentValidation;
using System;

namespace Business.ValidationRules.FluentValidation
{
    public class HistoryValidator : AbstractValidator<History>
    { // Structural validation for History with FluentValidation
        public HistoryValidator()
        {
            RuleFor(x => x.Dc_Date).NotEmpty();
            RuleFor(x => x.Dc_Category).NotEmpty();
            RuleFor(x => x.Dc_Event).NotEmpty().MinimumLength(5).WithMessage("Event field can not be empty and has to be at least 5 characters.");
            RuleFor(x => x.LanguageCode).NotEmpty().LessThanOrEqualTo(Convert.ToInt16(1)).GreaterThanOrEqualTo(Convert.ToInt16(0)).WithMessage("Language code can not be empty and has to be 0 for Turkish or 1 for Italian");
        }
    }
}