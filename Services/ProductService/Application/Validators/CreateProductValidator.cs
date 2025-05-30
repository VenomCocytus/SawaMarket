using FluentValidation;
using ProductService.Application.DTOs;
using ProductService.Common.Application.Validators;

namespace ProductService.Application.Validators;

public class CreateProductValidator : BaseValidator<CreateProductDto>
{
    public CreateProductValidator()
    {
        RuleFor(product => product.Name)
            .NotEmpty().WithMessage("Product.Name.Required")
            .Length(2, 100).WithMessage("Product.Name.Lenght")
            .MustAsync(BeUniqueName).WithMessage("Product.Name.Duplicate");

        RuleFor(product => product.Description)
            .MaximumLength(500).WithMessage("Product.Description.MaxLength");

        RuleFor(product => product.Price)
            .GreaterThan(0).WithMessage("Product.Price.GreaterThanZero");
    }
    
    private async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
    {
        //TODO: query the database or service to check if the product name is unique
        await Task.Delay(100, cancellationToken); // Simulating async operation
        return true; // Assume the name is unique for this example
    }
}