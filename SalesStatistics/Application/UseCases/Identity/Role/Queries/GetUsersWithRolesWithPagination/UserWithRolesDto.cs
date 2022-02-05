using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Application.UseCases.Identity.Role.Queries.GetUsersWithRolesWithPagination
{
    public class UserWithRolesDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}