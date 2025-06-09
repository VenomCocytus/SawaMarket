using ProductService.Contract.Persistence;

namespace ProductService.Application.Validators;

public abstract class ProductValidationHelper(IProductRepository productRepository)
{
    public async Task<bool> NameIsUnique(string name, CancellationToken cancellationToken)
    {
        // Check if the product name is unique using the repository
        return await productRepository.IsNameUniqueAsync(null, name);
    }

    public async Task<bool> CategoryIdExists(string name, CancellationToken cancellationToken)
    {
        // Check if the category ID exists using the repository
        return await productRepository.IsCategoryIdExisting(name);
    }
    
    public async Task<bool> ProductIdExists(string id, CancellationToken cancellationToken)
    {
        // Check if the product ID exists using the repository
        return await productRepository.ExistsByIdAsync(id);
    }
}