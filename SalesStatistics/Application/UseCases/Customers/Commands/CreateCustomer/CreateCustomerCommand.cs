using MediatR;

namespace Application.UseCases.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommand : IRequest<int>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}