using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using System.Linq;

namespace Application.UseCases.Orders.Queries.GetOrdersWithPagination
{
    public class GetOrdersWithPaginationQueryHandler : IRequestHandler<GetOrdersWithPaginationQuery, PaginatedList<OrderDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetOrdersWithPaginationQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedList<OrderDto>> Handle(GetOrdersWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Orders
                .Select(order => new OrderDto
                {
                    Id = order.Id,
                    Date = order.Date,
                    Sum = order.Sum,
                    CustomerFullName = order.Customer.FullName,
                    ManagerLastName = order.Manager.LastName,
                    ProductName = order.Product.Name
                })
                .OrderBy(o => o.Id);

            return await PaginatedList<OrderDto>.CreateAsync(query, request.PageNumber, request.PageSize);
        }
    }
}