using Ardalis.Result;
using Azure;
using CleanArchAttendanceApp.Core.Models;
using CleanArchAttendanceApp.UseCases.User.Command.Create;
using FastEndpoints;
using MediatR;

namespace CleanArchAttendanceApp.WebApi.Endpoints.UserEndpoint;

public class Create : Endpoint<CreateRequest, CreateResponse>
{
    private readonly IMediator _mediator;

    public Create(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Post("/users/create");
        //Roles("Admin");
        Roles();
        Summary(s =>
        {
            s.Summary = "Create a new user";
            s.Description = "creates a new user";
            s.ExampleRequest = new CreateRequest()
            {
                FullName = string.Empty,
                UserName = string.Empty,
                Password = string.Empty,
                Role = UserRole.Employee
            };
            s.ResponseExamples[200] = new CreateResponse()
            {
                User = new UserWithoutAttendanceDto()
                {
                    Id = Guid.NewGuid(),
                    FullName = string.Empty,
                    UserName = string.Empty,
                    Role = UserRole.Employee
                }
            };
            s.Responses[200] = "returns the created user";
            s.Responses[401] = "Unauthorized";
            s.Responses[403] = "forbidden";
        });
    }

    public override async Task HandleAsync(CreateRequest req, CancellationToken ct)
    {
        // need more work
        if (req.Role != UserRole.Employee && req.Role != UserRole.Admin)
            ThrowError("Invalid User Role!");
        //ThrowError("Invalid User Role!");

        var command = new CreateUserCommand(
            req.FullName!, req.UserName!, req.Password!, req.Role!);

        var result = await _mediator.Send(command);

        if (result.Status == ResultStatus.Unauthorized)
            //return Result.Error("you need to login first!");
            ThrowError("you need to login first!");

        if (result.Status == ResultStatus.Forbidden)
            //return Result.Error("you shouldn't be here!");
            ThrowError("you shouldn't be here!");

        if (!result.IsSuccess)
            foreach (var error in result.Errors)
                ThrowError(error);

        if(result.IsSuccess)
            Response.User = result.Value;
    }
}