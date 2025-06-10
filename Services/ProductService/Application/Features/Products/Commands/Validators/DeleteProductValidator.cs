using FluentValidation;
using ProductService.Application.Features.Products.Commands.Requests;
using ProductService.Application.Helper;
using ProductService.Domain.Validators;

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