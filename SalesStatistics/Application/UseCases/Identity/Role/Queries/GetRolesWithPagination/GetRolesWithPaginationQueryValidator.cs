using FluentValidation;

namespace Application.UseCases.Identity.Role.Queries.GetRolesWithPagination
{
    public class GetRolesWithPaginationQueryValidator : AbstractValidator<GetRolesWithPaginationQuery>
    {
        public GetRolesWithPaginationQueryValidator()
        {
            RuleFor(v => v.PageNumber)
                .GreaterThanOrEqualTo(1).WithMessage("Page number at least greater than or equal to 1!");

            RuleFor(v => v.PageSize)
                .GreaterThanOrEqualTo(1).WithMessage("Page size at least greater than or equal to 1!");
        }
    }
}