using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Managers.Commands.UpdateManager
{
    public class UpdateManagerCommandHandler : IRequestHandler<UpdateManagerCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateManagerCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateManagerCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Managers
                .FindAsync(new object[] {request.Id}, cancellationToken);

            if (entity is null)
            {
                throw new NotFoundException(nameof(Manager), request.Id);
            }

            var checkForExistsEntity = _context.Managers
                .Any(manager => manager.LastName.Equals(entity.LastName));

            if (checkForExistsEntity)
            {
                throw new ItemExistsException($"{nameof(Manager)} with last name, that you write, is already exists!");
            }

            entity.LastName = request.LastName;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}