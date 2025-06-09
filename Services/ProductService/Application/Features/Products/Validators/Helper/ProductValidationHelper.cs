using ProductService.Contract.Persistence;

namespace ProductService.Application.Features.Products.Validators.Helper;

public abstract class ProductValidationHelper(IProductRepository productRepository)
{
    public async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
    {
        // Check if the product name is unique using the repository
        return await productRepository.IsNameUniqueAsync(null, name);
    }

    public async Task<bool> BeExistingCategoryId(string name, CancellationToken cancellationToken)
    {
        // Check if the category ID exists using the repository
        return await productRepository.IsCategoryIdExisting(name);
    }
    
    public async Task<bool> BeExistingProductId(string id, CancellationToken cancellationToken)
    {
        // Check if the product ID exists using the repository
        return await productRepository.ExistsByIdAsync(id);
    }
    
    public bool BeAValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out _)
               && (url.StartsWith("http://") || url.StartsWith("https://"));
    }
}