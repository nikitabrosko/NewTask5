using MediatR;

namespace Application.UseCases.Products.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<int>
    {
        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}