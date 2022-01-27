using MediatR;

namespace Application.UseCases.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommand : IRequest
    {
        public int Id { get; set; }
    }
}