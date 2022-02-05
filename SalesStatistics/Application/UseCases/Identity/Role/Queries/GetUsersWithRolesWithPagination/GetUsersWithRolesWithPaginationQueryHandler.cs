using System.Threading;
using System.Threading.Tasks;
using Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace Application.UseCases.Identity.Role.Queries.GetUsersWithRolesWithPagination
{
    public class GetUsersWithRolesWithPaginationQueryHandler : 
        IRequestHandler<GetUsersWithRolesWithPaginationQuery, PaginatedList<UserWithRolesDto>>
    {
        private readonly UserManager<Domain.IdentityEntities.User> _userManager;

        public GetUsersWithRolesWithPaginationQueryHandler(UserManager<Domain.IdentityEntities.User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<PaginatedList<UserWithRolesDto>> Handle(GetUsersWithRolesWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var query = _userManager.Users
                .Select(user => new UserWithRolesDto
                {
                    Id = user.Id,
                    Name = user.UserName,
                });

            foreach (var user in query.ToList())
            {
                var entity = await _userManager.FindByIdAsync(user.Id);

                user.Roles = await _userManager.GetRolesAsync(entity);
            }

            return await PaginatedList<UserWithRolesDto>.CreateAsync(query, request.PageNumber, request.PageSize);
        }
    }
}