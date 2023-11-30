using Ardalis.Result;
using CleanArchAttendanceApp.Core.Interfaces;
using CleanArchAttendanceApp.UseCases.User.Command.Create;
using FastEndpoints;
using MediatR;

namespace CleanArchAttendanceApp.WebApi.Endpoints.UserEndpoint;

public class Delete : EndpointWithoutRequest<DeleteResponse>
{
    private readonly IMediator _mediator;

    public Delete(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Delete("users/{Id:Guid}");
        Roles("Admin");
        Summary(s =>
        {
            s.Summary = "Delete User by Id";
            s.Description = "Delete a User with the give id if it exists";
            s.ResponseExamples[200] = new DeleteResponse { Message = "user deleted!" };
            s.Responses[200] = "ok with a confirmation message";
            s.Responses[404] = "Can't delete it for now";
        });
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var userId = Route<Guid>("Id");

        var command = new DeleteUserCommand(userId);
        var result = await _mediator.Send(command);

        if (result.Status == ResultStatus.Unauthorized)
            ThrowError("you need to login first!");

        if (result.Status == ResultStatus.Forbidden)
            ThrowError("you shouldn't be here!");

        if (!result.IsSuccess)
            foreach (var error in result.Errors)
                ThrowError(error);

        if (result.IsSuccess)
            Response.Message = "user deleted!";
    }
}
