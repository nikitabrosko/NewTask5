using System;
using Application.Common.Models;
using MediatR;

namespace Application.UseCases.Orders.Queries.GetFilteringOrdersWithPagination
{
    public class GetFilteringOrdersWithPaginationQuery : IRequest<PaginatedList<OrderDto>>
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public string DateFromFilter { get; set; }

        public string DateToFilter { get; set; }

        public string Sum { get; set; }
    }
}