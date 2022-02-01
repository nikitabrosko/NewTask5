using FluentValidation;

namespace Application.UseCases.Customers.Queries.GetCustomersWithPagination
{
    public class GetCustomersWithPaginationQueryValidator : AbstractValidator<GetCustomersWithPaginationQuery>
    {
        public GetCustomersWithPaginationQueryValidator()
        {
            RuleFor(v => v.PageNumber)
                .GreaterThanOrEqualTo(1).WithMessage("Page number at least greater than or equal to 1!");

            RuleFor(v => v.PageSize)
                .GreaterThanOrEqualTo(1).WithMessage("Page size at least greater than or equal to 1!");
        }
    }
}