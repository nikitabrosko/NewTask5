using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.Common.Models;
using Application.UseCases.Orders.Queries.GetOrdersWithPagination;

namespace WebUI.Controllers
{
    public class OrderController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PaginatedList<OrderDto>>> OrdersPage(
            [FromQuery] GetOrdersWithPaginationQuery query)
        {
            return View(await Mediator.Send(query));
        }
    }
}
