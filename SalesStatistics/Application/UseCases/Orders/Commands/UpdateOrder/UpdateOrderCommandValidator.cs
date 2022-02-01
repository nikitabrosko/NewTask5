using FluentValidation;

namespace Application.UseCases.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(v => v.Sum)
                .GreaterThanOrEqualTo(0).WithMessage("Sum must be greater than or equal to 0");

            RuleFor(v => v.CustomerId)
                .GreaterThanOrEqualTo(1).WithMessage("Customer id must be greater than or equal to 1");

            RuleFor(v => v.ManagerId)
                .GreaterThanOrEqualTo(1).WithMessage("Manager id must be greater than or equal to 1");

            RuleFor(v => v.ProductId)
                .GreaterThanOrEqualTo(1).WithMessage("Product id must be greater than or equal to 1");
        }
    }
}