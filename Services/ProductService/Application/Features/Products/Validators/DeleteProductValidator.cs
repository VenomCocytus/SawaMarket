using FluentValidation;
using ProductService.Application.Features.Products.Commands;
using ProductService.Application.Features.Products.Validators.Helper;
using ProductService.Common.Application.Validators;

namespace ProductService.Application.Features.Products.Validators;

public class DeleteProductValidator : BaseValidator<DeleteProductCommand>
{
    public DeleteProductValidator(ProductValidationHelper productValidationHelper)
    {
        RuleFor(product => product.Id)
            .NotEmpty().WithMessage("Product.Id.Required")
            .MustAsync(productValidationHelper.BeExistingProductId)
                .WithMessage("Product.Id.NotExists");
    }
}