using AutoMapper;
using ProductService.Application.DTOs;
using ProductService.Contract.DTOs;
using ProductService.Domain.Models;

namespace ProductService.Application.Mappers;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<Product, GetProductDto>();
        CreateMap<CreateProductDto, Product>();
    }
}