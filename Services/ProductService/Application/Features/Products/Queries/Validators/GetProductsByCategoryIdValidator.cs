using FluentValidation;
using ProductService.Application.Features.Products.Queries.Requests;
using ProductService.Application.Helper;
using ProductService.Domain.Validators;

namespace ProductService.Application.Features.Products.Queries.Validators;

public class GetProductsByCategoryIdValidator : BaseValidator<GetProductsByCategoryIdQuery>
{
    public GetProductsByCategoryIdValidator(ProductValidationHelper validationHelper)
    {
        RuleFor(product => product.CategoryId)
            .NotEmpty().WithMessage("Product.CategoryId.Required")
            .MustAsync(validationHelper.BeExistingCategoryId!)
                .WithMessage("Product.CategoryId.NotExists");
    }
}