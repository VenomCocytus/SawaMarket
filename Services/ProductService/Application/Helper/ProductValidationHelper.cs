using ProductService.Application.Features.Products.Queries.Requests;
using ProductService.Contract.Persistence;

namespace ProductService.Application.Helper;

public abstract class ProductValidationHelper(IProductRepository productRepository)
{
    public async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken) => 
        await productRepository.IsNameUniqueAsync(null, name);

    public async Task<bool> BeExistingCategoryId(string name, CancellationToken cancellationToken) => 
        await productRepository.ExistsByCategoryId(name);
    
    public async Task<bool> BeExistingProductId(string id, CancellationToken cancellationToken) => 
        await productRepository.ExistsByIdAsync(id);
    
    public async Task<bool> BeExistingProductName(string name, CancellationToken cancellationToken) =>
        await productRepository.ExistsByName(name);
    
    public static bool HaveAtLeastOneIdentifier(GetOneProductQuery request) => 
        !string.IsNullOrEmpty(request.Name) || !string.IsNullOrEmpty(request.Id);
    
    public bool BeAValidUrl(string url) => 
        Uri.TryCreate(url, UriKind.Absolute, out _) 
            && (url.StartsWith("http://") || url.StartsWith("https://"));
}