using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Models;
using Application.UseCases.Customers.Commands.CreateCustomer;
using Application.UseCases.Customers.Commands.DeleteCustomer;
using Application.UseCases.Customers.Commands.UpdateCustomer;
using Application.UseCases.Customers.Queries.GetCustomersWithPagination;
using Application.UseCases.Customers.Queries.GetFilteringCustomersWithPagination;
using Microsoft.AspNetCore.Authorization;

namespace WebUI.Controllers
{
    [Authorize]
    public class CustomerController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PaginatedList<CustomerDto>>> CustomersPage(
            [FromQuery] GetCustomersWithPaginationQuery query)
        {
            ViewBag.Title = "Customers page";

            return View(await Mediator.Send(query));
        }

        [HttpGet]
        public IActionResult Filter()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult<PaginatedList<CustomerDto>>> FilteringCustomersPage(
            [FromForm] GetFilteringCustomersWithPaginationQuery query)
        {
            ViewBag.Title = "Customers page";

            if (query.FirstNameFilter is null && query.LastNameFilter is null)
            {
                return RedirectToAction("CustomersPage");
            }

            return View("CustomersPage", await Mediator.Send(query));
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Title = "Create customer page";

            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult<int>> Create([FromForm] CreateCustomerCommand command)
        {
            try
            {
                await Mediator.Send(command);
            }
            catch (ItemExistsException exception)
            {
                return View("Error", new []{exception.Message});
            }

            return RedirectToAction("CustomersPage");
        }

        [Authorize(Roles = "admin")]
        [HttpGet("{id:int}")]
        public IActionResult Update([FromRoute] int id)
        {
            ViewBag.Title = "Update customer page";

            return View(new UpdateCustomerCommand { Id = id });
        }

        [Authorize(Roles = "admin")]
        [HttpPost("{command}")]
        [Route("Update/{command}")]
        public async Task<IActionResult> Update([FromForm] UpdateCustomerCommand command)
        {
            try
            {
                await Mediator.Send(command);
            }
            catch (ItemExistsException exception)
            {
                return View("Error", new[] { exception.Message });
            }

            return RedirectToAction("CustomersPage");
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        [Route("Delete/{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id,
            [FromQuery] GetCustomersWithPaginationQuery query)
        {
            await Mediator.Send(new DeleteCustomerCommand { Id = id });

            return View("CustomersData", await Mediator.Send(query));
        }
    }
}