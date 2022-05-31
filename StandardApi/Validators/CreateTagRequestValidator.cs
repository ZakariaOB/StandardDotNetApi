using FluentValidation;
using StandardApi.Contracts.V1.Requests;

namespace StandardApi.Validators
{
    public class CreateTagRequestValidator : AbstractValidator<CreateTagRequest>
    {
        public CreateTagRequestValidator()
        {
            RuleFor(x => x.Description)
                .NotEmpty()
                // Validate alphanumeric characters
                .Matches("^[a-zA-Z][a-zA-Z0-9]*$")
                .WithMessage("Please provide valid alphanumeric characters");
        }
    }
}
