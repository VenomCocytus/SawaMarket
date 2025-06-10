using FluentValidation;
using ProductService.Application.Features.Products.Queries.Requests;
using ProductService.Application.Helper;
using ProductService.Domain.Validators;

namespace ProductService.Application.Features.Products.Queries.Validators;

public class GetOneProductValidator : BaseValidator<GetOneProduct>
{
    public GetOneProductValidator(ProductValidationHelper validationHelper)
    {
        RuleFor(x => x)
            .Must(ProductValidationHelper.HaveAtLeastOneIdentifier)
            .NotNull().WithMessage("Product.Id.Or.Name.Required");
        
        RuleFor(product => product.Id)
            .MustAsync(validationHelper.BeExistingProductId!)
                .When(product => !string.IsNullOrEmpty(product.Id))
                    .WithMessage("Product.Id.NotExists");
        
        When(product => !string.IsNullOrEmpty(product.Name), () =>
        {
            RuleFor(product => product.Name)
                .MustAsync(validationHelper.BeExistingProductName!)
                    .WithMessage("Product.Name.NotExists");
        });
    }
}