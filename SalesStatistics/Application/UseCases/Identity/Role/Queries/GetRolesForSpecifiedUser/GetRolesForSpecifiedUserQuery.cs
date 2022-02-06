using System.Collections.Generic;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.UseCases.Identity.Role.Queries.GetRolesForSpecifiedUser
{
    public class GetRolesForSpecifiedUserQuery : IRequest<IList<string>>
    {
        public string UserId { get; set; }
    }
}