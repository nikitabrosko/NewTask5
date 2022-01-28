using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.Common.Models;
using Application.UseCases.Managers.Commands.CreateManager;
using Application.UseCases.Managers.Commands.DeleteManager;
using Application.UseCases.Managers.Commands.UpdateManager;
using Application.UseCases.Managers.Queries.GetFilteringManagersWithPagination;
using Application.UseCases.Managers.Queries.GetManagersWithPagination;

namespace WebUI.Controllers
{
    public class ManagerController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PaginatedList<ManagerDto>>> ManagersPage(
            [FromQuery] GetManagersWithPaginationQuery query)
        {
            return View(await Mediator.Send(query));
        }

        [HttpPost]
        public async Task<ActionResult<PaginatedList<CustomerDto>>> FilteringManagersPage(
            [FromForm] GetFilteringManagersWithPaginationQuery query)
        {
            if (query.LastNameFilter is null)
            {
                return RedirectToAction("ManagersPage");
            }

            return View("ManagersPage", await Mediator.Send(query));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromForm] CreateManagerCommand command)
        {
            await Mediator.Send(command);

            return RedirectToAction("ManagersPage");
        }

        [HttpGet("{id:int}")]
        public IActionResult Update([FromRoute] int id)
        {
            return View(new UpdateManagerCommand { Id = id });
        }

        [HttpPost("{command}")]
        [Route("Update/{command}")]
        public async Task<IActionResult> Update([FromForm] UpdateManagerCommand command)
        {
            await Mediator.Send(command);

            return RedirectToAction("ManagersPage");
        }

        [HttpDelete("{id}")]
        [Route("Delete/{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await Mediator.Send(new DeleteManagerCommand { Id = id });

            return NoContent();
        }
    }
}