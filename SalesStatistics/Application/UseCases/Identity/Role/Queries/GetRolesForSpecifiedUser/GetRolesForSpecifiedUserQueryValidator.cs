using FluentValidation;

namespace Application.UseCases.Identity.Role.Queries.GetRolesForSpecifiedUser
{
    public class GetRolesForSpecifiedUserQueryValidator : AbstractValidator<GetRolesForSpecifiedUserQuery>
    {
        public GetRolesForSpecifiedUserQueryValidator()
        {
            RuleFor(v => v.UserId)
                .NotEmpty().WithMessage("User Id cannot be empty!")
                .NotNull().WithMessage("User Id cannot be null!");
        }
    }
}