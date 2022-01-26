using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Models;
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
    }
}
