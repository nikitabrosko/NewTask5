using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Models;
using Application.UseCases.Customers.Commands.CreateCustomer;
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

        [HttpGet]
        public IActionResult Update([FromQuery] CustomerDto customer)
        {
            return View(new UpdateCustomerCommand
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName
            });
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromForm] UpdateCustomerCommand command)
        {
            await Mediator.Send(command);

            return RedirectToAction("CustomersPage");
        }
    }
}
