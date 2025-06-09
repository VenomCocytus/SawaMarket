using FluentValidation;
using ProductService.Application.Features.Products.Commands.Requests;
using ProductService.Common.Application.Validators;
using ProductService.Common.Helper;

namespace ProductService.Application.Features.Products.Commands.Validators;

public class UpdateProductValidator : BaseValidator<UpdateProductCommand>
{
    public UpdateProductValidator(ProductValidationHelper validationHelper)
    {
        RuleFor(product => product.Id)
            .NotEmpty().WithMessage("Product.Id.Required")
            .MustAsync(validationHelper.BeExistingProductId)
                .WithMessage("Product.Id.NotExists");

        RuleFor(product => product.Name)
            .NotEmpty().WithMessage("Product.Name.Required")
            .Length(2, 100).WithMessage("Product.Name.Length")
            .MustAsync(validationHelper.BeUniqueName).WithMessage("Product.Name.Duplicate");

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