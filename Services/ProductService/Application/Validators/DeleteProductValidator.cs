using FluentValidation;
using ProductService.Application.Features.Products.Commands;
using ProductService.Common.Application.Validators;
using ProductService.Contract.Persistence;

namespace ProductService.Application.Validators;

public class DeleteProductValidator : BaseValidator<DeleteProductCommand>
{
    public DeleteProductValidator(ProductValidationHelper productValidationHelper)
    {
        RuleFor(product => product.Id)
            .NotEmpty().WithMessage("Product.Id.Required")
            .MustAsync(productValidationHelper.ProductIdExists)
                .WithMessage("Product.Id.NotExists");
    }
}