using FluentValidation;

namespace Application.UseCases.Customers.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
    {
        public UpdateCustomerCommandValidator()
        {
            RuleFor(v => v.FirstName)
                .MaximumLength(20)
                .NotEmpty();

            RuleFor(v => v.LastName)
                .MaximumLength(30)
                .NotEmpty();
        }
    }
}