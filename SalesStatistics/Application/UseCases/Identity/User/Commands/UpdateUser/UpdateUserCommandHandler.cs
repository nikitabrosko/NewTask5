using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.UseCases.Identity.User.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly UserManager<Domain.IdentityEntities.User> _userManager;

        public UpdateUserCommandHandler(UserManager<Domain.IdentityEntities.User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var entity = await _userManager.FindByIdAsync(request.Id);

            if (request.Name != null)
            {
                entity.UserName = request.Name;
            }

            if (request.Email != null)
            {
                await _userManager.ChangeEmailAsync(entity, request.Email, "");
            }

            if (request.Password != null)
            {
                await _userManager.ChangePasswordAsync(entity, entity.PasswordHash, request.Password);
            }

            return Unit.Value;
        }
    }
}