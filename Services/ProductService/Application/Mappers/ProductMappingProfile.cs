using AutoMapper;
using ProductService.Application.Features.Products.Commands;
using ProductService.Contract.DTOs;
using ProductService.Domain.Models;

namespace ProductService.Application.Mappers;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<Product, ProductResponse>();
        CreateMap<CreateProductCommand, Product>();
    }
}