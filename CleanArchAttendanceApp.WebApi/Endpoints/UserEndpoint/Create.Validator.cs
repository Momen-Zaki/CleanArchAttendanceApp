using FastEndpoints;
using FluentValidation;

namespace CleanArchAttendanceApp.WebApi.Endpoints.UserEndpoint;

public class CreateRequestValidator : Validator<CreateRequest>
{
    public CreateRequestValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty()
            .WithMessage("Full Name is required!")
            .MinimumLength(5)
            .WithMessage("Full Name can't be less than 5 character")
            .MaximumLength(60)
            .WithMessage("Full Name can't be more than 60 character");

        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage("Username is required!")
            .MinimumLength(3)
            .WithMessage("Username can't be less than 3 character")
            .MaximumLength(50)
            .WithMessage("Username can't be more than 60 character");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required!")
            .MinimumLength(8)
            .WithMessage("Password can't be less than 8 character")
            .MaximumLength(32)
            .WithMessage("Password can't be more than 32 character")
            .Matches("^(?=.*[a-zA-Z])(?=.*\\d).{8,}$")
            .WithMessage("Password Must contain letters and numbers");

        //RuleFor(x => x.Role)
        //    .Must(r => r == UserRole.Admin || r == UserRole.Admin);
    }
}
