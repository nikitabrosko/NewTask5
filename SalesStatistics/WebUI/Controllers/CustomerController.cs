using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Models;
using Application.UseCases.Customers.Commands.CreateCustomer;
using Application.UseCases.Customers.Commands.DeleteCustomer;
using Application.UseCases.Customers.Commands.UpdateCustomer;
using Application.UseCases.Customers.Queries.GetCustomersWithPagination;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await Mediator.Send(new DeleteCustomerCommand {Id = id});

            return NoContent();
        }
    }
}
