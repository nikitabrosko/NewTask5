using Application.Common.Models;
using Application.UseCases.Customers.Queries.GetCustomersWithPagination;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using System.Linq;

namespace Application.UseCases.Managers.Queries.GetManagersWithPagination
{
    public class GetManagersWithPaginationQueryHandler : IRequestHandler<GetManagersWithPaginationQuery, PaginatedList<ManagerDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetManagersWithPaginationQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedList<ManagerDto>> Handle(GetManagersWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Managers
                .Select(manager => new ManagerDto
                {
                    Id = manager.Id,
                    LastName = manager.LastName
                })
                .OrderBy(m => m.Id);

            return await PaginatedList<ManagerDto>.CreateAsync(query, request.PageNumber, request.PageSize);
        }
    }
}