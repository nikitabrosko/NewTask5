using FluentValidation;

namespace Application.UseCases.Managers.Commands.UpdateManager
{
    public class UpdateManagerCommandValidator : AbstractValidator<UpdateManagerCommand>
    {
        public UpdateManagerCommandValidator()
        {
            RuleFor(v => v.LastName)
                .MinimumLength(2).WithMessage("Last name length must be greater than or equal to 2!")
                .MaximumLength(30).WithMessage("Last name length must be lower than or equal to 30!")
                .NotEmpty().WithMessage("Last name is required!");
        }
    }
}