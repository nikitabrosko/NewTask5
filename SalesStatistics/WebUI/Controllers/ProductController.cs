using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.Common.Models;
using Application.UseCases.Products.Commands.CreateProduct;
using Application.UseCases.Products.Commands.UpdateProduct;
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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromForm] CreateProductCommand command)
        {
            await Mediator.Send(command);

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
            await Mediator.Send(command);

            return RedirectToAction("ProductsPage");
        }
    }
}