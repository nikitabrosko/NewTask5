using FluentValidation;

namespace Application.UseCases.Identity.Role.Commands.UpdateRole
{
    public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
    {
        public UpdateRoleCommandValidator()
        {
            RuleFor(v => v.Name)
                .MinimumLength(2).WithMessage("Role name length must be greater than or equal to 2!")
                .MaximumLength(40).WithMessage("Role name length must be lower than or equal to 40!")
                .NotEmpty().WithMessage("Role name is required!");
        }
    }
}