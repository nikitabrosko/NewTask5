using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Orders.Queries.GetFilteringOrdersWithPagination
{
    public class GetFilteringOrdersWithPaginationQueryHandler : IRequestHandler<GetFilteringOrdersWithPaginationQuery, PaginatedList<OrderDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetFilteringOrdersWithPaginationQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        
        public Task<PaginatedList<OrderDto>> Handle(GetFilteringOrdersWithPaginationQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Order> orders = null;

            var filterState = request.Sum is null
                            ? FilterState.OnlyFullDate : request.DateFromFilter is null && request.DateToFilter is null
                                ? FilterState.OnlySum : FilterState.AllParameters;

            orders = filterState switch
            {
                FilterState.OnlySum => _context.Orders
                    .Where(order => order.Sum.Equals(decimal.Parse(request.Sum, CultureInfo.InvariantCulture))),
                FilterState.OnlyFullDate => _context.Orders
                    .Where(order => order.Date >= DateTime.Parse(request.DateFromFilter) &&
                                    order.Date <= DateTime.Parse(request.DateToFilter)),
                FilterState.AllParameters => _context.Orders
                    .Where(order => (order.Date >= DateTime.Parse(request.DateFromFilter) &&
                                     order.Date <= DateTime.Parse(request.DateToFilter)) &&
                                    order.Sum.Equals(decimal.Parse(request.Sum, CultureInfo.InvariantCulture))),
                _ => _context.Orders
            };

            var query = orders
                .Select(order => new OrderDto
                {
                    Id = order.Id,
                    Date = order.Date,
                    Sum = order.Sum,
                    CustomerFullName = order.Customer.FullName,
                    ManagerLastName = order.Manager.LastName,
                    ProductName = order.Product.Name
                })
                .OrderBy(order => order.Id);

            return PaginatedList<OrderDto>.CreateAsync(query, request.PageNumber, request.PageSize);
        }
    }
}