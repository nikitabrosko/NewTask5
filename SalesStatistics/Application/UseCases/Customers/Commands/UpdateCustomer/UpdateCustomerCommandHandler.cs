using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Customers.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateCustomerCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Customers
                .FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity is null)
            {
                throw new NotFoundException(nameof(Customer), request.Id);
            }

            var checkForExistsEntity = _context.Customers
                .Any(customer => customer.FirstName.Equals(request.FirstName)
                                 && customer.LastName.Equals(request.LastName));

            if (checkForExistsEntity)
            {
                throw new ItemExistsException($"{nameof(Customer)} with first and last name, that you write, is already exists!");
            }

            entity.FirstName = request.FirstName;
            entity.LastName = request.LastName;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}