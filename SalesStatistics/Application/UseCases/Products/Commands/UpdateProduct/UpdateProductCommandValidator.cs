using FluentValidation;

namespace Application.UseCases.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(v => v.Name)
                .MaximumLength(30)
                .NotEmpty()
                .WithMessage("Name is invalid!");

            RuleFor(v => v.Price)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Price must be greater than or equal to 0");
        }
    }
}