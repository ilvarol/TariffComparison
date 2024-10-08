using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TariffProvider.Application.Features.Queries.GetProducts;
using TariffProvider.Domain.Models;

namespace TariffProvider.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, GetProductsViewModel>()
            .ForMember(dest => dest.AdditionalKwhCost, 
                       opt => opt.MapFrom(src => src.AdditionalKwhCost * 100));
    }
}
