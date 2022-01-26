using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.Common.Models;
using Application.UseCases.Managers.Commands.CreateManager;
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
    }
}
