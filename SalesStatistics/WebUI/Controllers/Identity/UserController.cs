using System.Threading.Tasks;
using Application.UseCases.Identity.User.Commands.CreateUser;
using Application.UseCases.Identity.User.Commands.DeleteUser;
using Application.UseCases.Identity.User.Commands.UpdateUser;
using Application.UseCases.Identity.User.Queries.GetUsersWithPagination;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers.Identity
{
    public class UserController : ApiControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> UsersPage([FromQuery] GetUsersWithPaginationQuery query)
        {
            return View(await Mediator.Send(query));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateUserCommand command)
        {
            await Mediator.Send(command);

            return RedirectToAction("UsersPage");
        }

        [HttpGet]
        public IActionResult Update([FromRoute] string id)
        {
            return View(id);
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromForm] UpdateUserCommand command)
        {
            await Mediator.Send(command);

            return RedirectToAction("UsersPage");
        }

        [HttpDelete("{id}")]
        [Route("Delete/{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            await Mediator.Send(new DeleteUserCommand {Id = id});

            return RedirectToAction("UsersPage");
        }
    }
}