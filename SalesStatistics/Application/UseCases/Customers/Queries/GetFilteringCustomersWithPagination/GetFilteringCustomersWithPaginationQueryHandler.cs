using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using System.Linq;
using Domain.Entities;

namespace Application.UseCases.Customers.Queries.GetFilteringCustomersWithPagination
{
    public class GetFilteringCustomersWithPaginationQueryHandler : IRequestHandler<GetFilteringCustomersWithPaginationQuery, PaginatedList<CustomerDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetFilteringCustomersWithPaginationQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedList<CustomerDto>> Handle(GetFilteringCustomersWithPaginationQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Customer> customers = null;

            var filterState = request.FirstNameFilter is null
                    ? FilterState.OnlyLastName : request.LastNameFilter is null
                        ? FilterState.OnlyFirstName : FilterState.BothParameters;

            customers = filterState switch
            {
                FilterState.BothParameters => _context.Customers
                    .Where(customer => (customer.FirstName.Equals(request.FirstNameFilter) ||
                                        customer.FirstName.StartsWith(request.FirstNameFilter)) &&
                                       (customer.LastName.Equals(request.LastNameFilter) ||
                                        customer.LastName.StartsWith(request.LastNameFilter))),
                FilterState.OnlyFirstName => _context.Customers
                    .Where(customer => customer.FirstName.Equals(request.FirstNameFilter) ||
                                       customer.FirstName.StartsWith(request.FirstNameFilter)),
                FilterState.OnlyLastName => _context.Customers
                    .Where(customer => customer.LastName.Equals(request.LastNameFilter) ||
                                       customer.LastName.StartsWith(request.LastNameFilter)),
                _ => _context.Customers
            };

            var query = customers
                .Select(customer => new CustomerDto 
                {
                    Id = customer.Id,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    FullName = customer.FullName
                })
                .OrderBy(customer => customer.Id);

            return await PaginatedList<CustomerDto>.CreateAsync(query, request.PageNumber, request.PageSize);
        }
    }
}