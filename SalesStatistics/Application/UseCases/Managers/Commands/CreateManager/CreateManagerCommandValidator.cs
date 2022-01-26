using FluentValidation;

namespace Application.UseCases.Managers.Commands.CreateManager
{
    public class CreateManagerCommandValidator : AbstractValidator<CreateManagerCommand>
    {
        public CreateManagerCommandValidator()
        {
            RuleFor(v => v.LastName)
                .MaximumLength(30)
                .NotEmpty()
                .WithMessage("Last name is required!");
        }
    }
}