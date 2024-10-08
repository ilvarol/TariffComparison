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

public class GetProductsQuery : IRequest<List<GetProductsViewModel>>
{
}
