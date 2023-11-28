﻿using FastEndpoints;
using FluentValidation;

namespace CleanArchAttendanceApp.WebApi.Endpoints.AuthEndpoint;

public class LoginValidator : Validator<LoginRequest>
{
    public LoginValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage("Username is required!");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required!");
    }
}
