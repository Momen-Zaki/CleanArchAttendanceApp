using Ardalis.Result;
using CleanArchAttendanceApp.UseCases.User.Query.GetAll;
using FastEndpoints;
using MediatR;

namespace CleanArchAttendanceApp.WebApi.Endpoints.UserEndpoint;

public class GetAllUsers : EndpointWithoutRequest<GetAllResponse>
{
    private readonly IMediator _mediator;

    public GetAllUsers(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Get("users");
        Roles("Admin");
        Summary(s =>
        {
            s.Summary = "Get All Users";
            s.Description = "Get a list of all users";
            s.Responses[200] = "ok with a list of all useres";
            s.Responses[404] = "Can't delete it for now";
        });
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var query = new GetAllUsersQuery();
        var result = await _mediator.Send(query);

        if (result.Status == ResultStatus.Unauthorized)
            ThrowError("you need to login first!");

        if (result.Status == ResultStatus.Forbidden)
            ThrowError("you shouldn't be here!");

        if (result.IsSuccess)
            Response.Users = result.Value;
    }
}
