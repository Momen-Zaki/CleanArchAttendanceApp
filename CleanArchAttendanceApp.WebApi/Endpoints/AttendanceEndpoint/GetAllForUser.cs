using Ardalis.Result;
using CleanArchAttendanceApp.Core.Interfaces;
using CleanArchAttendanceApp.Core.Models;
using CleanArchAttendanceApp.UseCases.Attendance.Query.GetAllForUer;
using FastEndpoints;
using MediatR;

namespace CleanArchAttendanceApp.WebApi.Endpoints.AttendanceEndpoint;

public class GetAllForUser : EndpointWithoutRequest<GetAllForUserResponse>
{
    private readonly IMediator _mediator;

    public GetAllForUser(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Get("users/{Id:Guid}/attendance");
        Roles("admin");
        Summary(s =>
        {
            s.Summary = "Get all Attendance Records of a User";
            s.Description = "Returns a List of  all Attendance Records of a User";
            s.ResponseExamples[200] = new GetAllForUserResponse()
            { AttendanceList = new List<AttendanceDto>() };
            s.Responses[200] = "Returns a List of  all Attendance Records of a User";
        });
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var userId = Route<Guid>("Id");

        var query = new GetAllForUserQuery(userId);
        var result = await _mediator.Send(query);


        if (result.Status == ResultStatus.Unauthorized)
            ThrowError("you need to login first!");

        if (result.Status == ResultStatus.Forbidden)
            ThrowError("you shouldn't be here!");

        if (!result.IsSuccess)
            foreach (var error in result.Errors)
                ThrowError(error);

        if (result.IsSuccess)
            Response.AttendanceList = result.Value;
    }
}
