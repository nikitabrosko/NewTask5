using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Models;
using Application.UseCases.Products.Commands.CreateProduct;
using Application.UseCases.Products.Commands.DeleteProduct;
using Application.UseCases.Products.Commands.UpdateProduct;
using Application.UseCases.Products.Queries.GetFilteringProductsWithPagination;
using Application.UseCases.Products.Queries.GetProductsWithPagination;

namespace WebUI.Controllers
{
    public class ProductController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PaginatedList<ProductDto>>> ProductsPage(
            [FromQuery] GetProductsWithPaginationQuery query)
        {
            return View(await Mediator.Send(query));
        }

        [HttpPost]
        public async Task<ActionResult<PaginatedList<CustomerDto>>> FilteringProductsPage(
            [FromForm] GetFilteringProductsWithPaginationQuery query)
        {
            if (query.NameFilter is null && query.PriceFilter is null)
            {
                return RedirectToAction("ProductsPage");
            }

            return View("ProductsPage", await Mediator.Send(query));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

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

        [HttpGet("{id:int}")]
        public IActionResult Update([FromRoute] int id)
        {
            return View(new UpdateProductCommand { Id = id });
        }

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