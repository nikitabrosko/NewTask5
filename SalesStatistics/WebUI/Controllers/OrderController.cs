using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Models;
using Application.UseCases.Orders.Commands.CreateOrder;
using Application.UseCases.Orders.Commands.DeleteOrder;
using Application.UseCases.Orders.Commands.UpdateOrder;
using Application.UseCases.Orders.Queries.GetFilteringOrdersWithPagination;
using Application.UseCases.Orders.Queries.GetOrdersWithPagination;
using Microsoft.AspNetCore.Authorization;

namespace WebUI.Controllers
{
    [Authorize]
    public class OrderController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PaginatedList<OrderDto>>> OrdersPage(
            [FromQuery] GetOrdersWithPaginationQuery query)
        {
            return View(await Mediator.Send(query));
        }

        [HttpPost]
        public async Task<ActionResult<PaginatedList<CustomerDto>>> FilteringOrdersPage(
            [FromForm] GetFilteringOrdersWithPaginationQuery query)
        {
            if (query.DateFromFilter is null && query.DateToFilter is null && query.Sum is null)
            {
                return RedirectToAction("OrdersPage");
            }

            return View("OrdersPage", await Mediator.Send(query));
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult<int>> Create([FromForm] CreateOrderCommand command)
        {
            try
            {
                await Mediator.Send(command);
            }
            catch (NotFoundException exception)
            {
                return View("Error", new[] { exception.Message });
            }

            return RedirectToAction("OrdersPage");
        }

        [Authorize(Roles = "admin")]
        [HttpGet("{id:int}")]
        public IActionResult Update([FromRoute] int id)
        {
            return View(new UpdateOrderCommand { Id = id });
        }

        [Authorize(Roles = "admin")]
        [HttpPost("{command}")]
        [Route("Update/{command}")]
        public async Task<IActionResult> Update([FromForm] UpdateOrderCommand command)
        {
            try
            {
                await Mediator.Send(command);
            }
            catch (NotFoundException exception)
            {
                return View("Error", new[] { exception.Message });
            }

            return RedirectToAction("OrdersPage");
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        [Route("Delete/{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id,
            [FromQuery] GetOrdersWithPaginationQuery query)
        {
            await Mediator.Send(new DeleteOrderCommand { Id = id });

            return View("OrdersData", await Mediator.Send(query));
        }
    }
}
