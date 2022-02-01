using FluentValidation;

namespace Application.UseCases.Customers.Queries.GetFilteringCustomersWithPagination
{
    public class GetFilteringCustomersWithPaginationQueryValidator : AbstractValidator<GetFilteringCustomersWithPaginationQuery>
    {
        public GetFilteringCustomersWithPaginationQueryValidator()
        {
            RuleFor(v => v.PageNumber)
                .GreaterThanOrEqualTo(1).WithMessage("Page number at least greater than or equal to 1!");

            RuleFor(v => v.PageSize)
                .GreaterThanOrEqualTo(1).WithMessage("Page size at least greater than or equal to 1!");
        }
    }
}