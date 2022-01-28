using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;

namespace Application.UseCases.Managers.Queries.GetFilteringManagersWithPagination
{
    public class GetFilteringManagersWithPaginationQueryHandler : IRequestHandler<GetFilteringManagersWithPaginationQuery, PaginatedList<ManagerDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetFilteringManagersWithPaginationQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedList<ManagerDto>> Handle(GetFilteringManagersWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Managers
                .Where(manager => manager.LastName.Equals(request.LastNameFilter)
                                  || manager.LastName.StartsWith(request.LastNameFilter))
                .Select(manager => new ManagerDto
                {
                    Id = manager.Id,
                    LastName = manager.LastName
                })
                .OrderBy(manager => manager.Id);

            return await PaginatedList<ManagerDto>.CreateAsync(query, request.PageNumber, request.PageSize);
        }
    }
}