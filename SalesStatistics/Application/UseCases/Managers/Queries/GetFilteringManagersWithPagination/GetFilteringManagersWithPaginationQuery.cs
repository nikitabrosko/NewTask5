using Application.Common.Models;
using MediatR;

namespace Application.UseCases.Managers.Queries.GetFilteringManagersWithPagination
{
    public class GetFilteringManagersWithPaginationQuery : IRequest<PaginatedList<ManagerDto>>
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public string LastNameFilter { get; set; }
    }
}