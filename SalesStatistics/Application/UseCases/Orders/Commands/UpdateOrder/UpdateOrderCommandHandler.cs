using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateOrderCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Orders
                .FindAsync(new object[] {request.Id}, cancellationToken);

            if (entity is null)
            {
                throw new NotFoundException(nameof(Order), request.Id);
            }

            var customerEntity = await GetCustomerAsync(request.CustomerId, cancellationToken);
            var managerEntity = await GetManagerAsync(request.ManagerId, cancellationToken);
            var productEntity = await GetProductAsync(request.ProductId, cancellationToken);

            entity.Date = request.Date;
            entity.Sum = request.Sum;
            entity.Customer = customerEntity;
            entity.Manager = managerEntity;
            entity.Product = productEntity;

            _context.Orders.Update(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        protected async Task<Customer> GetCustomerAsync(int id, CancellationToken cancellationToken)
        {
            var customerEntity = await _context.Customers
                .FindAsync(new object[] {id}, cancellationToken);

            if (customerEntity is null)
            {
                throw new NotFoundException(nameof(Customer), id);
            }

            return customerEntity;
        }

        protected async Task<Manager> GetManagerAsync(int id, CancellationToken cancellationToken)
        {
            var managerEntity = await _context.Managers
                .FindAsync(new object[] {id}, cancellationToken);

            if (managerEntity is null)
            {
                throw new NotFoundException(nameof(Manager), id);
            }

            return managerEntity;
        }

        protected async Task<Product> GetProductAsync(int id, CancellationToken cancellationToken)
        {
            var productEntity = await _context.Products
                .FindAsync(new object[] { id }, cancellationToken);

            if (productEntity is null)
            {
                throw new NotFoundException(nameof(Product), id);
            }

            return productEntity;
        }
    }
}