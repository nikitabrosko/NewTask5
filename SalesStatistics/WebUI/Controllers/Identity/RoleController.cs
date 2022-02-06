using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.UseCases.Identity.Role.Commands.AddRoleToUser;
using Application.UseCases.Identity.Role.Commands.CreateRole;
using Application.UseCases.Identity.Role.Commands.DeleteRole;
using Application.UseCases.Identity.Role.Commands.RemoveRoleFromUser;
using Application.UseCases.Identity.Role.Commands.UpdateRole;
using Application.UseCases.Identity.Role.Queries.GetRolesForSpecifiedUser;
using Application.UseCases.Identity.Role.Queries.GetRolesWithPagination;
using Application.UseCases.Identity.Role.Queries.GetUsersWithRolesWithPagination;
using Microsoft.AspNetCore.Authorization;

namespace WebUI.Controllers.Identity
{
    [Authorize(Roles = "admin")]
    public class RoleController : ApiControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> RolesPage([FromQuery] GetRolesWithPaginationQuery query)
        {
            return View(await Mediator.Send(query));
        }

        [HttpGet]
        public async Task<IActionResult> UsersRolesPage([FromQuery] GetUsersWithRolesWithPaginationQuery query)
        {
            return View(await Mediator.Send(query));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateRoleCommand command)
        {
            try
            {
                var result = await Mediator.Send(command);

                if (result.Succeeded)
                {
                    return RedirectToAction("RolesPage");
                }

                return View("IdentityError", result.Errors);
            }
            catch (NotFoundException exception)
            {
                return View("Error", new[] { exception.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult Update([FromRoute] string id)
        {
            return View(new UpdateRoleCommand { Id = id });
        }

        [HttpPost("{command}")]
        [Route("Update/{command}")]
        public async Task<IActionResult> Update([FromForm] UpdateRoleCommand command)
        {
            try
            {
                var result = await Mediator.Send(command);

                if (result.Succeeded)
                {
                    return RedirectToAction("RolesPage");
                }

                return View("IdentityError", result.Errors);
            }
            catch (NotFoundException exception)
            {
                return View("Error", new[] { exception.Message });
            }
        }

        [HttpDelete("{id}")]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            try
            {
                var result = await Mediator.Send(new DeleteRoleCommand { Id = id });

                if (result.Succeeded)
                {
                    return RedirectToAction("RolesPage");
                }

                return View("IdentityError", result.Errors);
            }
            catch (NotFoundException exception)
            {
                return View("Error", new[] { exception.Message });
            }
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> AddRoleToUser([FromRoute] string userId, [FromQuery] GetRolesWithPaginationQuery query)
        {
            ViewBag.UserId = userId;

            return View(await Mediator.Send(query));
        }

        [HttpGet("{userId}/{roleId}")]
        public async Task<IActionResult> AddRoleToUser(string userId, string roleId)
        {
            try
            {
                var result = await Mediator.Send(new AddRoleToUserCommand { UserId = userId, RoleId = roleId });

                if (result.Succeeded)
                {
                    return RedirectToAction("UsersRolesPage");
                }

                return View("IdentityError", result.Errors);
            }
            catch (NotFoundException exception)
            {
                return View("Error", new[] { exception.Message });
            }
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> RemoveRoleFromUser(string userId)
        {
            ViewBag.UserId = userId;

            return View(await Mediator.Send(new GetRolesForSpecifiedUserQuery {UserId = userId}));
        }

        [HttpGet("{userId}/{roleName}")]
        public async Task<IActionResult> RemoveRoleFromUser(string userId, string roleName)
        {
            try
            {
                var result = await Mediator.Send(new RemoveRoleFromUserCommand {UserId = userId, RoleName = roleName});

                if (result.Succeeded)
                {
                    return RedirectToAction("UsersRolesPage");
                }

                return View("IdentityError", result.Errors);
            }
            catch (NotFoundException exception)
            {
                return View("Error", new[] { exception.Message });
            }
        }
    }
}
