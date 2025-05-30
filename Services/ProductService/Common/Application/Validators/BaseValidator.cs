using FluentValidation;
using FluentValidation.Resources;

namespace ProductService.Common.Application.Validators;

public abstract class BaseValidator<T> : AbstractValidator<T>
{
    protected BaseValidator()
    {
        ValidatorOptions.Global.LanguageManager = new LanguageManager();
    }
}