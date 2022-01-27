using System;
using MediatR;

namespace Application.UseCases.Orders.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<int>
    {
        public DateTime Date { get; set; }

        public decimal Sum { get; set; }

        public int CustomerId { get; set; }

        public int ManagerId { get; set; }

        public int ProductId { get; set; }
    }
}