using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.Common.Models;
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
    }
}
