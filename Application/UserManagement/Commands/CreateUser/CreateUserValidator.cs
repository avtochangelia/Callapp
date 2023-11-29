using FluentValidation;

namespace Application.UserManagement.Commands.CreateUser;

public class CreateUserValidator : AbstractValidator<CreateUser>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty();

        RuleFor(x => x.LastName).NotEmpty();

        RuleFor(x => x.PersonalNumber)
            .NotEmpty()
            .Length(11)
            .Must(BeOnlyDigits).WithMessage("PersonalNumber must contain only digits.");

        RuleFor(x => x.UserName)
            .NotEmpty()
            .MinimumLength(4)
            .MaximumLength(15);

        RuleFor(x => x.Password)
            .NotEmpty()
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$")
            .WithMessage("Password must contain at least one uppercase letter, one lowercase letter, one digit, and be at least 8 characters long.");

        RuleFor(x => x.Email)
            .EmailAddress();
    }

    private bool BeOnlyDigits(string personalNumber)
    {
        return personalNumber.All(char.IsDigit);
    }
}