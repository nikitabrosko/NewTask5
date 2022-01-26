using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using System.Linq;

namespace Application.UseCases.Customers.Queries.GetCustomersWithPagination
{
    public class GetCustomersWithPaginationQueryHandler : IRequestHandler<GetCustomersWithPaginationQuery, PaginatedList<CustomerDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetCustomersWithPaginationQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedList<CustomerDto>> Handle(GetCustomersWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Customers
                .Select(customer => new CustomerDto
                {
                    Id = customer.Id,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    FullName = customer.FullName
                })
                .OrderBy(c => c.Id);

            return await PaginatedList<CustomerDto>.CreateAsync(query, request.PageNumber, request.PageSize);
        }
    }
}