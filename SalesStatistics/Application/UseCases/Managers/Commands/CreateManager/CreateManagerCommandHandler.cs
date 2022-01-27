using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Managers.Commands.CreateManager
{
    public class CreateManagerCommandHandler : IRequestHandler<CreateManagerCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateManagerCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateManagerCommand request, CancellationToken cancellationToken)
        {
            var entity = new Manager
            {
                LastName = request.LastName
            };

            await _context.Managers.AddAsync(entity, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}