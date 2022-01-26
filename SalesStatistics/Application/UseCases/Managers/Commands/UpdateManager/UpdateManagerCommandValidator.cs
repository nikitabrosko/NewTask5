using FluentValidation;

namespace Application.UseCases.Managers.Commands.UpdateManager
{
    public class UpdateManagerCommandValidator : AbstractValidator<UpdateManagerCommand>
    {
        public UpdateManagerCommandValidator()
        {
            RuleFor(v => v.LastName)
                .MaximumLength(30)
                .NotEmpty()
                .WithMessage("Last name is invalid!");
        }
    }
}