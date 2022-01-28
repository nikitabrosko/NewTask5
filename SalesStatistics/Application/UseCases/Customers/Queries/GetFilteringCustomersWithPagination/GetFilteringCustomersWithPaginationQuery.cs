using Application.Common.Models;
using MediatR;

namespace Application.UseCases.Customers.Queries.GetFilteringCustomersWithPagination
{
    public class GetFilteringCustomersWithPaginationQuery : IRequest<PaginatedList<CustomerDto>>
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public string FirstNameFilter { get; set; }

        public string LastNameFilter { get; set; }
    }
}