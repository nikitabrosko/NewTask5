using FluentValidation;

namespace Application.UseCases.Products.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(v => v.Name)
                .MaximumLength(30)
                .NotEmpty()
                .WithMessage("Name is required");

            RuleFor(v => v.Price)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Price must be greater than or equal to 0");
        }
    }
}