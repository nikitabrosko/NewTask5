using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.UseCases.Identity.Role.Commands.AddRolesToUser
{
    public class AddRoleToUserCommandHandler<TUser> : IRequestHandler<AddRoleToUserCommand, IdentityResult> where TUser : class
    {
        private readonly UserManager<TUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AddRoleToUserCommandHandler(UserManager<TUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> Handle(AddRoleToUserCommand request, CancellationToken cancellationToken)
        {
            var userEntity = await _userManager.FindByIdAsync(request.UserId);

            if (userEntity is null)
            {
                throw new NotFoundException(nameof(TUser), request.UserId);
            }

            var roleEntity = await _roleManager.FindByIdAsync(request.RoleId);

            if (roleEntity is null)
            {
                throw new NotFoundException(nameof(IdentityRole), request.RoleId);
            }

            return await _userManager.AddToRoleAsync(userEntity, roleEntity.Name);
        }
    }
}