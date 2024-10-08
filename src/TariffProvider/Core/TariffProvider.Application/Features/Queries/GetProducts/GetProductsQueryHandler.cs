using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TariffProvider.Application.Interfaces.Repositories;

namespace TariffProvider.Application.Features.Queries.GetProducts;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<GetProductsViewModel>>
{
    private readonly IProductRepository productRepository;
    private readonly IMapper mapper;

    public GetProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        this.productRepository = productRepository;
        this.mapper = mapper;
    }

    public async Task<List<GetProductsViewModel>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var query = productRepository.AsQueryable();

        var result = await query.ProjectTo<GetProductsViewModel>(mapper.ConfigurationProvider)
                          .ToListAsync(cancellationToken);

        return result;
    }
}
