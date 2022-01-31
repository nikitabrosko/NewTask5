using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Models;
using Application.UseCases.Customers.Commands.CreateCustomer;
using Application.UseCases.Customers.Commands.DeleteCustomer;
using Application.UseCases.Customers.Commands.UpdateCustomer;
using Application.UseCases.Customers.Queries.GetCustomersWithPagination;
using Application.UseCases.Customers.Queries.GetFilteringCustomersWithPagination;

namespace WebUI.Controllers
{
    public class CustomerController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PaginatedList<CustomerDto>>> CustomersPage(
            [FromQuery] GetCustomersWithPaginationQuery query)
        {
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
            if (query.FirstNameFilter is null && query.LastNameFilter is null)
            {
                return RedirectToAction("CustomersPage");
            }

            return View("CustomersPage", await Mediator.Send(query));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromForm] CreateCustomerCommand command)
        {
            await Mediator.Send(command);

            return RedirectToAction("CustomersPage");
        }

        [HttpGet("{id:int}")]
        public IActionResult Update([FromRoute] int id)
        {
            return View(new UpdateCustomerCommand { Id = id });
        }

        [HttpPost("{command}")]
        [Route("Update/{command}")]
        public async Task<IActionResult> Update([FromForm] UpdateCustomerCommand command)
        {
            await Mediator.Send(command);

            return RedirectToAction("CustomersPage");
        }

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