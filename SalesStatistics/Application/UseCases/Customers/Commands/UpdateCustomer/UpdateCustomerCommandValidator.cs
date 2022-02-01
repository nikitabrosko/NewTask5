using FluentValidation;

namespace Application.UseCases.Customers.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
    {
        public UpdateCustomerCommandValidator()
        {
            RuleFor(v => v.FirstName)
                .MinimumLength(2).WithMessage("First name length must be greater than or equal to 2!")
                .MaximumLength(20).WithMessage("First name length must be lower than or equal to 20!")
                .NotEmpty().WithMessage("First name is required!");

            RuleFor(v => v.LastName)
                .MinimumLength(2).WithMessage("Last name length must be greater than or equal to 2!")
                .MaximumLength(30).WithMessage("Last name length must be lower than or equal to 30!")
                .NotEmpty().WithMessage("Last name is required!");
        }
    }
}