using Application.Common.Models;
using MediatR;

namespace Application.UseCases.Orders.Queries.GetOrdersWithPagination
{
    public class GetOrdersWithPaginationQuery : IRequest<PaginatedList<OrderDto>>
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }
}