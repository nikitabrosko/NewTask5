using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.UseCases.Identity.User.Commands.CreateUser;
using Application.UseCases.Identity.User.Queries.LoginUser;
using Domain.IdentityEntities;
using Microsoft.AspNetCore.Identity;

namespace WebUI.Controllers
{
    public class AccountController : ApiControllerBase
    {
        private readonly SignInManager<User> _signInManager;

        public AccountController(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm] CreateUserCommand command)
        {
            var result = await Mediator.Send(command);

            if (result.Result.Succeeded)
            {
                await _signInManager.SignInAsync(result.User, false);

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginUserQuery {ReturnUrl = returnUrl});
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginUserQuery query)
        {
            var result = await Mediator.Send(query);

            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(query.ReturnUrl) && Url.IsLocalUrl(query.ReturnUrl))
                {
                    return Redirect(query.ReturnUrl);
                }

                return RedirectToAction("Index", "Home");
            }

            return View(query);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
