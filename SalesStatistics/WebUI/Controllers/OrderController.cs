using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.Common.Models;
using Application.UseCases.Orders.Commands.CreateOrder;
using Application.UseCases.Orders.Commands.UpdateOrder;
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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromForm] CreateOrderCommand command)
        {
            await Mediator.Send(command);

            return RedirectToAction("OrdersPage");
        }

        [HttpGet("{id:int}")]
        public IActionResult Update([FromRoute] int id)
        {
            return View(new UpdateOrderCommand { Id = id });
        }

        [HttpPost("{command}")]
        [Route("Update/{command}")]
        public async Task<IActionResult> Update([FromForm] UpdateOrderCommand command)
        {
            await Mediator.Send(command);

            return RedirectToAction("OrdersPage");
        }
    }
}
