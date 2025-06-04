using AutoMapper;
using ProductService.Contract.DTOs;
using ProductService.Domain.Models;

namespace ProductService.Application.Mappers;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<Product, ProductDto>();
        CreateMap<CreateProductDto, Product>();
    }
}