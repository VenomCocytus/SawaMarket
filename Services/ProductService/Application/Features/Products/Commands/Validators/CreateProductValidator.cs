using FluentValidation;
using ProductService.Application.Features.Products.Commands.Requests;
using ProductService.Application.Helper;
using ProductService.Domain.Validators;

namespace ProductService.Application.Features.Products.Commands.Validators;

public class CreateProductValidator : BaseValidator<CreateProductCommand>
{
    public CreateProductValidator(ProductValidationHelper validationHelper)
    {
        RuleFor(product => product.Name)
            .NotEmpty().WithMessage("Product.Name.Required")
            .Length(2, 100).WithMessage("Product.Name.Lenght")
            .MustAsync(validationHelper.BeUniqueName)
                .WithMessage("Product.Name.Duplicate");

        RuleFor(product => product.Description)
            .MaximumLength(500).WithMessage("Product.Description.MaxLength");

        RuleFor(product => product.Price)
            .NotEmpty().WithMessage("Product.Price.Required")
            .NotNull().WithMessage("Product.Price.Required")
            .GreaterThan(0).WithMessage("Product.Price.GreaterThanZero");

        RuleFor(product => product.CategoryId)
            .NotEmpty().WithMessage("Product.Category.Required")
            .MustAsync(validationHelper.BeExistingCategoryId)
                .WithMessage("Product.CategoryId.NotExists");
    }
}