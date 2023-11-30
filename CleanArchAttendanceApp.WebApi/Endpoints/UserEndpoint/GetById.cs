using Ardalis.Result;
using CleanArchAttendanceApp.UseCases.User.Query.GetById;
using FastEndpoints;
using MediatR;

namespace CleanArchAttendanceApp.WebApi.Endpoints.UserEndpoint;

public class GetById : EndpointWithoutRequest<GetByIdResponse>
{
    private readonly IMediator _mediator;

    public GetById(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Get("users/{Id:Guid}");
        Roles("Admin");
        Description(x => x.WithName("GetUserById"));
        Summary(s =>
        {
            s.Summary = "Get User by Id";
            s.Description = "Return a User with the give id if it exists";
            s.Responses[200] = "ok with the user data";
            s.Responses[404] = "Can't find a user with this Id";
        });
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var userId = Route<Guid>("Id");
        var query = new GetUserByIdQuery(userId);
        var result = await _mediator.Send(query);

        if (result.Status == ResultStatus.NotFound)
            ThrowError("user not found!");

        if (result.Status == ResultStatus.Unauthorized)
            ThrowError("you need to login first!");

        if (result.Status == ResultStatus.Forbidden)
            ThrowError("you shouldn't be here!");

        if (result.IsSuccess)
            Response.User = result.Value;
    }
}
