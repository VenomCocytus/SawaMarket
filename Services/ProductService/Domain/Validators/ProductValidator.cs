using ProductService.Application.Helper;
using ProductService.Domain.Models;

namespace ProductService.Domain.Validators;

using FluentValidation;

public class ProductValidator : BaseValidator<Product>
{
    public ProductValidator(ProductValidationHelper productValidationHelper)
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("Product.Name.Required")
            .Length(3, 100).WithMessage("Product.Name.Length");

        RuleFor(p => p.Description)
            .MaximumLength(500).WithMessage("Product.Description.MaxLength");

        RuleFor(p => p.Price)
            .NotEmpty().WithMessage("Price.Required")
            .NotNull().WithMessage("Price.Required")
            .GreaterThan(0).WithMessage("Price.GreaterThanZero")
            .LessThanOrEqualTo(100000).WithMessage("Price.LessThanOrEqualTo");

        RuleFor(p => p.StockQuantity)
            .NotEmpty().WithMessage("Stock.Quantity.Required")
            .InclusiveBetween(0, int.MaxValue).WithMessage("Stock.Quantity.NonNegative");

        RuleFor(p => p.CategoryId)
            .NotEmpty().WithMessage("Category.Required")
            .Length(1, 50).WithMessage("Category.Length");

        RuleFor(p => p.ThumbnailUrl)
            .Must(productValidationHelper.BeAValidUrl).WithMessage("Thumbnail.NotAValidUrl");

        RuleFor(p => p.ProductUrl)
            .Must(productValidationHelper.BeAValidUrl).WithMessage("Product.NotAValidUrl");
    }
}