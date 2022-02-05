using System.Threading;
using System.Threading.Tasks;
using Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace Application.UseCases.Identity.User.Queries.GetUsersWithPagination
{
    public class GetUsersWithPaginationQueryHandler : IRequestHandler<GetUsersWithPaginationQuery, PaginatedList<UserDto>>
    {
        private readonly UserManager<Domain.IdentityEntities.User> _userManager;

        public GetUsersWithPaginationQueryHandler(UserManager<Domain.IdentityEntities.User> userManager)
        {
            _userManager = userManager;
        }

        public Task<PaginatedList<UserDto>> Handle(GetUsersWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var query = _userManager.Users
                .Select(user => new UserDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = user.UserName
                });

            return PaginatedList<UserDto>.CreateAsync(query, request.PageNumber, request.PageSize);
        }
    }
}