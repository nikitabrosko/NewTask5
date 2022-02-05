using Application.Common.Models;
using MediatR;

namespace Application.UseCases.Identity.Role.Queries.GetRolesWithPagination
{
    public class GetRolesWithPaginationQuery : IRequest<PaginatedList<RoleDto>>
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }
}