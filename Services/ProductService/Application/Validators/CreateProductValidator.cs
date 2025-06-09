using FluentValidation;
using ProductService.Application.Features.Products.Commands;
using ProductService.Common.Application.Validators;

namespace ProductService.Application.Validators;

public class CreateProductValidator : BaseValidator<CreateProductCommand>
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

        RuleFor(product => product.CategoryId)
            .NotEmpty().WithMessage("Product.Category.Required")
            .MustAsync(CategoryExists).WithMessage("Product.CategoryId.NotExists");
    }
    
    private static async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
    {
        //TODO: query the database or service to check if the product name is unique
        //TODO: create a mongo database online to try the connection
        await Task.Delay(100, cancellationToken); // Simulating async operation
        return true; // Assume the name is unique for this example
    }

    private static async Task<bool> CategoryExists(string name, CancellationToken cancellationToken)
    {
        //TODO: implement a method to check if the submitted category exists in the data context
        await Task.Delay(100, cancellationToken); // Simulating async operation
        return true; // Placeholder.  Assume the category already exist
    }
}