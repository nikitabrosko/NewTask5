using FluentValidation;

namespace Application.UseCases.Products.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(v => v.Name)
                .MinimumLength(2).WithMessage("Name length must be greater than or equal to 2!")
                .MaximumLength(30).WithMessage("Name length must be lower than or equal to 30!")
                .NotEmpty().WithMessage("Name is required!");

            RuleFor(v => v.Price)
                .GreaterThanOrEqualTo(0).WithMessage("Price must be greater than or equal to 0");
        }
    }
}