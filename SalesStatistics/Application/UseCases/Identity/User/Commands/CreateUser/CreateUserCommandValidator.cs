using FluentValidation;

namespace Application.UseCases.Identity.User.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(v => v.Name)
                .MinimumLength(2).WithMessage("User name length must be greater than or equal to 2!")
                .MaximumLength(40).WithMessage("User name length must be lower than or equal to 40!")
                .NotEmpty().WithMessage("User name is required!");

            RuleFor(v => v.Email)
                .MinimumLength(5).WithMessage("Email length must be greater than or equal to 2!")
                .MaximumLength(50).WithMessage("Email length is more than 50!")
                .NotEmpty().WithMessage("Email is required!");

            RuleFor(v => v.Password)
                .MinimumLength(4).WithMessage("Password length must be greater than or equal to 4!")
                .MaximumLength(30).WithMessage("Password length must be lower than or equal to 30!")
                .NotEmpty().WithMessage("Password is required!");
        }
    }
}