using FluentValidation;
using MongoDB.Bson;
using ProductService.Application.Features.Products.Commands;
using ProductService.Common.Application.Validators;
using ProductService.Infrastructure.Repositories;

namespace ProductService.Application.Validators;

public class UpdateProductValidator : BaseValidator<UpdateProductCommand>
{
    private readonly ProductRepository _productRepository;
    
    public UpdateProductValidator(ProductRepository productRepository)
    {
        _productRepository = productRepository;
        RuleFor(product => product.Id)
            .NotEmpty().WithMessage("Product.Id.Required")
            .MustAsync(ProductIdExists).WithMessage("Product.Id.NotExists");

        RuleFor(product => product.Name)
            .NotEmpty().WithMessage("Product.Name.Required")
            .Length(2, 100).WithMessage("Product.Name.Length")
            .MustAsync(NameIsUnique).WithMessage("Product.Name.Duplicate");

        RuleFor(product => product.Description)
            .MaximumLength(500).WithMessage("Product.Description.MaxLength");

        RuleFor(product => product.Price)
            .NotEmpty().WithMessage("Product.Price.Required")
            .NotNull().WithMessage("Product.Price.Required")
            .GreaterThan(0).WithMessage("Product.Price.GreaterThanZero");

        RuleFor(product => product.CategoryId)
            .NotEmpty().WithMessage("Product.Category.Required")
            .MustAsync(CategoryIdExists).WithMessage("Product.CategoryId.NotExists");
    }
    
    private async Task<bool> ProductIdExists(string id, CancellationToken cancellationToken)
    {
        // Check if the product ID exists using the repository
        return await _productRepository.ExistsByIdAsync(id);
    }
    
    private async Task<bool> NameIsUnique(UpdateProductCommand command, string name, 
        CancellationToken cancellationToken)
    {
        // Check if the product name is unique using the repository
        return await _productRepository.IsNameUniqueAsync(command.Id, name);
    }

    private async Task<bool> CategoryIdExists(string name, CancellationToken cancellationToken)
    {
        // Check if the category ID exists using the repository
        return await _productRepository.IsCategoryIdExisting(name);
    }
}