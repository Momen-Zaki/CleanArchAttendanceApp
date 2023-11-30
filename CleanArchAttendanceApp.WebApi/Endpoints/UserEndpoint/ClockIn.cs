using Ardalis.Result;
using CleanArchAttendanceApp.UseCases.User.Command.ClockIn;
using FastEndpoints;
using MediatR;

namespace CleanArchAttendanceApp.WebApi.Endpoints.UserEndpoint;

public class ClockIn : Endpoint<ClockInRequest, ClockInResponse>
{
    private readonly IMediator _mediator;

    public ClockIn(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Get("users/{Id:Guid}/clockin");
        Roles();
        Description(x => x.WithName("ClockIn"));
        Summary(s =>
        {
            s.Summary = "Clock-in for today attendance";
            s.Description = "employee can clock-in for today attendance";
            s.ExampleRequest = new ClockInRequest();
            s.ResponseExamples[200] = new ClockInResponse()
            { Messege = string.Empty };
            s.Responses[200] = "ok, with a messege to wish u a good day.";
            s.Responses[403] = "forbidden";
        });
    }

    public override async Task HandleAsync(ClockInRequest req, CancellationToken ct)
    {
        var routeId = Route<Guid>("Id");

        if (routeId.ToString() != req.Id)
            ThrowError("Unauthorized");

        var command = new ClockInCommand(routeId);
        var result = await _mediator.Send(command, ct);

        if (result.Status == ResultStatus.Unauthorized)
            ThrowError("you need to login first!");

        if (result.Status == ResultStatus.Forbidden)
            ThrowError("you shouldn't be here!");

        if (!result.IsSuccess)
            foreach (var error in result.Errors)
                ThrowError(error);

        if (result.IsSuccess)
            Response.Messege = "have a nice day";
    }
}
