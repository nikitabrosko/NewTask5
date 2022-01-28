using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Products.Queries.GetFilteringProductsWithPagination
{
    public class GetFilteringProductsWithPaginationQueryHandler : IRequestHandler<GetFilteringProductsWithPaginationQuery, PaginatedList<ProductDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetFilteringProductsWithPaginationQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedList<ProductDto>> Handle(GetFilteringProductsWithPaginationQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Product> products = null;

            var filterState = request.NameFilter is null && request.PriceFilter is null
                ? FilterState.NoParameters : request.NameFilter is null
                    ? FilterState.OnlyPrice : request.PriceFilter is null
                        ? FilterState.OnlyName : FilterState.BothParameters;

            switch (filterState)
            {
                case FilterState.NoParameters:
                    products = _context.Products;
                    break;
                case FilterState.BothParameters:
                    products = _context.Products
                        .Where(product => (product.Name.Equals(request.NameFilter) 
                                           || product.Name.StartsWith(request.NameFilter)) 
                                          && product.Price.Equals(decimal.Parse(request.PriceFilter)));
                    break;
                case FilterState.OnlyName:
                    products = _context.Products
                        .Where(product => product.Name.Equals(request.NameFilter) 
                                          || product.Name.StartsWith(request.NameFilter));
                    break;
                case FilterState.OnlyPrice:
                    products = _context.Products
                        .Where(product => product.Price.Equals(decimal.Parse(request.PriceFilter)));
                    break;
            }

            var query = products?
                .Select(product => new ProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price
                })
                .OrderBy(product => product.Id);

            return await PaginatedList<ProductDto>.CreateAsync(query, request.PageNumber, request.PageSize);
        }
    }
}