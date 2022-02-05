using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.UseCases.Identity.Role.Queries.GetRolesWithPagination
{
    public class GetRolesWithPaginationQueryHandler : IRequestHandler<GetRolesWithPaginationQuery, PaginatedList<RoleDto>>
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public GetRolesWithPaginationQueryHandler(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public Task<PaginatedList<RoleDto>> Handle(GetRolesWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var query = _roleManager.Roles
                .Select(role => new RoleDto
                {
                    Id = role.Id,
                    Name = role.Name
                });

            return PaginatedList<RoleDto>.CreateAsync(query, request.PageNumber, request.PageSize);
        }
    }
}