using Ardalis.Result;
using CleanArchAttendanceApp.UseCases.User.Command.ClockOut;
using FastEndpoints;
using MediatR;

namespace CleanArchAttendanceApp.WebApi.Endpoints.UserEndpoint;

public class ClockOut : Endpoint<ClockOutRequest, ClockOutResponse>
{
    private readonly IMediator _mediator;

    public ClockOut(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Get("users/{Id:Guid}/clockout");
        Roles();
        Description(x => x.WithName("ClockOut"));
        Summary(s =>
        {
            s.Summary = "Clock-Out for today attendance";
            s.Description = "employee can clock-out for today attendance";
            s.ExampleRequest = new ClockInRequest();
            s.ResponseExamples[200] = new ClockInResponse()
            { Messege = string.Empty };
            s.Responses[200] = "ok, with a messege to see u tomorrow .";
            s.Responses[403] = "forbidden";
        });
    }

    public override async Task HandleAsync(ClockOutRequest req, CancellationToken ct)
    {
        var routeId = Route<Guid>("Id");

        if (routeId.ToString() != req.Id)
            ThrowError("Unauthorized");

        var command = new ClockOutCommand(routeId);
        var result = await _mediator.Send(command, ct);

        if (result.Status == ResultStatus.Unauthorized)
            ThrowError("you need to login first!");

        if (result.Status == ResultStatus.Forbidden)
            ThrowError("you shouldn't be here!");

        if (!result.IsSuccess)
            foreach (var error in result.Errors)
                ThrowError(error);

        if (result.IsSuccess)
            Response.Messege = "see u tomorrow";
    }
}
