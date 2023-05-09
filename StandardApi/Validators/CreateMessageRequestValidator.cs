using FluentValidation;
using StandardApi.Contracts.V1.Requests;

namespace StandardApi.Validators
{
    public class CreateMessageRequestValidator : AbstractValidator<CreateMessageRequest>
    {
        public CreateMessageRequestValidator()
        {
            RuleFor(x => x.Text)
                .NotEmpty()
                // Validate alphanumeric characters
                .Matches("^[a-zA-Z][a-zA-Z0-9]*$")
                .WithMessage("Please provide valid alphanumeric characters");
        }
    }
}
