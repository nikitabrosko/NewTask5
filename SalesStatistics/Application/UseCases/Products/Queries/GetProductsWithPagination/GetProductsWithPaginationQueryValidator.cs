using FluentValidation;

namespace Application.UseCases.Products.Queries.GetProductsWithPagination
{
    public class GetProductsWithPaginationQueryValidator : AbstractValidator<GetProductsWithPaginationQuery>
    {
        public GetProductsWithPaginationQueryValidator()
        {
            RuleFor(v => v.PageNumber)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Page number at least greater than or equal to 1!");

            RuleFor(v => v.PageSize)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Page size at least greater than or equal to 1!");
        }
    }
}