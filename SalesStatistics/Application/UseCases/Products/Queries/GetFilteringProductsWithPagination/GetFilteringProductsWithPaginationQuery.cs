using Application.Common.Models;
using MediatR;

namespace Application.UseCases.Products.Queries.GetFilteringProductsWithPagination
{
    public class GetFilteringProductsWithPaginationQuery : IRequest<PaginatedList<ProductDto>>
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public string NameFilter { get; set; }

        public string PriceFilter { get; set; }
    }
}