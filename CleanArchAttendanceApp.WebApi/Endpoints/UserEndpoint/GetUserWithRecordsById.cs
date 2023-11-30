using Ardalis.Result;
using CleanArchAttendanceApp.UseCases.User.Query.GetById;
using FastEndpoints;
using MediatR;

namespace CleanArchAttendanceApp.WebApi.Endpoints.UserEndpoint;

public class GetUserWithRecordsById: EndpointWithoutRequest<GetUserWithRecordsByIdResponse>
{
    private readonly IMediator _mediator;

    public GetUserWithRecordsById(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Get("users/{Id:Guid}/withrecords");
        Roles("Admin");
        Description(x => x.WithName("GetUserWithRecordsById"));
        Summary(s =>
        {
            s.Summary = "Get User by Id with All his Attendance Record";
            s.Description = "Return a User with All his Attendance " +
                  "Record by the give id if it exists";
            s.Responses[200] = "ok with the user data";
            s.Responses[404] = "Can't find a user with this Id";
        });
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var userId = Route<Guid>("Id");

        var query = new GetUserWithRecordsQuery(userId);
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
