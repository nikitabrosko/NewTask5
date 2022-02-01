using FluentValidation;

namespace Application.UseCases.Managers.Commands.CreateManager
{
    public class CreateManagerCommandValidator : AbstractValidator<CreateManagerCommand>
    {
        public CreateManagerCommandValidator()
        {
            RuleFor(v => v.LastName)
                .MinimumLength(2).WithMessage("Last name length must be greater than or equal to 2!")
                .MaximumLength(30).WithMessage("Last name length must be lower than or equal to 30!")
                .NotEmpty().WithMessage("Last name is required!");
        }
    }
}