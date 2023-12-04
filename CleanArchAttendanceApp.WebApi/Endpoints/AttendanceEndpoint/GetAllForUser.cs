using CleanArchAttendanceApp.Core.Interfaces;
using CleanArchAttendanceApp.Core.Models;
using FastEndpoints;

namespace CleanArchAttendanceApp.WebApi.Endpoints.AttendanceEndpoint;

public class GetAllForUser : EndpointWithoutRequest<GetAllForUserResponse>
{
    private readonly IAttendanceRepository _repositoy;

    public GetAllForUser(IAttendanceRepository repositoy)
    {
        _repositoy = repositoy;
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
            //s.Responses[401] = "Unauthorized";
            //s.Responses[403] = "Forbidden";
        });
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var userId = Route<Guid>("Id");
        var user = await _repositoy.GetUserByIdAsync(userId, true);
        if (user == null)
            ThrowError("user not found");

        var attendancRecords =
            await _repositoy.GetAllAttendanceRecordsByUserIdAsync(user.Id);
        var result = new List<AttendanceDto>();
        foreach (var a in attendancRecords)
        {
            result.Add(new AttendanceDto()
            {
                Id = a.Id,
                AttendanceDay = a.AttendanceDay,
                ClockedIn = a.ClockedIn,
                ClockedInAt = a.ClockedInAt,
                ClockedOut = a.ClockedOut,
                ClockedOutAt = a.ClockedOutAt,
                UserId = a.UserId
            });
        }
        Response.AttendanceList = result;
        await SendAsync(Response, cancellation: ct);
    }
}
