using MediatR;

namespace Application.UseCases.Customers.Commands.DeleteCustomer
{
    public class DeleteCustomerCommand : IRequest
    {
        public int Id { get; set; }
    }
}