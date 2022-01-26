using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Models;
using Application.UseCases.Customers.Commands.CreateCustomer;
using Application.UseCases.Customers.Queries.GetCustomersWithPagination;
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
            return View("Create");
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromForm] CreateCustomerCommand command)
        {
            await Mediator.Send(command);

            return RedirectToAction("CustomersPage");
        }
    }
}
