using System;
using MediatR;

namespace Application.UseCases.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommand : IRequest
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public decimal Sum { get; set; }

        public int CustomerId { get; set; }

        public int ManagerId { get; set; }

        public int ProductId { get; set; }
    }
}