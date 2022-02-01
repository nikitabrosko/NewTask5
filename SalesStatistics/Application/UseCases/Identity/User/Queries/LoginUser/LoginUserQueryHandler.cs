using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.UseCases.Identity.User.Queries.LoginUser
{
    public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, SignInResult>
    {
        private readonly SignInManager<Domain.IdentityEntities.User> _signInManager;

        public LoginUserQueryHandler(SignInManager<Domain.IdentityEntities.User> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<SignInResult> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            return await _signInManager.PasswordSignInAsync(request.Name, request.Password, 
                request.RememberMe, false);
        }
    }
}