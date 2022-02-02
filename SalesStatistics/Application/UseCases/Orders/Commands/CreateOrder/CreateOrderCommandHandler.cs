using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateOrderCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var customerEntity = await GetCustomerAsync(request.CustomerId, cancellationToken);
            var managerEntity = await GetManagerAsync(request.ManagerId, cancellationToken);
            var productEntity = await GetProductAsync(request.ProductId, cancellationToken);

            var entity = new Order
            {
                Date = request.Date,
                Sum = request.Sum,
                Customer = customerEntity,
                Manager = managerEntity,
                Product = productEntity
            };
            
            await _context.Orders.AddAsync(entity, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }

        protected async Task<Customer> GetCustomerAsync(int id, CancellationToken cancellationToken)
        {
            var customerEntity = await _context.Customers
                .FindAsync(new object[] { id }, cancellationToken);

            if (customerEntity is null)
            {
                throw new NotFoundException(nameof(Customer), id);
            }

            return customerEntity;
        }

        protected async Task<Manager> GetManagerAsync(int id, CancellationToken cancellationToken)
        {
            var managerEntity = await _context.Managers
                .FindAsync(new object[] { id }, cancellationToken);

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