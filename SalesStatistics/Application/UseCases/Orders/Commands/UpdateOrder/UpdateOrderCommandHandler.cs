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

            var customerEntity = await GetCustomerAsync(entity.Customer, request.CustomerId, cancellationToken);
            var managerEntity = await GetManagerAsync(entity.Manager, request.ManagerId, cancellationToken);
            var productEntity = await GetProductAsync(entity.Product, request.ProductId, cancellationToken);

            entity.Date = request.Date;
            entity.Sum = request.Sum;
            entity.Customer = customerEntity;
            entity.Manager = managerEntity;
            entity.Product = productEntity;

            _context.Orders.Update(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        protected async Task<Customer> GetCustomerAsync(Customer customer, int id, CancellationToken cancellationToken)
        {
            if (customer.Id.Equals(id))
            {
                return customer;
            }

            var customerEntity = await _context.Customers
                .FindAsync(new object[] {id}, cancellationToken);

            if (customerEntity is null)
            {
                throw new NotFoundException(nameof(Customer), id);
            }

            return customerEntity;
        }

        protected async Task<Manager> GetManagerAsync(Manager manager, int id, CancellationToken cancellationToken)
        {
            if (manager.Id.Equals(id))
            {
                return manager;
            }

            var managerEntity = await _context.Managers
                .FindAsync(new object[] {id}, cancellationToken);

            if (managerEntity is null)
            {
                throw new NotFoundException(nameof(Manager), id);
            }

            return managerEntity;
        }

        protected async Task<Product> GetProductAsync(Product product, int id, CancellationToken cancellationToken)
        {
            if (product.Id.Equals(id))
            {
                return product;
            }

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