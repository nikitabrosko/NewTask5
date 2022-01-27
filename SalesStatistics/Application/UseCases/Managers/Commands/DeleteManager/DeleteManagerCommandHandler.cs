using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Managers.Commands.DeleteManager
{
    public class DeleteManagerCommandHandler : IRequestHandler<DeleteManagerCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteManagerCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteManagerCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Managers
                .FindAsync(new object[] {request.Id}, cancellationToken);

            if (entity is null)
            {
                throw new NotFoundException(nameof(Manager), request.Id);
            }

            var ordersCount = await _context.Orders
                .CountAsync(o => o.Manager.Id.Equals(request.Id), cancellationToken);

            if (ordersCount > 0)
            {
                throw new ForeignKeyDeletionException(nameof(Manager), nameof(Order));
            }

            _context.Managers.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}