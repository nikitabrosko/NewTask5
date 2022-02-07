using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Models;
using Application.UseCases.Products.Commands.CreateProduct;
using Application.UseCases.Products.Commands.DeleteProduct;
using Application.UseCases.Products.Commands.UpdateProduct;
using Application.UseCases.Products.Queries.GetFilteringProductsWithPagination;
using Application.UseCases.Products.Queries.GetProductsWithPagination;
using Microsoft.AspNetCore.Authorization;

namespace WebUI.Controllers
{
    [Authorize]
    public class ProductController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PaginatedList<ProductDto>>> ProductsPage(
            [FromQuery] GetProductsWithPaginationQuery query)
        {
            ViewBag.Title = "Products page";

            return View(await Mediator.Send(query));
        }

        [HttpPost]
        public async Task<ActionResult<PaginatedList<CustomerDto>>> FilteringProductsPage(
            [FromForm] GetFilteringProductsWithPaginationQuery query)
        {
            ViewBag.Title = "Products page";

            if (query.NameFilter is null && query.PriceFilter is null)
            {
                return RedirectToAction("ProductsPage");
            }

            return View("ProductsPage", await Mediator.Send(query));
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Title = "Create product page";

            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult<int>> Create([FromForm] CreateProductCommand command)
        {
            try
            {
                await Mediator.Send(command);
            }
            catch (ItemExistsException exception)
            {
                return View("Error", new[] { exception.Message });
            }

            return RedirectToAction("ProductsPage");
        }

        [Authorize(Roles = "admin")]
        [HttpGet("{id:int}")]
        public IActionResult Update([FromRoute] int id)
        {
            ViewBag.Title = "Update product page";

            return View(new UpdateProductCommand { Id = id });
        }

        [Authorize(Roles = "admin")]
        [HttpPost("{command}")]
        [Route("Update/{command}")]
        public async Task<IActionResult> Update([FromForm] UpdateProductCommand command)
        {
            try
            {
                await Mediator.Send(command);
            }
            catch (ItemExistsException exception)
            {
                return View("Error", new[] { exception.Message });
            }

            return RedirectToAction("ProductsPage");
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        [Route("Delete/{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id,
            [FromQuery] GetProductsWithPaginationQuery query)
        {
            await Mediator.Send(new DeleteProductCommand { Id = id });

            return View("ProductsData", await Mediator.Send(query));
        }
    }
}