using FastEndpoints;
using Ardalis.Result;
using CleanArchAttendanceApp.UseCases.Auth;
using MediatR;

namespace CleanArchAttendanceApp.WebApi.Endpoints.AuthEndpoint;

public class Login : Endpoint<LoginRequest, LoginResponse>
{
    private readonly IMediator _mediator;

    public Login(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Post("login");
        AllowAnonymous();
        Description(x => x.WithName("Login"));
        Summary(s =>
        {
            s.Summary = "Login";
            s.Description = "Return a Token for the give username and password";
            s.ExampleRequest = new LoginRequest()
            { Username = string.Empty, Password = string.Empty };
            s.ResponseExamples[200] = new LoginResponse();
            s.Responses[200] = "returns a Useraname and the Auth Token";
        });
    }

    public override async Task HandleAsync(LoginRequest req, CancellationToken ct)
    {

        var command = new LoginCommand(req.Username!, req.Password!);

        var result = await _mediator.Send(command, ct);

        if (result.Status == ResultStatus.Unauthorized)
            ThrowError("you need to login first!");

        if (result.Status == ResultStatus.Forbidden)
            ThrowError("you shouldn't be here!");

        if (result.Status == ResultStatus.Invalid)
            ThrowError("Invaild Credentials");

        if (!result.IsSuccess)
            foreach (var error in result.Errors)
                ThrowError(error);

        if (result.IsSuccess)
            Response.UserCredentials = result.Value;
    }
}
