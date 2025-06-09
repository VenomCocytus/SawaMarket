using FluentValidation;
using ProductService.Application.Features.Products.Commands;
using ProductService.Common.Application.Validators;

namespace ProductService.Application.Validators;

public class UpdateProductValidator : BaseValidator<UpdateProductCommand>
{
    public UpdateProductValidator(ProductValidationHelper validationHelper)
    {
        RuleFor(product => product.Id)
            .NotEmpty().WithMessage("Product.Id.Required")
            .MustAsync(validationHelper.ProductIdExists)
                .WithMessage("Product.Id.NotExists");

        RuleFor(product => product.Name)
            .NotEmpty().WithMessage("Product.Name.Required")
            .Length(2, 100).WithMessage("Product.Name.Length")
            .MustAsync(validationHelper.NameIsUnique).WithMessage("Product.Name.Duplicate");

        RuleFor(product => product.Description)
            .MaximumLength(500).WithMessage("Product.Description.MaxLength");

        RuleFor(product => product.Price)
            .NotEmpty().WithMessage("Product.Price.Required")
            .NotNull().WithMessage("Product.Price.Required")
            .GreaterThan(0).WithMessage("Product.Price.GreaterThanZero");

        RuleFor(product => product.CategoryId)
            .NotEmpty().WithMessage("Product.Category.Required")
            .MustAsync(validationHelper.CategoryIdExists)
                .WithMessage("Product.CategoryId.NotExists");
    }
}