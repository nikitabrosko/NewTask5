using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Models;
using Application.UseCases.Managers.Commands.CreateManager;
using Application.UseCases.Managers.Commands.DeleteManager;
using Application.UseCases.Managers.Commands.UpdateManager;
using Application.UseCases.Managers.Queries.GetFilteringManagersWithPagination;
using Application.UseCases.Managers.Queries.GetManagersWithPagination;
using Microsoft.AspNetCore.Authorization;

namespace WebUI.Controllers
{
    [Authorize]
    public class ManagerController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PaginatedList<ManagerDto>>> ManagersPage(
            [FromQuery] GetManagersWithPaginationQuery query)
        {
            ViewBag.Title = "Managers page";

            return View(await Mediator.Send(query));
        }

        [HttpPost]
        public async Task<ActionResult<PaginatedList<CustomerDto>>> FilteringManagersPage(
            [FromForm] GetFilteringManagersWithPaginationQuery query)
        {
            ViewBag.Title = "Managers page";

            if (query.LastNameFilter is null)
            {
                return RedirectToAction("ManagersPage");
            }

            return View("ManagersPage", await Mediator.Send(query));
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Title = "Create manager page";

            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult<int>> Create([FromForm] CreateManagerCommand command)
        {
            try
            {
                await Mediator.Send(command);
            }
            catch (ItemExistsException exception)
            {
                return View("Error", new[] { exception.Message });
            }

            return RedirectToAction("ManagersPage");
        }

        [Authorize(Roles = "admin")]
        [HttpGet("{id:int}")]
        public IActionResult Update([FromRoute] int id)
        {
            ViewBag.Title = "Update manager page";

            return View(new UpdateManagerCommand { Id = id });
        }

        [Authorize(Roles = "admin")]
        [HttpPost("{command}")]
        [Route("Update/{command}")]
        public async Task<IActionResult> Update([FromForm] UpdateManagerCommand command)
        {
            try
            {
                await Mediator.Send(command);
            }
            catch (ItemExistsException exception)
            {
                return View("Error", new[] { exception.Message });
            }

            return RedirectToAction("ManagersPage");
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        [Route("Delete/{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id,
            [FromQuery] GetManagersWithPaginationQuery query)
        {
            await Mediator.Send(new DeleteManagerCommand { Id = id });

            return View("ManagersData", await Mediator.Send(query));
        }
    }
}