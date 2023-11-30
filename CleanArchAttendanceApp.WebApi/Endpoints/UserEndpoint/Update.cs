using Ardalis.Result;
using CleanArchAttendanceApp.UseCases.User.Command.Create;
using FastEndpoints;
using MediatR;

namespace CleanArchAttendanceApp.WebApi.Endpoints.UserEndpoint;

public class UpdateUser : Endpoint<UpdateRequest, UpdateResponse>
{
    private readonly IMediator _mediator;

    public UpdateUser(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Put("users/{Id:Guid}");
        Roles("Admin");
        Summary(s =>
        {
            s.Summary = "Update a User by Id";
            s.Description = "updates a User with the give id " +
                  "if it exists with the new user object passed";
            s.Responses[200] = "ok with the modified user data";
            s.Responses[404] = "Can't find a user with this Id";
        });
    }
    public override async Task HandleAsync(UpdateRequest req, CancellationToken ct)
    {
        var userId = Route<Guid>("Id");

        var command = new UpdateUserCommand(userId,
            req.FullName!,
            req.UserName!,
            req.Password!,
            req.Role!);

        var result = await _mediator.Send(command, ct);

        if (result.Status == ResultStatus.Unauthorized)
            ThrowError("you need to login first!");

        if (result.Status == ResultStatus.Forbidden)
            ThrowError("you shouldn't be here!");

        if (!result.IsSuccess)
            foreach (var error in result.Errors)
                ThrowError(error);

        if (result.IsSuccess)
            Response.User = result.Value;
    }
}
