using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.UseCases.Identity.User.Commands.CreateUser;
using Application.UseCases.Identity.User.Queries.LoginUser;
using Domain.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers.Identity
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

            return View("IdentityError", result.Result.Errors);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return PartialView("_SignInPartial", new LoginUserQuery {ReturnUrl = returnUrl});
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginUserQuery query)
        {
            try
            {
                var result = await Mediator.Send(query);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                return Content("Something went wrong. Check password!");
            }
            catch (NotFoundException exception)
            {
                return Content(exception.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
