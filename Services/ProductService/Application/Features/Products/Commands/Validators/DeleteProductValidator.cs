using FluentValidation;
using ProductService.Application.Features.Products.Commands.Requests;
using ProductService.Common.Application.Validators;
using ProductService.Common.Helper;

namespace ProductService.Application.Features.Products.Commands.Validators;

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