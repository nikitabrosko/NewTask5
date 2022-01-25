using FluentValidation;

namespace Application.UseCases.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(v => v.FirstName)
                .MaximumLength(20)
                .NotEmpty()
                .WithMessage("First name is required!");

            RuleFor(v => v.LastName)
                .MaximumLength(30)
                .NotEmpty()
                .WithMessage("Last name is required!");
        }
    }
}