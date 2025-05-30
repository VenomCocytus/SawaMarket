using AutoMapper;
using ProductService.Application.DTOs;
using ProductService.Domain.Models;

namespace ProductService.Application.Mappers;

public class ProfileProfile : Profile
{
    public ProfileProfile()
    {
        CreateMap<Product, GetProductDto>();
        CreateMap<CreateProductDto, Product>();
    }
}