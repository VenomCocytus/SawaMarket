using AutoMapper;

namespace ProductService.Application.Mappers;

public class ProductMapper
{
    // To ensure that the mapper is only created once and reused
    private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            // Map only public, not null and internal properties
            cfg.ShouldMapProperty = p => p.GetMethod != null 
                                         && (p.GetMethod.IsPublic || p.GetMethod.IsAssembly);
            
            // Registering the mapping profile
            cfg.AddProfile<ProductMappingProfile>();
        });
        var mapper = configuration.CreateMapper();
        
        return mapper;
    });
    
    public static IMapper Mapper => Lazy.Value;
}