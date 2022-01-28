using FluentValidation;

namespace Application.UseCases.Products.Queries.GetFilteringProductsWithPagination
{
    public class GetFilteringProductsWithPaginationQueryValidator : AbstractValidator<GetFilteringProductsWithPaginationQuery>
    {
        public GetFilteringProductsWithPaginationQueryValidator()
        {
            RuleFor(v => v.PageNumber)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Page number should be at least greater than or equal to 1!");

            RuleFor(v => v.PageSize)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Page size at least greater than or equal to 1!");
        }
    }
}