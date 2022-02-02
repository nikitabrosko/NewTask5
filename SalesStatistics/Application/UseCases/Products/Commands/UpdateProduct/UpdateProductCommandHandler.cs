using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateProductCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Products
                .FindAsync(new object[] {request.Id}, cancellationToken);

            if (entity is null)
            {
                throw new NotFoundException(nameof(Product), request.Id);
            }

            var checkForExistsEntity = _context.Products
                .Any(product => product.Name.Equals(entity.Name));

            if (checkForExistsEntity)
            {
                throw new ItemExistsException($"{nameof(Product)} with name, that you write, is already exists!");
            }

            entity.Name = request.Name;
            entity.Price = request.Price;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}