using Application.Common.Models;
using MediatR;

namespace Application.UseCases.Products.Queries.GetProductsWithPagination
{
    public class GetProductsWithPaginationQuery : IRequest<PaginatedList<ProductDto>>
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }
}