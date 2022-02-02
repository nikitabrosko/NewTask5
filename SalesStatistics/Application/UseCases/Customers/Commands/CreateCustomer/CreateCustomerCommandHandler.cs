using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System.Linq;

namespace Application.UseCases.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateCustomerCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var entity = new Customer
            {
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            var checkForExistsEntity = _context.Customers
                .Any(customer => customer.FirstName.Equals(entity.FirstName) 
                                 && customer.LastName.Equals(entity.LastName));

            if (checkForExistsEntity)
            {
                throw new ItemExistsException($"{nameof(Customer)} with first and last name, that you write, is already exists!");
            }

            await _context.Customers.AddAsync(entity, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}